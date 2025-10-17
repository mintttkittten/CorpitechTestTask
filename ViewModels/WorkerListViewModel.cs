using ClosedXML.Excel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services;

namespace TestTask.ViewModels
{
    public partial class WorkerListViewModel : ViewModelBase
    {
        private readonly WorkerRepository _workerRepository;

        [ObservableProperty]
        private ObservableCollection<WorkerViewModel> _workers;

        [ObservableProperty]
        private WorkerViewModel? _selectedWorker;

        [ObservableProperty]
        private WorkerViewModel? _workerToEdit;

        [ObservableProperty]
        private bool _isEditingWorker = false;
        private readonly IFileService _fileService;

        public WorkerListViewModel(IFileService fileService)
        {
            _fileService = fileService;
            _workerRepository = new WorkerRepository();
            _workers = new ObservableCollection<WorkerViewModel>();
            LoadWorkersCommand.Execute(null);
        }

        [RelayCommand]
        private async Task LoadWorkers()
        {
            var workers = await _workerRepository.GetAllWorkersAsync();
            Workers = new ObservableCollection<WorkerViewModel>(workers.Select(w => new WorkerViewModel(w)));
        }

        [RelayCommand]
        private void AddNewWorker()
        {
            WorkerToEdit = new WorkerViewModel(new Worker());
            IsEditingWorker = true;
        }

        [RelayCommand]
        private void EditWorker(WorkerViewModel worker)
        {
            var workerCopy = new Worker
            {
                Id = worker.Id,
                Name = worker.Name,
                Surname = worker.Surname,
                Lastname = worker.Lastname,
                Role = worker.Role,
                Salary = worker.Salary
            };
            WorkerToEdit = new WorkerViewModel(workerCopy);
            IsEditingWorker = true;
        }

        [RelayCommand]
        private async Task SaveWorker()
        {
            if (WorkerToEdit == null || WorkerToEdit.HasErrors)
            {
                Console.WriteLine("Worker has validation errors or is null.");
                return;
            }

            var worker = WorkerToEdit.GetWorker();

            if (worker.Id == 0)
            {
                await _workerRepository.AddWorkerAsync(worker);
                Workers.Add(new WorkerViewModel(worker));
            }
            else
            {
                await _workerRepository.UpdateWorkerAsync(worker);
                await LoadWorkers();
            }

            IsEditingWorker = false;
            WorkerToEdit = null;
        }

        [RelayCommand]
        private void CancelEdit()
        {
            IsEditingWorker = false;
            WorkerToEdit = null;
        }

        [RelayCommand]
        private async Task DeleteWorker(WorkerViewModel worker)
        {
            if (worker != null)
            {
                await _workerRepository.DeleteWorkerAsync(worker.Id);
                Workers.Remove(worker);
            }
        }

        [RelayCommand]
        private async Task ExportToExcel()
        {
            if (!Workers.Any())
            {
                Console.WriteLine("No workers to export.");
                return;
            }

            var filePath = await _fileService.SaveFileAsync("Workers", "xlsx");

            if (filePath == null)
            {
                return;
            }

            try
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Работники");

                    worksheet.Cell(1, 1).Value = "Идентификатор";
                    worksheet.Cell(1, 2).Value = "Имя";
                    worksheet.Cell(1, 3).Value = "Фамилия";
                    worksheet.Cell(1, 4).Value = "Отчество";
                    worksheet.Cell(1, 5).Value = "Должность";
                    worksheet.Cell(1, 6).Value = "Зарплата (BYN)";

                    for (int i = 0; i < Workers.Count; i++)
                    {
                        var worker = Workers[i];
                        worksheet.Cell(i + 2, 1).Value = worker.Id;
                        worksheet.Cell(i + 2, 2).Value = worker.Name;
                        worksheet.Cell(i + 2, 3).Value = worker.Surname;
                        worksheet.Cell(i + 2, 4).Value = worker.Lastname;
                        worksheet.Cell(i + 2, 5).Value = worker.Role.ToString();
                        worksheet.Cell(i + 2, 6).Value = worker.Salary;
                    }

                    worksheet.Columns().AdjustToContents();

                    workbook.SaveAs(filePath);
                    Console.WriteLine($"Workers exported to {filePath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error exporting to Excel: {ex.Message}");
            }
        }
    }
}
