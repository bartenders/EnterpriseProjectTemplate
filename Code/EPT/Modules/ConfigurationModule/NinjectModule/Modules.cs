using EP.Infrastructure.Interfaces;
using EP.Modules.ConfigurationModule.ViewModels;

namespace EP.Modules.ConfigurationModule.NinjectModule
{
    public class RuntimeModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Bind<IShellModule>().To<ConfigurationViewModel>().InSingletonScope();
          }
    }
}
