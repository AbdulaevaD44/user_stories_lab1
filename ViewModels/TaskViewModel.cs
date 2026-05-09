using System;
using System.ComponentModel;
using GardenApp.Models;

namespace GardenApp.ViewModels
{
    public class TaskViewModel : INotifyPropertyChanged
    {
        private readonly Task _model;

        public TaskViewModel(Task model)
        {
            _model = model;
        }

        public int Id => _model.Id;
        public string Description => _model.Description;
        public DateTime DueDate => _model.DueDate;
        
        public bool IsCompleted
        {
            get => _model.IsCompleted;
            set
            {
                _model.IsCompleted = value;
                OnPropertyChanged(nameof(IsCompleted));
                OnPropertyChanged(nameof(DisplayText));
            }
        }

        public string DisplayText => $"[{(IsCompleted ? "✓" : " ")}] {Description} (до {DueDate:dd.MM})";

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}