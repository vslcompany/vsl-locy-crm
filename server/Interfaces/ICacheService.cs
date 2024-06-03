namespace vsl_crm_api.Interfaces
{
    public interface ICacheService
    {
        void SetValue(string key, string value, TimeSpan? expiration = null);
        string? GetValue(string key);
        void Remove(string key);
    }
}
