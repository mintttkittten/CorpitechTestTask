using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TestTask.Models;

namespace TestTask.ViewModels
{
    public partial class WorkerViewModel : ObservableValidator
    {
        private Worker _worker;

        public WorkerViewModel(Worker worker)
        {
            _worker = worker ?? throw new ArgumentNullException(nameof(worker));
            ValidateAllProperties();
        }

        public Worker GetWorker() => _worker;

        public int Id => _worker.Id;

        [Required(ErrorMessage = "Имя обязательно.")]
        [MinLength(2, ErrorMessage = "Имя должно содержать не менее 2 символов.")]
        [MaxLength(100, ErrorMessage = "Имя не может превышать 100 символов.")]
        public string Name
        {
            get => _worker.Name;
            set
            {
                if (SetProperty(_worker.Name, value, _worker, (w, v) => w.Name = v, true))
                {
                    ValidateProperty(value, nameof(Name));
                    OnPropertyChanged(nameof(NameError));
                }
            }
        }
        public string? NameError => HasErrors ? string.Join(Environment.NewLine, GetErrors(nameof(Name)).Select(e => e.ErrorMessage)) : null;


        [Required(ErrorMessage = "Фамилия обязательна.")]
        [MinLength(2, ErrorMessage = "Фамилия должна содержать не менее 2 символов.")]
        [MaxLength(100, ErrorMessage = "Фамилия не может превышать 100 символов.")]
        public string Surname
        {
            get => _worker.Surname;
            set
            {
                if (SetProperty(_worker.Surname, value, _worker, (w, v) => w.Surname = v, true))
                {
                    ValidateProperty(value, nameof(Surname));
                    OnPropertyChanged(nameof(SurnameError));
                }
            }
        }
        public string? SurnameError => HasErrors ? string.Join(Environment.NewLine, GetErrors(nameof(Surname)).Select(e => e.ErrorMessage)) : null;


        [MaxLength(100, ErrorMessage = "Отчество не может превышать 100 символов.")]
        public string? Lastname
        {
            get => _worker.Lastname;
            set
            {
                if (SetProperty(_worker.Lastname, value, _worker, (w, v) => w.Lastname = v, true))
                {
                    ValidateProperty(value, nameof(Lastname));
                    OnPropertyChanged(nameof(LastnameError));
                }
            }
        }
        public string? LastnameError => HasErrors ? string.Join(Environment.NewLine, GetErrors(nameof(Lastname)).Select(e => e.ErrorMessage)) : null;


        public WorkerRole Role
        {
            get => _worker.Role;
            set
            {
                if (SetProperty(_worker.Role, value, _worker, (w, v) => w.Role = v, true))
                {
                    ValidateProperty(value, nameof(Role));
                    OnPropertyChanged(nameof(RoleError));
                }
            }
        }
        public string? RoleError => HasErrors ? string.Join(Environment.NewLine, GetErrors(nameof(Role)).Select(e => e.ErrorMessage)) : null;


        [Required(ErrorMessage = "Зарплата обязательна.")]
        [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "Зарплата должна быть положительным значением.")]
        public decimal Salary
        {
            get => _worker.Salary;
            set
            {
                if (SetProperty(_worker.Salary, value, _worker, (w, v) => w.Salary = v, true))
                {
                    ValidateProperty(value, nameof(Salary));
                    OnPropertyChanged(nameof(SalaryError));
                }
            }
        }
        public string? SalaryError => HasErrors ? string.Join(Environment.NewLine, GetErrors(nameof(Salary)).Select(e => e.ErrorMessage)) : null;
    }
}
