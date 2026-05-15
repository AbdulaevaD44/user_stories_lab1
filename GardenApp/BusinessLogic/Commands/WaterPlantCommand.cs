using GardenApp.ViewModels;

namespace GardenApp.BusinessLogic.Commands
{
    public class WaterPlantCommand : ICommand
    {
        private readonly PlantViewModel _plant;

        public WaterPlantCommand(PlantViewModel plant)
        {
            _plant = plant;
        }

        public void Execute()
        {
            _plant.Water(); // метод из PlantViewModel
        }

        public string Description => $"Полив: {_plant.Name}";
    }
}