using System.Collections.Generic;

namespace GardenApp.BusinessLogic.Observers
{
    public class NotificationManager
    {
        private static NotificationManager _instance;
        private readonly List<INotificationObserver> _observers = new List<INotificationObserver>();

        private NotificationManager() { }

        public static NotificationManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new NotificationManager();
                return _instance;
            }
        }

        public void Subscribe(INotificationObserver observer)
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);
        }

        public void Unsubscribe(INotificationObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Notify(string message)
        {
            foreach (var observer in _observers)
            {
                observer.Update(message);
            }
        }
    }
}