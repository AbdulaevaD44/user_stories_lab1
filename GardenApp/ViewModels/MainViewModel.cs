using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using GardenApp.Models;
using GardenApp.BusinessLogic.Commands;
using GardenApp.BusinessLogic.Strategies;
using GardenApp.BusinessLogic.Observers;

namespace GardenApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<PlantViewModel> Plants { get; set; }
        public ObservableCollection<TaskViewModel> Tasks { get; set; }

        // --- Паттерны: Strategy, Command, Observer ---
        private IWateringStrategy _wateringStrategy;
        private readonly Stack<ICommand> _commandHistory = new Stack<ICommand>();

        public IWateringStrategy WateringStrategy
        {
            get => _wateringStrategy;
            set 
            { 
                _wateringStrategy = value; 
                UpdateWateringSummary(); 
                OnPropertyChanged();
            }
        }

        public string CommandHistory => string.Join("\n", _commandHistory.Select(c => c.Description).Take(5));

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
            // Инициализация растений (как было)
            Plants = new ObservableCollection<PlantViewModel>
            {
                new PlantViewModel(new Plant { Id = 1, Name = "Помидоры", Location = "Теплица", PlantingDate = new DateTime(2025, 5, 1), LastWateredDate = DateTime.Now.AddDays(-3), WateringIntervalDays = 2 }),
                new PlantViewModel(new Plant { Id = 2, Name = "Огурцы", Location = "Грядка №3", PlantingDate = new DateTime(2025, 5, 10), LastWateredDate = DateTime.Now.AddDays(-1), WateringIntervalDays = 2 }),
                new PlantViewModel(new Plant { Id = 3, Name = "Клубника", Location = "Грядка №1", PlantingDate = new DateTime(2025, 4, 15), LastWateredDate = DateTime.Now.AddDays(-2), WateringIntervalDays = 2 }),
                new PlantViewModel(new Plant { Id = 4, Name = "Перец", Location = "Горшки на веранде", PlantingDate = new DateTime(2025, 4, 25), LastWateredDate = DateTime.Now.AddDays(-5), WateringIntervalDays = 3 })
            };

            // Инициализация задач (как было)
            Tasks = new ObservableCollection<TaskViewModel>
            {
                new TaskViewModel(new Task { Id = 1, Description = "Полить помидоры", DueDate = DateTime.Now.AddDays(1), IsCompleted = false, RelatedPlantId = 1 }),
                new TaskViewModel(new Task { Id = 2, Description = "Прополоть грядки", DueDate = DateTime.Now.AddDays(2), IsCompleted = false }),
                new TaskViewModel(new Task { Id = 3, Description = "Собрать клубнику", DueDate = DateTime.Now.AddDays(3), IsCompleted = false, RelatedPlantId = 3 }),
                new TaskViewModel(new Task { Id = 4, Description = "Удобрить перец", DueDate = DateTime.Now.AddDays(4), IsCompleted = false, RelatedPlantId = 4 })
            };

            // Подписка на изменения растений (как было)
            foreach (var plant in Plants)
            {
                plant.PropertyChanged += (s, e) =>
                {
                    if (e.PropertyName == nameof(PlantViewModel.LastWateredDate))
                        UpdateWateringSummary();
                };
            }

            // === НОВОЕ ДЛЯ 4-Й ЛАБЫ ===
            // Установка стратегии по умолчанию
            _wateringStrategy = new FixedIntervalStrategy();
            // Подписка на уведомления (Observer)
            NotificationManager.Instance.Subscribe(new WateringNotificationObserver("MainViewModel"));

            UpdateWateringSummary();
        }

        // Метод UpdateWateringSummary – ПЕРЕОПРЕДЕЛЁН (использует стратегию и оповещает Observer)
        private void UpdateWateringSummary()
        {
            var thirstyPlants = Plants.Where(p => _wateringStrategy.IsWateringNeeded(p)).ToList();
            if (thirstyPlants.Any())
            {
                WateringSummary = $"🌿 Требуют полива: {string.Join(", ", thirstyPlants.Select(p => p.Name))}";
                // Оповещаем всех подписчиков
                NotificationManager.Instance.Notify($"Требуют полива: {string.Join(", ", thirstyPlants.Select(p => p.Name))}");
            }
            else
            {
                WateringSummary = "✅ Все растения политы вовремя!";
            }
        }

        // Диагностика по фото (имитация) – без изменений
        public void DiagnosePlant(PlantViewModel plant, string photoUrl)
        {
            var random = new Random();
            string[] diseases = { "Фитофтороз", "Мучнистая роса", "Тля", "Паутинный клещ", "Ржавчина" };
            string[] treatments = { 
                "Обработать бордоской жидкостью", 
                "Опрыскать раствором соды", 
                "Использовать мыльный раствор",
                "Применить акарициды",
                "Удалить поражённые листья и обработать фунгицидом"
            };
            int index = random.Next(diseases.Length);
            DiagnosisResult = $"🔬 {plant.Name}: обнаружен {diseases[index]}\n💊 Лечение: {treatments[index]}";
        }

        // Информация об урожае – без изменений
        public string GetHarvestInfo(PlantViewModel plant)
        {
            var days = (DateTime.Now - plant.PlantingDate).Days;
            int daysToHarvest = plant.Name.Contains("Помидоры") ? 90 : 
                               plant.Name.Contains("Огурцы") ? 50 : 
                               plant.Name.Contains("Клубника") ? 60 : 70;
            if (days >= daysToHarvest)
                return $"🎉 {plant.Name} готов к сбору! Прошло {days} дней.";
            return $"📅 {plant.Name} будет готов через {daysToHarvest - days} дней. Посажено: {plant.PlantingDate:dd.MM}";
        }

        // Добавление задачи – без изменений
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

        // === НОВЫЕ МЕТОДЫ ДЛЯ ПАТТЕРНА COMMAND ===
        public void ExecuteCommand(ICommand command)
        {
            command.Execute();
            _commandHistory.Push(command);
            OnPropertyChanged(nameof(CommandHistory));
        }

        public void UndoLastCommand()
        {
            if (_commandHistory.Count > 0)
            {
                _commandHistory.Pop();
                OnPropertyChanged(nameof(CommandHistory));
            }
        }

        // === НОВЫЙ МЕТОД ДЛЯ СМЕНЫ СТРАТЕГИИ ===
        public void SetWateringStrategy(string strategyType)
        {
            switch (strategyType)
            {
                case "FixedInterval":
                    WateringStrategy = new FixedIntervalStrategy();
                    break;
                case "SoilMoisture":
                    WateringStrategy = new SoilMoistureStrategy();
                    break;
                default:
                    WateringStrategy = new FixedIntervalStrategy();
                    break;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}