using EPT.DAL.Northwind;
using EPT.Infrastructure.Interfaces;
using EPT.Modules.EmployeeModule.ViewModels;

namespace EPT.Modules.EmployeeModule.NinjectModule
{
    public class RuntimeModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Bind<IShellModule>().To<EmployeeViewModel>().InSingletonScope();
            Bind<Repository>().ToSelf().InSingletonScope();
            Bind<CustomerViewModel>().ToSelf().InSingletonScope();
            Bind<OrdersViewModel>().ToSelf().InSingletonScope();
          }
    }
}
