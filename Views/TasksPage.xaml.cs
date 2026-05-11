using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using GardenApp.ViewModels;

namespace GardenApp.Views
{
    public partial class TasksPage : Page
    {
        private MainViewModel _viewModel;

        public TasksPage()
        {
            InitializeComponent();
            _viewModel = new MainViewModel();
            TasksList.ItemsSource = _viewModel.Tasks;
            NewTaskDate.SelectedDate = DateTime.Today;
            UpdateStats();
        }

        private void TaskStatusChanged(object sender, RoutedEventArgs e)
        {
            var checkBox = sender as CheckBox;
            var task = checkBox?.Tag as TaskViewModel;
            if (task != null)
            {
                _viewModel.ToggleTask(task);
                UpdateStats();
            }
        }

        private void AddTaskBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NewTaskDesc.Text))
            {
                MessageBox.Show("Введите описание задачи!", 
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (NewTaskDate.SelectedDate == null)
            {
                MessageBox.Show("Выберите дату выполнения!", 
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _viewModel.AddTask(NewTaskDesc.Text, NewTaskDate.SelectedDate.Value);
            NewTaskDesc.Clear();
            UpdateStats();
            
            // Обновляем список
            TasksList.ItemsSource = null;
            TasksList.ItemsSource = _viewModel.Tasks;
            
            MessageBox.Show("Задача добавлена!", 
                "Готово", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void UpdateStats()
        {
            int total = _viewModel.Tasks.Count;
            int completed = _viewModel.Tasks.Count(t => t.IsCompleted);
            StatsText.Text = $"📊 Статистика: выполнено {completed} из {total} задач";
        }
    }
}