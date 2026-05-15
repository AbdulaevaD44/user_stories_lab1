using System.Linq;
using System.Windows;
using System.Windows.Controls;
using GardenApp.Models;
using GardenApp.ViewModels;

namespace GardenApp.Views
{
    public partial class PlantsPage : Page
    {
        private MainViewModel _viewModel;
        private PlantViewModel _selectedPlant;

        public PlantsPage()
        {
            InitializeComponent();
            _viewModel = new MainViewModel();
            LoadData();
        }

        private void LoadData()
        {
            PlantsList.ItemsSource = _viewModel.Plants;
            PlantsList.SelectionChanged += (s, e) =>
            {
                _selectedPlant = PlantsList.SelectedItem as PlantViewModel;
            };
            UpdateWateringReminders();
        }

        private void UpdateWateringReminders()
        {
            var thirsty = _viewModel.Plants.Where(p => p.IsWateringNeeded).ToList();
            WateringRemindersList.ItemsSource = thirsty.Select(p => 
                $"{p.Name} — не поливали {p.WateringStatus.Split(' ')[1]} дн.").ToList();
        }

        private void RefreshWateringBtn_Click(object sender, RoutedEventArgs e)
        {
            UpdateWateringReminders();
            MessageBox.Show("Напоминания обновлены!", "Готово", 
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void UpdateLocationBtn_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedPlant != null && !string.IsNullOrEmpty(NewLocationBox.Text))
            {
                _selectedPlant.UpdateLocation(NewLocationBox.Text);
                PlantsList.Items.Refresh();
                NewLocationBox.Clear();
                MessageBox.Show($"Местоположение {_selectedPlant.Name} обновлено!", 
                    "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (_selectedPlant == null)
            {
                MessageBox.Show("Сначала выберите растение из списка.", 
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}