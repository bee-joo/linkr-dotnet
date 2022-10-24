namespace linkr_dotnet.Responses
{
    public class BadRequestResponse : ResponseModel
    {
        public BadRequestResponse(string message) : base(System.Net.HttpStatusCode.BadRequest, message) {}
    }
}
