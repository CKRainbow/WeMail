using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Services.Dialogs;
using WeMail.Common.User;

namespace WeMail.Common.Helpers
{
    public static class Login
    {
        public static void HandleLogin(IDialogService dialogService, IUser user)
        {
            dialogService.ShowDialog(
                "UserLoginView",
                (r) =>
                {
                    var result = r.Result;
                    if (result == ButtonResult.OK)
                    {
                        var parameters = r.Parameters;
                        if (parameters.ContainsKey("LoginStatus"))
                        {
                            var loginStatus = parameters.GetValue<bool>("LoginStatus");
                            user.SetUserLoginState(loginStatus);
                        }
                    }
                }
            );
        }
    }
}
