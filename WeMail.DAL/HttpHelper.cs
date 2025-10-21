using System.Linq.Expressions;
using WeMail.DAL.DTOs;

namespace WeMail.DAL
{
    public static class HttpHelper
    {
        private static List<ContactDto> contacts =
            new()
            {
                new ContactDto
                {
                    Name = "Alice",
                    Email = "alice@abc.com",
                    Age = 18,
                    PhoneNumber = "18698888888",
                    Sex = 1,
                }
            };

        public static List<ContactDto> getContacts()
        {
            return new(contacts);
        }

        public static bool InsertContact(
            string email,
            string phoneNumber,
            string name,
            int age,
            int sex
        )
        {
            contacts.Add(
                new()
                {
                    Email = email,
                    PhoneNumber = phoneNumber,
                    Name = name,
                    Age = age,
                    Sex = sex
                }
            );
            return true;
        }

        public static UserDto? Login(string account, string password)
        {
            // Simulate a login process and return a UserDto
            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            if (account.Equals("admin") && password.Equals("admin"))
            {
                return new UserDto { Token = "dummy" };
            }
            return null;
        }
    }
}
