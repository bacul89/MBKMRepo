using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBKM.Services
{
    public class HashPasswordService
    {
        private static string GetRandomSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(12);
        }
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, GetRandomSalt());
        }
        public static bool ValidatePassword(string password, string corectHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, corectHash);
        }
        //contoh penggunaan register password>>  Password = Hashing.HashPassword(registerVM.Password)
        //contoh penggunaan validasi password inputan dengan password yang telah di bcrpyt(boolean)>> Hashing.ValidatePassword(logInVM.Password, myAccount.Password)
    }
}
