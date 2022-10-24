using linkr_dotnet.Helpers;
using linkr_dotnet.Responses;
using linkr_dotnet.Repositories;
using StackExchange.Redis;
using linkr_dotnet.Models;
using Swashbuckle.AspNetCore.Annotations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IConnectionMultiplexer>(c => ConnectionMultiplexer.Connect(builder.Configuration["REDIS_CONNECTION_STRING"]));
builder.Services.AddScoped<ILinkRepo, LinkRepo>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.EnableAnnotations();
});

var app = builder.Build();
var idLength = 6;

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseMiddleware<ExceptionHandlerMiddleware>();
}

app.UseHttpsRedirection();

app.MapGet("/{id}", async (string id, ILinkRepo linkRepo) =>
{
    if (id == null)
    {
        return Results.BadRequest(new BadRequestResponse("Url id is empty"));
    }

    var link = await linkRepo.GetLink(id);
    if (link == null)
    {
        return Results.NotFound(new NotFoundResponse($"Link /{id} not found"));
    }
    return Results.Redirect(link);
})
    .Produces(StatusCodes.Status302Found)
    .Produces<ResponseModel>(StatusCodes.Status400BadRequest)
    .Produces<ResponseModel>(StatusCodes.Status404NotFound)
    .WithMetadata(new SwaggerOperationAttribute("Get redirection", "Get redirection by url id"));


app.MapPost("/", async (LinkDTO linkDto, ILinkRepo linkRepo, IConfiguration configuration) =>
{
    var link = linkDto?.Link;

    if (link == null || link.Length == 0)
    {
        return Results.BadRequest(new BadRequestResponse("Url is empty"));
    }

    if (!link.StartsWith("https://"))
    {
        link = string.Concat("https://", link);
    }

    if (!Utility.CheckURLValid(link))
    {
        return Results.BadRequest(new BadRequestResponse("Url is invalid"));
    }

    var id = Utility.GenerateString(idLength);
    await linkRepo.PutLink(new Link { Id = id, Url = link });
    return Results.Ok($"{configuration["DOMAIN"]}/{id}");
})
    .Produces(StatusCodes.Status200OK)
    .Produces<ResponseModel>(StatusCodes.Status400BadRequest)
    .WithMetadata(new SwaggerOperationAttribute("Add URL", "Validate URL and add to DB"));


app.Run();
