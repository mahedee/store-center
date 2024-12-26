namespace StoreCenter.Application.Interfaces
{
    public interface ITokenGeneratorService
    {
        public string GetJWTToken((string userId, string userName, string email, IList<string> roles, IList<string> permissions) userDetails);
    }
}
