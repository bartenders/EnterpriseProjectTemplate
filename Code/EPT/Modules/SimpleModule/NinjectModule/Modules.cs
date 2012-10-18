using EPT.Infrastructure.Interfaces;
using EPT.Modules.SimpleModule.ViewModels;

namespace EPT.Modules.SimpleModule.NinjectModule
{
    public class RuntimeModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Bind<IShellModule>().To<SimpleViewModel>().InSingletonScope();
          }
    }
}
