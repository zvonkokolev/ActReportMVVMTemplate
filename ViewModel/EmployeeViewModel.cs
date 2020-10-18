using ActReport.Core.Entities;
using ActReport.Persistence;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace ViewModel
{
    public class EmployeeViewModel : ViewModelBase
    {
        private string _firstName;
        private string _lastName;
        private Employee _selectedEmployee;
        private Employee _newEmployee = new Employee();
        private ObservableCollection<Employee> _employees;
        private object _cmdSaveChanges;
        private object _cmdAddNewEmp;

        public EmployeeViewModel()
        {
            LoadEmployees();
        }

        private void LoadEmployees()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var employees = uow.EmployeeRepository
                    .Get(orderBy: coll => coll.OrderBy(emp => emp.LastName))
                    .ToList();

                Employees = new ObservableCollection<Employee>(employees);
            }
        }

        public string FirstName 
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        public Employee SelectedEmployee
        {
            get => _selectedEmployee;
            set
            {
                _selectedEmployee = value;
                FirstName = _selectedEmployee?.FirstName;
                LastName = _selectedEmployee?.LastName;
                OnPropertyChanged(nameof(SelectedEmployee));
            }
        }

        public Employee NewEmployee
        {
            get => _newEmployee;
            set
            {
                _newEmployee = value;
                FirstName = _newEmployee?.FirstName;
                LastName = _newEmployee?.LastName;
                OnPropertyChanged(nameof(NewEmployee));
            }
        }

        public ObservableCollection<Employee> Employees 
        {
            get => _employees;
            set
            {
                _employees = value;
                OnPropertyChanged(nameof(Employees));
            }
        }

        // Commands
        public ICommand CmdSaveChanges 
        {
            get
            {
                if(_cmdSaveChanges == null)
                {
                    _cmdSaveChanges = new RelayCommand(
                        execute: _ =>
                        {
                            using UnitOfWork uow = new UnitOfWork();

                            _selectedEmployee.FirstName = _firstName;
                            _selectedEmployee.LastName = _lastName;

                            uow.EmployeeRepository.Update(_selectedEmployee);
                            uow.Save();

                            LoadEmployees();
                        },
                        canExecute: _ => _selectedEmployee != null && _selectedEmployee.LastName.Length >= 3);
                }

                return (ICommand)_cmdSaveChanges;
            }
            set
            {
                _cmdSaveChanges = value;
            }
        }

        public ICommand CmdAddNewEmp
        {
            get 
            {
                if (_cmdAddNewEmp == null)
                {
                    _cmdAddNewEmp = new RelayCommand(
                        execute: _ =>
                        {
                            using UnitOfWork uow = new UnitOfWork();

                            _newEmployee.FirstName = _firstName;
                            _newEmployee.LastName = _lastName;

                            uow.EmployeeRepository.Insert(_newEmployee);
                            uow.Save();

                            LoadEmployees();
                        },
                        canExecute: _ => _newEmployee != null);
                }

                return (ICommand)_cmdAddNewEmp;
            }
            set
            {
                _cmdAddNewEmp = value;
            }
        }
    }
}
