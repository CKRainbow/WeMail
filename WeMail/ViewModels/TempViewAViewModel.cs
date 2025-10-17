using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using Microsoft.Extensions.Logging;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace WeMail.ViewModels
{
    public class TempViewAViewModel : BindableBase, IConfirmNavigationRequest
    {
        ILogger _logger;

        public TempViewAViewModel(ILogger logger)
        {
            _logger = logger;
        }

        public void ConfirmNavigationRequest(
            NavigationContext navigationContext,
            Action<bool> continuationCallback
        )
        {
            Debug.WriteLine($"I'm navigating to {navigationContext.Uri}");
            if (
                MessageBox.Show("Are you sure to leave View A?", "Question", MessageBoxButton.YesNo)
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

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            _logger.LogInformation("Leaving View A");
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _logger.LogInformation("Arrived at View A");
        }
    }
}
