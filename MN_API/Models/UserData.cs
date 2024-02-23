namespace MN_API.Models
{
    public class UserData
    {
        public long Id { get; set; } = 0;

        public string Name { get; set; } = string.Empty;

        public string Surname { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}
