using Caliburn.Micro;
using EPT.Infrastructure.API;
using EPT.Infrastructure.Messages;
using MahApps.Metro;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace EPT.Shell.ViewModels
{
    public class ShellViewModel : Conductor<IScreen>.Collection.OneActive, IShell, IHandle<ShowScreenMessage>
    {
        readonly ShellModuleViewModel _firstScreen;
        private readonly IEnumerable<IWindowCommand> _windowCommands;
        private readonly IEventAggregator _eventAggregator;
        private string _status;
        
   
        private IFlyout _flyouts;


        public ShellViewModel(ShellModuleViewModel firstScreen,
                IEnumerable<IWindowCommand> windowCommands,
                IEventAggregator eventAggregator
                )
        {
            _firstScreen = firstScreen;
            _windowCommands = windowCommands;
            _eventAggregator = eventAggregator;
   
            DisplayName = "Shell View - Enterprise Project Template";
            ThemeManager.ChangeTheme(Application.Current, ThemeManager.DefaultAccents.FirstOrDefault(a => a.Name == "Blue"), Theme.Light);
            eventAggregator.Subscribe(this);

            //ToDo, Antipattern, find a better solution
            //_backgroundBusyWatcher = (IBusyWatcher)IoC.GetInstance(typeof(IBusyWatcher), "Background");
        }

        public IBusyWatcher BackgroundBusyWatcher { get; private set; }

        public IEnumerable<IWindowCommand> WindowCommands
        {
            get { return _windowCommands; }
        }

        public void Back()
        {
            ActivateItem(_firstScreen);
        }

        protected override void OnInitialize()
        {
            ActivateItem(_firstScreen);
            base.OnInitialize();
        }

        public void Handle(ShowScreenMessage message)
        {
            ActivateItem(message.Screen ?? _firstScreen);
        }

        public void ExecuteWindowCommand(object dataContext)
        {
            var command = dataContext as IWindowCommand;
            if (command != null) command.CommandAction.Invoke();
        }



        public string StatusMessage
        {
            get { return _status; }
            set
            {
                if (value == _status) return;
                _status = value;
                NotifyOfPropertyChange(() => StatusMessage);
            }
        }

        public void ShowFlyouts()
        {
            var metro = (MahApps.Metro.Controls.MetroWindow)Application.Current.MainWindow;

            metro.Flyouts[0].IsOpen = !metro.Flyouts[0].IsOpen;
            metro.Flyouts[1].IsOpen = !metro.Flyouts[1].IsOpen;
        }


        public IFlyout Flyouts
        {
            get { return _flyouts; }
        }

        public void Log(string message)
        {
            throw new System.NotImplementedException();
        }
    }
}
