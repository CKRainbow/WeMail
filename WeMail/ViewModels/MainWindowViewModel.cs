using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Security.Policy;
using Microsoft.Extensions.Logging;
using Prism.Commands;
using Prism.Events;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using WeMail.Common.Events;
using WeMail.Common.Helpers;
using WeMail.Common.MVVM;
using WeMail.Common.User;
using WeMail.Models;

namespace WeMail.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "WeMail";

        private ObservableCollection<MainModel> _modules;

        private readonly IRegionManager _regionManager; // manage the regions of the shell
        private readonly IModuleCatalog _moduleCatalog;
        private readonly IDialogService _dialogService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IUser _user;
        private MainModel _currentModel;

        private IRegionNavigationJournal _journal;

        private DelegateCommand _loginCommand;
        public DelegateCommand LoginCommand
        {
            get => _loginCommand ??= new DelegateCommand(LoginCommandAction);
        }
        private DelegateCommand _loadModules;
        private DelegateCommand _openViewA;
        private DelegateCommand _openViewB;
        private DelegateCommand _goBack;
        private DelegateCommand _goForward;
        private DelegateCommand _showDialogue;
        private DelegateCommand<string> _showDialogueWithParameter;

        private CompositeCommand _compositeCommand;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public ObservableCollection<MainModel> Modules
        {
            get => _modules ??= new();
        }

        public MainModel CurrentModel
        {
            get { return _currentModel; }
            set
            {
                SetProperty(ref _currentModel, value);
                Navigate(value);
            }
        }

        public DelegateCommand LoadModules
        {
            get => _loadModules ??= new(InitModules);
        }

        public DelegateCommand OpenViewA
        {
            get => _openViewA ??= new(OpenViewAAction);
        }

        public DelegateCommand OpenViewB
        {
            get => _openViewB ??= new(OpenViewBAction);
        }

        public DelegateCommand GoBack
        {
            get => _goBack ??= new(GoBackAction);
        }

        public DelegateCommand GoForward
        {
            get => _goForward ??= new(GoForwardAction);
        }

        public DelegateCommand ShowDialogue
        {
            get => _showDialogue ??= new(ShowDialogueAction);
        }

        private void LoginCommandAction()
        {
            Login.HandleLogin(_dialogService, _user);
        }

        public DelegateCommand<string> ShowDialogueWithParameter
        {
            get =>
                _showDialogueWithParameter ??= new DelegateCommand<string>(
                    ShowDialogueWithParameterAction
                );
        }

        public CompositeCommand CompositeCommand
        {
            get => _compositeCommand ??= new CompositeCommand();
        }

        private void ShowDialogueAction()
        {
            _dialogService.Show(
                "MessageDialogueView",
                new DialogParameters("Value=Hello from MainWindowViewModel"),
                (r) =>
                {
                    var result = r.Result;
                    if (result == ButtonResult.OK)
                    {
                        var parameters = r.Parameters;
                        Debug.WriteLine(
                            "Dialog returned OK with message: "
                                + parameters.GetValue<string>("Message")
                        );
                    }
                }
            );
        }

        private void ShowDialogueWithParameterAction(string message)
        {
            MessagerEventModel messagerObject =
                new()
                {
                    Name = "John Doe",
                    Age = 30,
                    MessagerType = MessagerType.TypeAMessage
                };

            _eventAggregator.GetEvent<MessagerEvent>().Publish(messagerObject);

            _dialogService.Show(
                "MessageDialogueView",
                new DialogParameters($"Value={message}"),
                (r) =>
                {
                    var result = r.Result;
                    if (result == ButtonResult.OK)
                    {
                        var parameters = r.Parameters;
                        Debug.WriteLine(
                            "Dialog returned OK with message: "
                                + parameters.GetValue<string>("Message")
                        );
                    }
                }
            );
        }

        private void OpenViewAAction()
        {
            _regionManager.RequestNavigate(
                "ContentRegion",
                "TempViewA",
                args =>
                {
                    _journal = args.Context.NavigationService.Journal;
                }
            );
        }

        private void OpenViewBAction()
        {
            _regionManager.RequestNavigate(
                "ContentRegion",
                "TempViewB",
                args =>
                {
                    _journal = args.Context.NavigationService.Journal;
                }
            );
        }

        private void GoBackAction()
        {
            if (_journal != null && _journal.CanGoBack)
            {
                _journal.GoBack();
            }
        }

        private void GoForwardAction()
        {
            if (_journal != null && _journal.CanGoForward)
            {
                _journal.GoForward();
            }
        }

        public MainWindowViewModel(
            IRegionManager regionManager,
            IModuleCatalog moduleCatalog,
            IDialogService dialogService,
            IEventAggregator eventAggregator,
            IUser user,
            ILogger logger
        )
        {
            logger.LogInformation("Random Informatiom.");

            _regionManager = regionManager;
            _moduleCatalog = moduleCatalog;
            _dialogService = dialogService;
            _eventAggregator = eventAggregator;
            _user = user;

            CompositeCommand.RegisterCommand(OpenViewA);
            CompositeCommand.RegisterCommand(ShowDialogue);

            //_regionManager.RegisterViewWithRegion("ContentRegion", typeof(Views.PrismUserControl1));
            //_regionManager.RegisterViewWithRegion(
            //    "StackPanelRegion",
            //    typeof(Views.PrismUserControl1)
            //);
            //_regionManager.RegisterViewWithRegion(
            //    "StackPanelRegion",
            //    typeof(Views.PrismUserControl1)
            //);
        }

        public void InitModules()
        {
            var dirModuleCatalog = _moduleCatalog as DirectoryModuleCatalog;
            foreach (var moduleInfo in dirModuleCatalog.Modules)
            {
                switch (moduleInfo.ModuleName)
                {
                    case "Contact":
                        _modules.Add(
                            new MainModel
                            {
                                Name = moduleInfo.ModuleName,
                                DisplayName = "联系人",
                                IconPath = "contact_icon.png"
                            }
                        );
                        break;
                    case "Schedule":
                        _modules.Add(
                            new MainModel
                            {
                                Name = moduleInfo.ModuleName,
                                DisplayName = "日程",
                                IconPath = "schedule_icon.png"
                            }
                        );
                        break;
                    default:
                        break;
                }
            }
        }

        private void Navigate(MainModel mainModel)
        {
            var parameter = new NavigationParameters();
            parameter.Add("Contact", "Hello from MainWindowViewModel");
            _regionManager.RequestNavigate("ContentRegion", $"{mainModel.Name}View", parameter);
        }
    }
}
