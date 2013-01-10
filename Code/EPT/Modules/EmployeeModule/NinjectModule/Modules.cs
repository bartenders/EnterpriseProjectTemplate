using EPT.DAL.Northwind;
using EPT.Infrastructure.API;
using EPT.Modules.MasterDataModule.ViewModels;

namespace EPT.Modules.MasterDataModule.NinjectModule
{
    public class RuntimeModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Bind<Repository>().ToSelf().InSingletonScope();
            Bind<IShellModule>().To<EmployeeViewModel>().InSingletonScope();
            Bind<IShellModule>().To<CustomerViewModel>().InSingletonScope();
            Bind<OrdersViewModel>().ToSelf().InSingletonScope();
          }
    }
}
