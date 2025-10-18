using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using WeMail.DAL;

namespace WeMail.ViewModels
{
    public class UserLoginViewModel : BindableBase, IDialogAware
    {
        private string _account;
        private string _password;

        private DelegateCommand _loginCommand;
        private DelegateCommand _cancelCommand;

        public string Account
        {
            get => _account;
            set => SetProperty(ref _account, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public DelegateCommand LoginCommand
        {
            get => _loginCommand ??= new DelegateCommand(LoginCommandAction);
        }

        public DelegateCommand CancelCommand
        {
            get => _cancelCommand ??= new DelegateCommand(CancelCommandAction);
        }

        public UserLoginViewModel() { }

        public string Title => "登录";

        public event Action<IDialogResult> RequestClose;

        private void LoginCommandAction()
        {
            var parameters = new DialogParameters();
            var userDto = HttpHelper.Login(Account, Password);
            if (userDto != null && !string.IsNullOrEmpty(userDto.Token))
            {
                parameters.Add("LoginStatus", true);
                RequestClose?.Invoke(new DialogResult(ButtonResult.OK, parameters));
            }
        }

        private void CancelCommandAction()
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel));
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed() { }

        public void OnDialogOpened(IDialogParameters parameters) { }
    }
}
