using System.Windows.Controls;
using Prism.Regions;

namespace WeMail.Common.RegionAdapters
{
    public class StackPanelRegionAdapter : RegionAdapterBase<StackPanel>
    {
        public StackPanelRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory)
            : base(regionBehaviorFactory) { }

        protected override void Adapt(IRegion region, StackPanel regionTarget)
        {
            region.Views.CollectionChanged += (s, e) =>
            {
                // 当有新的View注册进Region后，应该通过什么方式将其加入StackPanel中
                if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
                {
                    //regionTarget.Children.Clear();
                    foreach (var item in e.NewItems)
                    {
                        if (item is System.Windows.UIElement element)
                        {
                            regionTarget.Children.Add(element);
                        }
                    }
                }
            };
        }

        protected override IRegion CreateRegion()
        {
            return new Region();
        }
    }
}
