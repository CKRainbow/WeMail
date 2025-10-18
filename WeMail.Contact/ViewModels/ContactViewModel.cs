using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using WeMail.Common.Events;
using WeMail.Common.Helpers;
using WeMail.Common.MVVM;
using WeMail.Common.User;

namespace WeMail.Contact.ViewModels
{
    public class ContactViewModel : BindableBase, INavigationAware
    {
        private IUser _user;
        private IDialogService _dialogService;
        private IEventAggregator _eventAggregator;

        private ObservableCollection<string> _contacts;

        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public ObservableCollection<string> Contacts
        {
            get => _contacts ??= new ObservableCollection<string>();
        }

        public ContactViewModel(
            IEventAggregator eventAggregator,
            IDialogService dialogService,
            IUser user
        )
        {
            Message = "View A from your Prism Module";
            _eventAggregator = eventAggregator;
            _dialogService = dialogService;
            _user = user;
        }

        private void InitData()
        {
            Message = "WeMail.Contact Prism Module loaded.";
            Contacts.Add("联系人小张");
            Contacts.Add("联系人小李");
        }

        private void OnMessagerReceived(MessagerEventModel messagerObject)
        {
            Debug.WriteLine(
                $"ContactViewModel received message: {messagerObject.Name} {messagerObject.Age}"
            );
        }

        private bool OnMessagerFilter(MessagerEventModel messagerObject)
        {
            return messagerObject.MessagerType == MessagerType.TypeBMessage;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (!_user.IsLogin())
            {
                Login.HandleLogin(_dialogService, _user);

                if (!_user.IsLogin())
                    return;
            }
            InitData();

            _eventAggregator
                .GetEvent<MessagerEvent>()
                .Subscribe(
                    OnMessagerReceived,
                    ThreadOption.PublisherThread,
                    false,
                    OnMessagerFilter
                );
            Debug.WriteLine("Enter ContactView!");
            var contact = navigationContext.Parameters["Contact"]; // 可以直接使用[]操作符，因为进行过重载，并非是对Dictionary操作
            if (contact == null)
                return;
            Debug.WriteLine($"{contact} Received!");
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            _eventAggregator.GetEvent<MessagerEvent>().Unsubscribe(OnMessagerReceived);
            Debug.WriteLine("Leave ContactView!");
        }
    }
}
