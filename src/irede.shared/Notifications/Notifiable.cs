namespace irede.shared.Notifications
{
    public abstract class Notifiable: INotifiable
    {

        public List<Notification> _notifications { get; private set; } = new List<Notification>();
        protected Notifiable() { _notifications = new List<Notification>(); }

        public IReadOnlyCollection<Notification> Notifications => _notifications;

        public void AddNotification(string message)
        {
            _notifications.Add(new Notification(message));
        }
        public void AddNotifications(IReadOnlyCollection<Notification> notifications)
        {
            _notifications.AddRange(notifications);
        }

        public bool IsValid() => _notifications.Count == 0;
    }

    public interface INotifiable
    {
        IReadOnlyCollection<Notification> Notifications { get; }
        void AddNotification(string message);
        void AddNotifications(IReadOnlyCollection<Notification> notifications);
        bool IsValid();



    }
}
