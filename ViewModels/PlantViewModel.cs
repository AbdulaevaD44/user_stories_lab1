using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using GardenApp.Models;

namespace GardenApp.ViewModels
{
    public class PlantViewModel : INotifyPropertyChanged
    {
        private Plant _plant;

        public PlantViewModel(Plant plant)
        {
            _plant = plant;
        }

        public int Id => _plant.Id;
        
        public string Name
        {
            get => _plant.Name;
            set { _plant.Name = value; OnPropertyChanged(); }
        }
        
        public string Location
        {
            get => _plant.Location;
            set { _plant.Location = value; OnPropertyChanged(); }
        }
        
        public DateTime PlantingDate
        {
            get => _plant.PlantingDate;
            set { _plant.PlantingDate = value; OnPropertyChanged(); }
        }
        
        public DateTime LastWateredDate
        {
            get => _plant.LastWateredDate;
            set { _plant.LastWateredDate = value; OnPropertyChanged(); }
        }
        
        public int WateringIntervalDays
        {
            get => _plant.WateringIntervalDays;
            set { _plant.WateringIntervalDays = value; OnPropertyChanged(); }
        }
        
        // Вычисляемое свойство: нужно ли поливать
        public bool IsWateringNeeded => (DateTime.Now - LastWateredDate).TotalDays >= WateringIntervalDays;
        
        // Текстовый статус для отображения
        public string WateringStatus => IsWateringNeeded 
            ? $"⚠️ Требуется полив! (не полито {DaysSinceLastWatering} дн.)" 
            : $"✅ Полито {DaysSinceLastWatering} дн. назад";
        
        private int DaysSinceLastWatering => (int)(DateTime.Now - LastWateredDate).TotalDays;
        
        // Метод для обновления местоположения (история #2)
        public void UpdateLocation(string newLocation)
        {
            _plant.Location = newLocation;
            OnPropertyChanged(nameof(Location));
        }
        
        // Метод для обновления даты полива
        public void Water()
        {
            LastWateredDate = DateTime.Now;
            OnPropertyChanged(nameof(LastWateredDate));
            OnPropertyChanged(nameof(IsWateringNeeded));
            OnPropertyChanged(nameof(WateringStatus));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}