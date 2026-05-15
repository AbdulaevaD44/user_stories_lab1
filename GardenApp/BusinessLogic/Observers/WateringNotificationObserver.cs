using System;

namespace GardenApp.BusinessLogic.Observers
{
    public class WateringNotificationObserver : INotificationObserver
    {
        private readonly string _name;

        public WateringNotificationObserver(string name)
        {
            _name = name;
        }

        public void Update(string message)
        {
            // В реальном приложении здесь можно показать уведомление
            Console.WriteLine($"[{_name}] {message}");
        }
    }
}