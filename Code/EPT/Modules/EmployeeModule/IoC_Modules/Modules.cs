using EPT.DAL.Northwind;
using EPT.Infrastructure.API;
using EPT.Modules.MasterDataModule.ViewModels;

namespace EPT.Modules.MasterDataModule.IoC_Modules
{
    public class RuntimeModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Bind<Repository>().ToSelf().InSingletonScope();
            Bind<IShellModule>().To<CustomersViewModel>().InSingletonScope();
            Bind<OrdersViewModel>().ToSelf().InSingletonScope();
        }
    }
}
