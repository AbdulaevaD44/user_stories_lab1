using System.Windows;
using System.Windows.Controls;
using GardenApp.ViewModels;

namespace GardenApp.Views
{
    public partial class CalendarPage : Page
    {
        private MainViewModel _viewModel;

        public CalendarPage()
        {
            InitializeComponent();
            _viewModel = new MainViewModel();
            LoadHarvestInfo();
        }

        private void LoadHarvestInfo()
        {
            var plantsWithInfo = new System.Collections.ObjectModel.ObservableCollection<PlantWithHarvest>();
            foreach (var plant in _viewModel.Plants)
            {
                plantsWithInfo.Add(new PlantWithHarvest
                {
                    Name = plant.Name,
                    PlantingDate = plant.PlantingDate,
                    Plant = plant
                });
            }
            HarvestList.ItemsSource = plantsWithInfo;
        }

        private void ShowHarvestInfo_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var plantWrapper = button?.Tag as PlantWithHarvest;
            if (plantWrapper != null)
            {
                string info = _viewModel.GetHarvestInfo(plantWrapper.Plant);
                SelectedInfoText.Text = info;
            }
        }
    }

    // Вспомогательный класс для отображения
    public class PlantWithHarvest
    {
        public string Name { get; set; } = string.Empty;
        public System.DateTime PlantingDate { get; set; }
        public PlantViewModel Plant { get; set; } = null!;
        public string HarvestInfo => "🌾 Сбор урожая";
    }
}