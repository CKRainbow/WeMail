using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using WeMail.Schedule.Views;

namespace WeMail.Schedule
{
    [Module(ModuleName = "Schedule")]
    public class ScheduleModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            var contentRegion = regionManager.Regions["ContentRegion"];
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ScheduleView>();
        }
    }
}
