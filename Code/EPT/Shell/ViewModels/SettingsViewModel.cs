using System.Windows.Controls;
using Caliburn.Micro;
using EPT.Infrastructure.Interfaces;

namespace EPT.Shell.ViewModels
{
    public class SettingsViewModel: Screen, IShellModule
    {
        private Image _icon;
        private int _orderPriority;

        public SettingsViewModel()
        {
            DisplayName = "Settings";
        }

        public Image Icon
        {
            get { return _icon; }
        }

        public int OrderPriority
        {
            get { return _orderPriority; }
        }

        public bool ActiveMenuEntry
        {
            get { return false; }
        }
    }
}