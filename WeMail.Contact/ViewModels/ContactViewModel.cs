using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;

namespace WeMail.Contact.ViewModels
{
    public class ContactViewModel : BindableBase
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
    }
}
