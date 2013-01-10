using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Caliburn.Micro;
using System.Linq;
using EPT.GUI.Helpers;
using EPT.Infrastructure.API;
using MahApps.Metro;


namespace EPT.Shell.ViewModels
{
    public class ShellViewModel : Conductor<IShellModule>.Collection.OneActive, IShell
    {
        private readonly SettingsViewModel _settingsView;
        private readonly AboutViewModel _aboutView;

        public ShellViewModel(AboutViewModel aboutView, SettingsViewModel settingsView)
        {
            _aboutView = aboutView;
            _settingsView = settingsView;

            DisplayName = "Enterprise Project Template";

            var shellModules = DesignerProperties.GetIsInDesignMode(new DependencyObject()) ? GetDesignTimeModules() : IoC.GetAllInstances(typeof(IShellModule)).Cast<IShellModule>().Where(m => m.ActiveMenuEntry).OrderBy(x => x.OrderPriority).ToList();

            ThemeManager.ChangeTheme(Application.Current, ThemeManager.DefaultAccents.FirstOrDefault(a => a.Name == "Blue"), Theme.Light);

            ScreenExtensions.TryActivate(this);

            Items.AddRange(shellModules);

            if (Items.Any())
                ActiveItem = Items.First();
            
        }

        public void LoadSettings()
        {
            ActiveItem = _settingsView;
        }

        public void ShowAbout()
        {
            ActiveItem = _aboutView;
        }

        public void ShowFlyouts()
        {
            var metro = (MahApps.Metro.Controls.MetroWindow) Application.Current.MainWindow;

            metro.Flyouts[0].IsOpen = !metro.Flyouts[0].IsOpen;
            metro.Flyouts[1].IsOpen = !metro.Flyouts[1].IsOpen;
        }

        private static IList<IShellModule> GetDesignTimeModules()
        {
            var modules = new List<IShellModule>
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
        private bool _activeMenuEntry;


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

        public bool ActiveMenuEntry
        {
            get { return _activeMenuEntry; }
        }

        public string DisplayName
        {
            get { return _demoModule; }
        }
    }
}