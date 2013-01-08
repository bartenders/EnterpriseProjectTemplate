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
            DisplayName = "Employee Module";
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
     

        public void SendMessage()
        {
            var eventAggregator = IoC.Get<IEventAggregator>();
            eventAggregator.Publish(new EmployeeAddedMessage("Test Message send from EmployeeViewModel"));
        }


        public Image Icon
        {
            get { return ImageHelper.CreateImage(UriHelper.GetPackUri(@"\Images\Light\appbar.cone.diagonal.png"), 48); }
        }

        public int OrderPriority { get; private set; }

        public bool ActiveMenuEntry
        {
            get { return true; }
        }
    }
}