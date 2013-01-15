using Caliburn.Micro;
using EPT.Infrastructure.API;
using EPT.Infrastructure.Framework;
using EPT.Shell.ViewModels;
using Ninject.Modules;

namespace EPT.Shell.IoC_Modules
{
    public class CoreModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IEventAggregator>().To<EventAggregator>().InSingletonScope();
            Bind<IWindowManager>().To<WindowManager>().InSingletonScope();
            Bind<IShell>().To<ShellViewModel>().InSingletonScope();
            Bind<IWindowCommand>().To<AboutViewModel>().InSingletonScope();
            Bind<IWindowCommand>().To<SettingsViewModel>().InSingletonScope();
            Bind<IBusyWatcher>().To<BusyWatcher>();
            Bind<BusyWatcher>().ToSelf().InSingletonScope().Named("Background");

            Bind<IDialogManager>().To<DialogConductorViewModel>().InSingletonScope();


            Bind<AboutViewModel>().ToSelf().InSingletonScope();
            Bind<SettingsViewModel>().ToSelf().InSingletonScope();
            Bind<ShellModuleViewModel>().ToSelf().InSingletonScope();
        }
    }
}