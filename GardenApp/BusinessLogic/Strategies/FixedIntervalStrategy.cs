using System;
using GardenApp.ViewModels;

namespace GardenApp.BusinessLogic.Strategies
{
    public class FixedIntervalStrategy : IWateringStrategy
    {
        public bool IsWateringNeeded(PlantViewModel plant)
        {
            return (DateTime.Now - plant.LastWateredDate).TotalDays >= plant.WateringIntervalDays;
        }

        public string GetAdvice(PlantViewModel plant)
        {
            var daysSince = (int)(DateTime.Now - plant.LastWateredDate).TotalDays;
            if (IsWateringNeeded(plant))
                return $"{plant.Name} нужно полить! (прошло {daysSince} дн.)";
            else
                return $"{plant.Name} полив через {plant.WateringIntervalDays - daysSince} дн.";
        }
    }
}