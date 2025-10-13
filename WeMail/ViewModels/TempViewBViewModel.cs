using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace WeMail.ViewModels
{
    public class TempViewBViewModel : BindableBase, IConfirmNavigationRequest
    {
        public TempViewBViewModel() { }

        public void ConfirmNavigationRequest(
            NavigationContext navigationContext,
            Action<bool> continuationCallback
        )
        {
            if (
                MessageBox.Show("Are you sure to leave View B?", "Question", MessageBoxButton.YesNo)
                == MessageBoxResult.Yes
            )
            {
                continuationCallback(true);
            }
            else
            {
                continuationCallback(false);
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext) { }

        public void OnNavigatedTo(NavigationContext navigationContext) { }
    }
}
