using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace WeMail.CustomControls.CustomControls
{
    public class MessageDialogueControl : BindableBase, IDialogAware
    {
        private DelegateCommand _okCommand;
        private DelegateCommand _cancelCommand;
        private string _message;

        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        public DelegateCommand OkCommand
        {
            get =>
                _okCommand ??= new DelegateCommand(() =>
                {
                    var parameters = new DialogParameters();
                    parameters.Add("Message", Message);
                    RequestClose?.Invoke(new DialogResult(ButtonResult.OK, parameters));
                });
        }

        public DelegateCommand CancelCommand
        {
            get =>
                _cancelCommand ??= new DelegateCommand(() =>
                {
                    RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel));
                });
        }

        public string Title => "Message";

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed() { }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            var parameterContent = parameters.GetValue<string>("Value");
            if (parameterContent != null)
            {
                Message = parameterContent;
            }
        }
    }
}
