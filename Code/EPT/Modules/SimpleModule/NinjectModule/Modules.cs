using EPT.Infrastructure.API;
using EPT.Modules.SearchModule.ViewModels;

namespace EPT.Modules.SearchModule.NinjectModule
{
    public class RuntimeModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Bind<IShellModule>().To<SearchViewModel>().InSingletonScope();
          }
    }
}
