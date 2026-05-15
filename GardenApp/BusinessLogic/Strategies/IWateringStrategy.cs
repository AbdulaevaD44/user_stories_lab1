using GardenApp.ViewModels;

namespace GardenApp.BusinessLogic.Strategies
{
    public interface IWateringStrategy
    {
        bool IsWateringNeeded(PlantViewModel plant);
        string GetAdvice(PlantViewModel plant);
    }
}