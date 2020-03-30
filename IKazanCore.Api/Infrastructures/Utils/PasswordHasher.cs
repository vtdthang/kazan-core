using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IKazanCore.Api.Infrastructures.Utils
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool ComparePassword(string password, string hashedPassword);
    }
    public class PasswordHasher : IPasswordHasher
    {
        const int COST = 10;

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, workFactor: COST);
        }

        
        public bool ComparePassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
