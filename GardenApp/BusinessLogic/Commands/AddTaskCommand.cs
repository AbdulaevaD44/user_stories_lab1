using System;
using GardenApp.ViewModels;

namespace GardenApp.BusinessLogic.Commands
{
    public class AddTaskCommand : ICommand
    {
        private readonly MainViewModel _viewModel;
        private readonly string _description;
        private readonly DateTime _dueDate;
        private readonly int? _plantId;

        public AddTaskCommand(MainViewModel viewModel, string description, DateTime dueDate, int? plantId = null)
        {
            _viewModel = viewModel;
            _description = description;
            _dueDate = dueDate;
            _plantId = plantId;
        }

        public void Execute()
        {
            _viewModel.AddTask(_description, _dueDate, _plantId);
        }

        public string Description => $"Добавлена задача: {_description}";
    }
}