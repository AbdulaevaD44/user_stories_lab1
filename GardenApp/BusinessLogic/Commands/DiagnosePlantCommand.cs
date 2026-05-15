using GardenApp.ViewModels;

namespace GardenApp.BusinessLogic.Commands
{
    public class DiagnosePlantCommand : ICommand
    {
        private readonly MainViewModel _viewModel;
        private readonly PlantViewModel _plant;
        private readonly string _photoPath;

        public DiagnosePlantCommand(MainViewModel viewModel, PlantViewModel plant, string photoPath)
        {
            _viewModel = viewModel;
            _plant = plant;
            _photoPath = photoPath;
        }

        public void Execute()
        {
            _viewModel.DiagnosePlant(_plant, _photoPath);
        }

        public string Description => $"Диагностика: {_plant.Name}";
    }
}