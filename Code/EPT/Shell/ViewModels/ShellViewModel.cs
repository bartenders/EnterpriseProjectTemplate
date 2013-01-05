using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Caliburn.Micro;
using System.Linq;
using EPT.GUI.Helpers;
using EPT.Infrastructure.Interfaces;


namespace EPT.Shell.ViewModels
{
    public class ShellViewModel : Conductor<IShellModule>.Collection.OneActive, IShell
    {
        public ShellViewModel()
        {
            DisplayName = "Enterprise Project Template";

            var shellModules = DesignerProperties.GetIsInDesignMode(new DependencyObject()) ? GetDesignTimeModules() : IoC.GetAllInstances(typeof(IShellModule)).Cast<IShellModule>().OrderBy(x => x.OrderPriority).ToList();
            
            ScreenExtensions.TryActivate(this);

            Items.AddRange(shellModules);

            if (Items.Any())
                ActiveItem = Items.First();
            
        }

        private static IList<IShellModule> GetDesignTimeModules()
        {
            var modules = new List<IShellModule>()
                {
                    new DesignTimeModule(
                        ImageHelper.CreateImage(UriHelper.GetPackUri(@"\Images\Light\appbar.box.png"), 48), 1,
                        "Demo Module 1"),
                   new DesignTimeModule(
                        ImageHelper.CreateImage(UriHelper.GetPackUri(@"\Images\Light\appbar.alien.png"), 48), 1,
                        "Demo Module 2"),
                };
            return modules;
        }

    }

    internal class DesignTimeModule : IShellModule
    {
        private readonly Image _createImage;
        private readonly int _i;
        private readonly string _demoModule;
    

        public DesignTimeModule(Image createImage, int i, string demoModule)
        {
            _createImage = createImage;
            _i = i;
            _demoModule = demoModule;
        }

        public Image Icon
        {
            get { return _createImage; }
        }

        public int OrderPriority
        {
            get { return _i; }
        }

        public string DisplayName
        {
            get { return _demoModule; }
        }
    }
}