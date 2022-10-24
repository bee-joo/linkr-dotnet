using linkr_dotnet.Models;
using StackExchange.Redis;

namespace linkr_dotnet.Repositories
{
    public class LinkRepo : ILinkRepo
    {
        private readonly IConnectionMultiplexer _redis;

        public LinkRepo(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public async Task<string?> GetLink(string id)
        {
            var db = _redis.GetDatabase();
            return await db.StringGetAsync(id);
        }

        public async Task<bool> PutLink(Link link)
        {
            var db = _redis.GetDatabase();
            return await db.StringSetAsync(link.Id, link.Url);
        }
    }
}
