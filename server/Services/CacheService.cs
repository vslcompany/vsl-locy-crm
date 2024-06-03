using vsl_crm_api.Interfaces;

namespace vsl_crm_api.Services
{
    public class CacheService : ICacheService
    {
        public Task StringDeleteAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task<bool> StringExistAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task<string?> StringGetAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task<bool> StringSetAsync(string key, string value, TimeSpan? expire = null)
        {
            throw new NotImplementedException();
        }
    }
}
