using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using WeMail.Contact.Models;
using WeMail.DAL;

namespace WeMail.Contact.ViewModels
{
    public class AddContactViewModel : BindableBase, IDialogAware
    {
        private bool _isInvalid = true;
        private ContactModel _contact;

        private DelegateCommand _addContactCommand;

        public event Action<IDialogResult> RequestClose;

        public bool IsInvalid
        {
            get => _isInvalid;
            set => SetProperty(ref _isInvalid, value);
        }

        public ContactModel Contact
        {
            get => _contact ??= new ContactModel();
            set => SetProperty(ref _contact, value);
        }

        public DelegateCommand AddContactCommand
        {
            get => _addContactCommand ??= new DelegateCommand(AddContactCommandAction);
        }

        public string Title => "Add new contact";

        public AddContactViewModel() { }

        public void AddContactCommandAction()
        {
            if (IsInvalid)
                return;

            HttpHelper.InsertContact(
                Contact.Email,
                Contact.PhoneNumber,
                Contact.Name,
                Contact.Age,
                Contact.Sex
            );

            RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed() { }

        public void OnDialogOpened(IDialogParameters parameters) { }
    }
}
