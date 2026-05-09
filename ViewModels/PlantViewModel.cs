using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using GardenApp.Models;

namespace GardenApp.ViewModels
{
    public class PlantViewModel : INotifyPropertyChanged
    {
        private readonly Plant _model;

        public PlantViewModel(Plant model)
        {
            _model = model;
        }

        public int Id => _model.Id;
        public string Name => _model.Name;
        public string Location => _model.Location;
        public DateTime PlantingDate => _model.PlantingDate;
        public DateTime LastWateredDate => _model.LastWateredDate;
        public int WateringIntervalDays => _model.WateringIntervalDays;

        public bool IsWateringNeeded => (DateTime.Now - LastWateredDate).TotalDays >= WateringIntervalDays;

        public string WateringStatus => IsWateringNeeded 
            ? $"⚠️ Требуется полив! (не полито {DaysSinceLastWatering} дн.)" 
            : $"✅ Полито {DaysSinceLastWatering} дн. назад";

        private int DaysSinceLastWatering => (int)(DateTime.Now - LastWateredDate).TotalDays;

        public void UpdateLocation(string newLocation)
        {
            _model.Location = newLocation;
            OnPropertyChanged(nameof(Location));
        }

        public void UpdateLastWateredDate(DateTime date)
        {
            _model.LastWateredDate = date;
            OnPropertyChanged(nameof(LastWateredDate));
            OnPropertyChanged(nameof(IsWateringNeeded));
            OnPropertyChanged(nameof(WateringStatus));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}