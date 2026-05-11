using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using GardenApp.ViewModels;

namespace GardenApp.Views
{
    public partial class DiagnosePage : Page
    {
        private MainViewModel _viewModel;
        private string _currentPhotoPath;

        public DiagnosePage()
        {
            InitializeComponent();
            _viewModel = new MainViewModel();
            PlantsCombo.ItemsSource = _viewModel.Plants;
            PlantsCombo.DisplayMemberPath = "Name";
        }

        private void LoadPhotoBtn_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "Изображения|*.jpg;*.jpeg;*.png";
            if (dialog.ShowDialog() == true)
            {
                _currentPhotoPath = dialog.FileName;
                PreviewImage.Source = new BitmapImage(new Uri(_currentPhotoPath));
            }
        }

        private void DiagnoseBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedPlant = PlantsCombo.SelectedItem as PlantViewModel;
            if (selectedPlant == null)
            {
                MessageBox.Show("Выберите растение для диагностики.", 
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _viewModel.DiagnosePlant(selectedPlant, _currentPhotoPath ?? "");
            ResultText.Text = _viewModel.DiagnosisResult;
        }
    }
}