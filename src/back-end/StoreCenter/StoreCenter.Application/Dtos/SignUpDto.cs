namespace StoreCenter.Application.Dtos
{
    public class SignUpDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
        public List<string> Roles { get; set; } = new();
    }
}
