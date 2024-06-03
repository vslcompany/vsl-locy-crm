namespace vsl_crm_api.Interfaces
{
    public interface ICacheService
    {
        Task<bool> StringSetAsync(string key, string value, TimeSpan? expire = null);
        Task<string?> StringGetAsync(string key);
        Task StringDeleteAsync(string key);
        Task<bool> StringExistAsync(string key);
    }
}
