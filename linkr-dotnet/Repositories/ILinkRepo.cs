using linkr_dotnet.Models;

namespace linkr_dotnet.Repositories
{
    public interface ILinkRepo
    {
        Task<string?> GetLink(string id);
        Task<bool> PutLink(Link link);
    }
}
