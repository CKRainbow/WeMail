using System;
using System.Collections.ObjectModel;
using System.Security.Policy;
using Prism.Commands;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;

namespace WeMail.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";

        private ObservableCollection<IModuleInfo> _modules;

        private readonly IRegionManager _regionManager; // manage the regions of the shell
        private readonly IModuleCatalog _moduleCatalog;
        private ModuleInfo _moduleInfo;

        private IRegionNavigationJournal _journal;

        private DelegateCommand _loadModules;
        private DelegateCommand _openViewA;
        private DelegateCommand _openViewB;
        private DelegateCommand _goBack;
        private DelegateCommand _goForward;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public ObservableCollection<IModuleInfo> Modules
        {
            get => _modules ??= new();
        }

        public ModuleInfo ModuleInfo
        {
            get => _moduleInfo;
            set
            {
                SetProperty(ref _moduleInfo, value);
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

        public MainWindowViewModel(IRegionManager regionManager, IModuleCatalog moduleCatalog)
        {
            _regionManager = regionManager;
            _moduleCatalog = moduleCatalog;

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
            Modules.AddRange(dirModuleCatalog.Modules);
        }

        private void Navigate(IModuleInfo moduleInfo)
        {
            var parameter = new NavigationParameters();
            parameter.Add("Contact", "Hello from MainWindowViewModel");
            _regionManager.RequestNavigate(
                "ContentRegion",
                $"{moduleInfo.ModuleName}View",
                parameter
            );
        }
    }
}
