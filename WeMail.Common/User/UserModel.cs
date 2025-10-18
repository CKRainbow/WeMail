using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeMail.Common.User
{
    public class UserModel : IUser
    {
        public string Account { get; set; }
        public string Password { get; set; }
        private bool IsLoggedIn { get; set; }

        public bool IsLogin()
        {
            return IsLoggedIn;
        }

        public void SetUserLoginState(bool state)
        {
            IsLoggedIn = state;
        }
    }
}
