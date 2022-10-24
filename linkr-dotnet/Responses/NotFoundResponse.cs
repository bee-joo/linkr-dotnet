namespace linkr_dotnet.Responses
{
    public class NotFoundResponse : ResponseModel
    {
        public NotFoundResponse(string message) : base(System.Net.HttpStatusCode.NotFound, message) { }
    }
}
