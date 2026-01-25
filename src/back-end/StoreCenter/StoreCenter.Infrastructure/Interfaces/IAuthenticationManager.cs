namespace StoreCenter.Infrastructure.Interfaces
{
    public interface IAuthenticationManager
    {
        Task<bool> SigninUserAsync(string userName, string password);
    }
}
