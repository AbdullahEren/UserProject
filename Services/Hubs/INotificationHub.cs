namespace Services.Hubs
{
    public interface INotificationHub
    {
        Task ReciveNotify(string message);
    }
}
