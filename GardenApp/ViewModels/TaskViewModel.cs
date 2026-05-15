using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using GardenApp.Models;

namespace GardenApp.ViewModels
{
    public class TaskViewModel : INotifyPropertyChanged
    {
        private Task _task;

        public TaskViewModel(Task task)
        {
            _task = task;
        }

        public int Id => _task.Id;
        
        public string Description
        {
            get => _task.Description;
            set { _task.Description = value; OnPropertyChanged(); }
        }
        
        public DateTime DueDate
        {
            get => _task.DueDate;
            set { _task.DueDate = value; OnPropertyChanged(); }
        }
        
        public bool IsCompleted
        {
            get => _task.IsCompleted;
            set { _task.IsCompleted = value; OnPropertyChanged(); }
        }
        
        public int? RelatedPlantId
        {
            get => _task.RelatedPlantId;
            set { _task.RelatedPlantId = value; OnPropertyChanged(); }
        }
        
        // Отображаемый текст
        public string DisplayText => $"[{(IsCompleted ? "✓" : " ")}] {Description} (до {DueDate:dd.MM})";

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}