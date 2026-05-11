using System;

namespace GardenApp.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public int? RelatedPlantId { get; set; }   // к какому растению относится задача
    }
}