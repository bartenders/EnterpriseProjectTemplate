using System.Windows.Controls;
using System.Windows.Input;
using Caliburn.Micro;
using EPT.DAL.Northwind;
using EPT.GUI.Commands;
using EPT.GUI.Helpers;
using EPT.Infrastructure.Interfaces;
using EPT.Infrastructure.Messages;

namespace EPT.Modules.EmployeeModule.ViewModels
{
    public sealed class EmployeeViewModel : Screen, IShellModule
    {

        public EmployeeViewModel()
        {
            _Employee = new Employee()
            {
                Address = "Adresse"
            };
            DisplayName = "EmployeeViewModel RunTime Display Name";
        }

        private Employee _Employee;
        public Employee Employee
        {
            get { return _Employee; }
            set
            {
                if (value == _Employee) return;
                _Employee = value;
                NotifyOfPropertyChange(() => Employee);
            }
        }


        private ICommand _TestCommand;
        public ICommand TestCommand
        {
            get
            {
                return _TestCommand ?? (_TestCommand = new DelegateCommand<EmployeeViewModel>(
                                                     (x) =>
                                                         {
                                                             // Add Command Logic here

                                                         },
                                                     // Can Do Command Logic
                                                     (x) => true));
            }
        }   

        private ICommand _SendMessageCommand;
        public ICommand SendMessageCommand
        {
            get
            {
                return _SendMessageCommand ?? (_SendMessageCommand = new RelayCommand<EmployeeViewModel>(
                (x) =>
                    {
                        var eventAggregator = IoC.Get<IEventAggregator>();
                        eventAggregator.Publish(new MyTestMessage("Test Message send from EmployeeViewModel"));
                    },
                // Can Do Command Logic
                (x) => true));
            }
        }


        public Image Icon
        {
            get { return ImageHelper.CreateImage(UriHelper.GetPackUri(@"\Images\Light\appbar.cone.diagonal.png"), 48); }
        }

        public int OrderPriority { get; private set; }
    }
}