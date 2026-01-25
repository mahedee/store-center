namespace StoreCenter.Infrastructure.Interfaces
{
    public interface ITokenGenerator
    {
        public string GenerateJWTToken((string userId, string userName, string email, IList<string> roles, IList<string> permissions) userDetails);
    }
}
