using MN_Shop.Server.Models;

namespace MN_Shop.Server.ModelsJson
{
    public class UserDataJson
    {
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Surname { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public int Role { get; set; } = (int)EUserRole.Customer;

        public string Password { get; set; } = string.Empty;

        public string CreatedAt { get; set; } = string.Empty;

        public string DefaultTimeZone { get; set; } = string.Empty;
    }
}
