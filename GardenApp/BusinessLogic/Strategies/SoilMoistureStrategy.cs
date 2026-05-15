using System;
using GardenApp.ViewModels;

namespace GardenApp.BusinessLogic.Strategies
{
    public class SoilMoistureStrategy : IWateringStrategy
    {
        private readonly Random _random = new Random();

        public bool IsWateringNeeded(PlantViewModel plant)
        {
            // Имитация датчика влажности: случайное значение от 0 до 100
            int moisture = _random.Next(0, 100);
            // Если влажность ниже 30% – поливать
            return moisture < 30;
        }

        public string GetAdvice(PlantViewModel plant)
        {
            int moisture = _random.Next(0, 100);
            if (moisture < 30)
                return $"{plant.Name}: влажность почвы {moisture}% – требуется полив!";
            else
                return $"{plant.Name}: влажность {moisture}% – полив не нужен.";
        }
    }
}