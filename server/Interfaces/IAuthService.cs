namespace vsl_crm_api.Interfaces
{
    public interface IAuthService<TModel>
    {
        Task changePassword(TModel data, string newPassword);
        string createToken(TModel data, double expire);
        Task<TModel?> getByEmail(string email);
        Task<TModel?> getById(long id);
        Task<TModel?> getByUsernameAndPassword(string username, string password);
        CookieOptions getCookieOptions(double day, bool isSecure);
        string hashPassword(string password);
    }
}
