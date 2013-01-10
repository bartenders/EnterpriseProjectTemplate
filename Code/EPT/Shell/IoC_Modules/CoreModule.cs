using Caliburn.Micro;
using EPT.Infrastructure.API;
using EPT.Shell.ViewModels;
using Ninject.Modules;

namespace EPT.Shell.IoC_Modules
{
    public class CoreModule : NinjectModule
    {
        public override void Load()
        {
            
            Bind<IEventAggregator>()
                .To<EventAggregator>()
                .InSingletonScope();
            Bind<IWindowManager>()
                .To<WindowManager>()
                .InSingletonScope();
            Bind<IShell>()
                .To<ShellViewModel>()
                .InSingletonScope();
            Bind<AboutViewModel>().ToSelf().InSingletonScope();
            Bind<SettingsViewModel>().ToSelf().InSingletonScope();
        }
    }
}