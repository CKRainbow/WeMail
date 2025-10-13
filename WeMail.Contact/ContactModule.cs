using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using WeMail.Contact.Views;

namespace WeMail.Contact
{
    [Module(ModuleName = "Contact")]
    public class ContactModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            var contentRegion = regionManager.Regions["ContentRegion"];
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ContactView>();
        }
    }
}
