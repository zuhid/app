namespace Zuhid.Tools;

public interface INotificationService {
  Task<bool> Send(string to, string subject, string message);
}
