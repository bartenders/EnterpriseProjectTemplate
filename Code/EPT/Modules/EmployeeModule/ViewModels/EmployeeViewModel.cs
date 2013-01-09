using System.Windows.Controls;
using Caliburn.Micro;
using EPT.DAL.Northwind;
using EPT.GUI.Helpers;
using EPT.Infrastructure.Interfaces;
using EPT.Infrastructure.Messages;

namespace EPT.Modules.EmployeeModule.ViewModels
{
    public sealed class EmployeeViewModel : Conductor<EmployeeViewModel>.Collection.AllActive, IShellModule
    {
        private readonly IEventAggregator _aggregator;
        private readonly Repository _employeeRepository;
        private Employee _employee;
        private BindableCollection<Employee> _employees;
        private Employee _selectedEmployee;
        private readonly CustomerViewModel _customerViewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeViewModel" /> class.
        /// </summary>
        public EmployeeViewModel()
        {
           
        }

        public EmployeeViewModel(IEventAggregator aggregator, Repository employeeRepository, CustomerViewModel customerViewModel)
        {
            _aggregator = aggregator;
            _employeeRepository = employeeRepository;
            _customerViewModel = customerViewModel;

            _employee = new Employee()
            {
                Address = "Adresse"
            };
            DisplayName = "Employee Module";
        }

        public CustomerViewModel CustomerViewModel
        {
            get { return _customerViewModel; }
        }


        /// <summary>
        /// Called when activating.
        /// </summary>
        protected override void OnActivate()
        {
            //TODO BusyTicket ziehen für Async Operation
            Employees = new BindableCollection<Employee>(_employeeRepository.GetAllEmployees());
            base.OnActivate();
        }
        
        public BindableCollection<Employee> Employees
        {
            get { return _employees ?? (_employees = new BindableCollection<Employee>()); }
            set
            {
                if (value == _employees) return;
                _employees = value;
                NotifyOfPropertyChange(() => Employees);
            }
        }

        public Employee SelectedEmployee
        {
            get { return _selectedEmployee ?? (_selectedEmployee = new Employee()); }
            set
            {
                if (value == _selectedEmployee) return;
                _selectedEmployee = value;
                if (value != null)
                {
                    _aggregator.Publish(new EmployeeAddedMessage(value.ToString()));    
                }
                NotifyOfPropertyChange(() => SelectedEmployee);
            }
        }

        public Employee Employee
        {
            get { return _employee; }
            set
            {
                if (value == _employee) return;
                _employee = value;
                NotifyOfPropertyChange(() => Employee);
            }
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