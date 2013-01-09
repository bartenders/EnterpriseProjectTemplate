using Caliburn.Micro;
using EPT.Infrastructure.Interfaces;
using EPT.Shell.ViewModels;
using Ninject.Modules;

namespace EPT.Shell.NinjectModules
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