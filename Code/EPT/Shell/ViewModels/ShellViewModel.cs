using Caliburn.Micro;
using System.Linq;
using EPT.Infrastructure.Interfaces;

namespace EPT.Shell.ViewModels
{
    public class ShellViewModel : Conductor<IShellModule>.Collection.OneActive, IShell
    {
        public ShellViewModel()
        {
            var shellModules = IoC.GetAllInstances(typeof (IShellModule)).Cast<IShellModule>().OrderBy(x => x.OrderPriority);
            
            ScreenExtensions.TryActivate(this);
            Items.AddRange(shellModules);

            if (Items.Any())
                ActiveItem = Items.First();
        }
    }
}