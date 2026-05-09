using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using GardenApp.Models;

namespace GardenApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<PlantViewModel> Plants { get; set; }
        public ObservableCollection<TaskViewModel> Tasks { get; set; }

        private string _wateringSummary;
        public string WateringSummary
        {
            get => _wateringSummary;
            set
            {
                _wateringSummary = value;
                OnPropertyChanged();
            }
        }

        private string _diagnosisResult;
        public string DiagnosisResult
        {
            get => _diagnosisResult;
            set
            {
                _diagnosisResult = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            // Инициализация растений
            Plants = new ObservableCollection<PlantViewModel>
            {
                new PlantViewModel(new Plant { Id = 1, Name = "Помидоры", Location = "Теплица", PlantingDate = new DateTime(2025, 5, 1), LastWateredDate = DateTime.Now.AddDays(-3), WateringIntervalDays = 2 }),
                new PlantViewModel(new Plant { Id = 2, Name = "Огурцы", Location = "Грядка №3", PlantingDate = new DateTime(2025, 5, 10), LastWateredDate = DateTime.Now.AddDays(-1), WateringIntervalDays = 2 }),
                new PlantViewModel(new Plant { Id = 3, Name = "Клубника", Location = "Грядка №1", PlantingDate = new DateTime(2025, 4, 15), LastWateredDate = DateTime.Now.AddDays(-2), WateringIntervalDays = 2 })
            };

            // Инициализация задач
            Tasks = new ObservableCollection<TaskViewModel>
            {
                new TaskViewModel(new Task { Id = 1, Description = "Полить помидоры", DueDate = DateTime.Now.AddDays(1), IsCompleted = false, RelatedPlantId = 1 }),
                new TaskViewModel(new Task { Id = 2, Description = "Прополоть грядки", DueDate = DateTime.Now.AddDays(2), IsCompleted = false }),
                new TaskViewModel(new Task { Id = 3, Description = "Собрать клубнику", DueDate = DateTime.Now.AddDays(3), IsCompleted = false, RelatedPlantId = 3 })
            };

            // Подписка на изменения растений
            foreach (var plant in Plants)
            {
                plant.PropertyChanged += (s, e) =>
                {
                    if (e.PropertyName == nameof(PlantViewModel.LastWateredDate))
                        UpdateWateringSummary();
                };
            }

            // Подписка на изменения задач
            foreach (var task in Tasks)
            {
                task.PropertyChanged += (s, e) =>
                {
                    if (e.PropertyName == nameof(TaskViewModel.IsCompleted))
                        OnPropertyChanged(nameof(Tasks));
                };
            }

            UpdateWateringSummary();
        }

        private void UpdateWateringSummary()
        {
            var thirstyPlants = Plants.Where(p => p.IsWateringNeeded).ToList();
            if (thirstyPlants.Any())
            {
                WateringSummary = $"🌿 Требуют полива: {string.Join(", ", thirstyPlants.Select(p => p.Name))}";
            }
            else
            {
                WateringSummary = "✅ Все растения политы вовремя!";
            }
        }

        public void DiagnosePlant(PlantViewModel plant, string photoUrl)
        {
            var random = new Random();
            string[] diseases = { "Фитофтороз", "Мучнистая роса", "Тля", "Паутинный клещ" };
            string[] treatments = { 
                "Обработать бордоской жидкостью", 
                "Опрыскать раствором соды", 
                "Использовать мыльный раствор",
                "Применить акарициды"
            };
            int index = random.Next(diseases.Length);
            DiagnosisResult = $"🔬 {plant.Name}: обнаружен {diseases[index]}\n💊 Лечение: {treatments[index]}";
        }

        public string GetHarvestInfo(PlantViewModel plant)
        {
            var days = (DateTime.Now - plant.PlantingDate).Days;
            int daysToHarvest = plant.Name.Contains("Помидоры") ? 90 : 
                               plant.Name.Contains("Огурцы") ? 50 : 70;
            if (days >= daysToHarvest)
                return $"🎉 {plant.Name} готов к сбору! Прошло {days} дней.";
            return $"📅 {plant.Name} будет готов через {daysToHarvest - days} дней. Посажено: {plant.PlantingDate:dd.MM}";
        }

        public void AddTask(string description, DateTime dueDate, int? plantId = null)
        {
            var newId = Tasks.Count + 1;
            var newTask = new TaskViewModel(new Task 
            { 
                Id = newId, 
                Description = description, 
                DueDate = dueDate, 
                IsCompleted = false, 
                RelatedPlantId = plantId 
            });
            
            newTask.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(TaskViewModel.IsCompleted))
                    OnPropertyChanged(nameof(Tasks));
            };
            
            Tasks.Add(newTask);
        }

        public void ToggleTask(TaskViewModel task)
        {
            task.IsCompleted = !task.IsCompleted;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}