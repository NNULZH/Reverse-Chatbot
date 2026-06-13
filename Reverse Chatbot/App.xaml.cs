using Prism.Ioc;
using Prism.Regions;
using Reverse_Chatbot.Interfaces;
using Reverse_Chatbot.Services;
using Reverse_Chatbot.Views;
using System.Windows;

namespace Reverse_Chatbot
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
            containerRegistry.RegisterForNavigation<ChatView>();

            containerRegistry.Register<IDeepseekApi,DeepseekApi>();
            containerRegistry.Register<IAgentService,AgentService>();
            containerRegistry.Register<IMemoryContextFactory, ContextFactory>();

            
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var RegionManager = Container.Resolve<Prism.Regions.IRegionManager>();

            RegionManager.RegisterViewWithRegion("ContentRegion", typeof(ChatView));
        }   
    }
}
