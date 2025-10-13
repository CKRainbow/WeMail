using System.Windows;
using System.Windows.Controls;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using WeMail.Common.RegionAdapter;
using WeMail.Views;

namespace WeMail
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<TempViewA>();
            containerRegistry.RegisterForNavigation<TempViewB>();
        }

        protected override void ConfigureRegionAdapterMappings(
            RegionAdapterMappings regionAdapterMappings
        )
        {
            base.ConfigureRegionAdapterMappings(regionAdapterMappings);

            regionAdapterMappings.RegisterMapping(
                typeof(StackPanel),
                Container.Resolve<StackPanelRegionAdapter>()
            );
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new DirectoryModuleCatalog() { ModulePath = @".\Apps" };
        }
    }
}
