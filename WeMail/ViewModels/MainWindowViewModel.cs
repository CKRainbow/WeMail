using System.Collections.ObjectModel;
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

        private DelegateCommand _loadModules;

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
            set => SetProperty(ref _moduleInfo, value);
        }

        public DelegateCommand LoadModules
        {
            get => _loadModules = new DelegateCommand(InitModules);
        }

        public MainWindowViewModel(IRegionManager regionManager, IModuleCatalog moduleCatalog)
        {
            _regionManager = regionManager;
            _moduleCatalog = moduleCatalog;

            _regionManager.RegisterViewWithRegion("ContentRegion", typeof(Views.PrismUserControl1));
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
    }
}
