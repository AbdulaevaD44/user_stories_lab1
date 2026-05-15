using System;

namespace GardenApp.Models
{
    public class Plant
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;      // где посажено (история #2)
        public DateTime PlantingDate { get; set; }                // дата посадки (история #4)
        public DateTime LastWateredDate { get; set; }             // последний полив (история #1)
        public int WateringIntervalDays { get; set; } = 2;        // поливать каждые N дней
    }
}