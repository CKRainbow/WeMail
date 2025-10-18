using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using WeMail.Common.Helpers;
using WeMail.Common.User;

namespace WeMail.Schedule.ViewModels
{
    public class ScheduleViewModel : BindableBase, INavigationAware
    {
        private IUser _user;
        private IDialogService _dialogService;

        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public ScheduleViewModel(IDialogService dialogService, IUser user)
        {
            _user = user;
            _dialogService = dialogService;
            Message = "View A from your Prism Module";
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (!_user.IsLogin())
            {
                Login.HandleLogin(_dialogService, _user);

                if (!_user.IsLogin())
                    return;
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext) { }
    }
}
