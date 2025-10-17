using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.Logging;
using NLog.Config;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using WeMail.Common.Helpers;
using WeMail.Common.RegionAdapters;
using WeMail.Views;

namespace WeMail
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private ILogger _logger;

        protected override Window CreateShell()
        {
            // UI 线程异常捕获
            DispatcherUnhandledException += App_DispatcherUnhandledException;
            // Task 线程异常捕获
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
            // 最后防线：多线程？
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            return Container.Resolve<MainWindow>();
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            _logger.LogCritical(
                "Unhandled exception: {StackTrace} {Message}",
                ex?.StackTrace,
                ex?.Message
            );

            MiniDump.TryDump("dumps", "WeMail");
        }

        private void TaskScheduler_UnobservedTaskException(
            object sender,
            UnobservedTaskExceptionEventArgs e
        )
        {
            _logger.LogCritical(
                "Unobserved task exception: {StackTrace} {Message}",
                e.Exception?.StackTrace,
                e.Exception?.Message
            );
        }

        private void App_DispatcherUnhandledException(
            object sender,
            System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e
        )
        {
            _logger.LogCritical(
                "Dispatcher unhandled exception: {StackTrace} {Message}",
                e.Exception?.StackTrace,
                e.Exception?.Message
            );
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<TempViewA>();
            containerRegistry.RegisterForNavigation<TempViewB>();

            var factory = new NLog.Extensions.Logging.NLogLoggerFactory();
            _logger = factory.CreateLogger("NLog");
            containerRegistry.RegisterInstance(_logger);
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
