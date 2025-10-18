using WeMail.DAL.DTOs;

namespace WeMail.DAL
{
    public static class HttpHelper
    {
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
