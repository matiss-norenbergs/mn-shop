using MN_Shop.Server.Models;

namespace MN_Shop.Server.Helpers
{
    public class UserHelper
    {
        internal static bool IsValidUserData(UserData userData, bool encryptPassword = true)
        {
            userData.Name = userData.Name.Trim();
            userData.Surname = userData.Surname.Trim();
            userData.Email = userData.Email.Trim();
            
            if (encryptPassword)
                userData.Password = EncryptPassword(userData.Password);

            if (string.IsNullOrEmpty(userData.Name) || string.IsNullOrEmpty(userData.Surname) || string.IsNullOrEmpty(userData.Email) || (encryptPassword && string.IsNullOrEmpty(userData.Password)))
                return false;

            return true;
        }

        internal static string EncryptPassword(string password)
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(password);
            data = System.Security.Cryptography.SHA256.HashData(data);
            var hash = System.Text.Encoding.ASCII.GetString(data);

            return hash;
        }
    }
}
