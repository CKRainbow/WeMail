using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Prism.Mvvm;
using Prism.Regions;

namespace WeMail.Contact.ViewModels
{
    public class ContactViewModel : BindableBase, INavigationAware
    {
        private ObservableCollection<string> _contacts;

        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public ObservableCollection<string> Contacts
        {
            get =>
                _contacts ??= new ObservableCollection<string>
                {
                    "Alice Johnson",
                    "Bob Smith",
                    "Charlie Brown",
                    "Diana Prince",
                    "Ethan Hunt"
                };
        }

        public ContactViewModel()
        {
            Message = "View A from your Prism Module";
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
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
            Debug.WriteLine("Leave ContactView!");
        }
    }
}
