using System;
using System.Collections.Generic;

namespace SOLID.IoC.Tutorial.Step5
{
    public interface INotifier
    {
        void Notify(string notificationInfo);
    }

    // Interface Segregation Principle 
    public interface IEMailNotifier : INotifier
    {
        string Subject { get; set; }
    }

    public class EmailNotifier : IEMailNotifier
    {
        public EmailNotifier()
        {
            Subject = "Your game is very popular";
        }

        public void Notify(string notificationInfo)
        {
            Console.WriteLine($"Sending email to {notificationInfo} with Subject '{Subject}'" );
        }

        public string Subject { get; set; }
    }

    public class SmsNotifier : INotifier
    {
        public void Notify(string notificationInfo)
        {
            Console.WriteLine($"Sending SMS to {notificationInfo}");
        }
    }

    public class CompositeNotifier : INotifier
    {
        private readonly IList<INotifier> _notifiers;

        public CompositeNotifier(IList<INotifier> notifiers)
        {
            _notifiers = notifiers;
        }

        public void Notify(string notificationInfo)
        {
            foreach (var notifier in _notifiers)
            {
                notifier.Notify(notificationInfo);
            }
        }
    }
}
