using System.Windows;
using System.Windows.Controls;

namespace GardenApp.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Открываем главную страницу (полив) при запуске
            MainFrame.Navigate(new PlantsPage());
        }

        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PlantsPage());
        }

        private void PlantsBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PlantsPage());
        }

        private void DiagnoseBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new DiagnosePage());
        }

        private void CalendarBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new CalendarPage());
        }

        private void TasksBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new TasksPage());
        }
    }
}