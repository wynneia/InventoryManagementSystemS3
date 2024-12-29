using System;
using System.Security.Cryptography;
using System.Text;

namespace InventoryMS.Controllers
{
    public static class PasswordHelper
    {
        // Method to hash the password
        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashBytes);
            }
        }

        // Method to verify the password by comparing hashes
        public static bool VerifyPassword(string enteredPassword, string storedPasswordHash)
        {
            string hashedEnteredPassword = HashPassword(enteredPassword);
            return hashedEnteredPassword == storedPasswordHash;
        }
    }
}
