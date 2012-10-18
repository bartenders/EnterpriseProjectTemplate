using EP.Infrastructure.Interfaces;
using EP.Modules.DevExpressModule.ViewModels;

namespace EP.Modules.DevExpressModule.NinjectModule
{
    public class RuntimeModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Bind<IShellModule>().To<DataGridViewModel>().InSingletonScope();
        }
    }
}
