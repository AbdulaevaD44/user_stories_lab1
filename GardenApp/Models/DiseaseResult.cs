namespace GardenApp.Models
{
    public class DiseaseResult
    {
        public string PlantName { get; set; } = string.Empty;
        public string DetectedDisease { get; set; } = string.Empty;
        public string TreatmentAdvice { get; set; } = string.Empty;
        public string PhotoUrl { get; set; } = string.Empty;   // для истории #3 (диагностика по фото)
    }
}