using System;

namespace SOLID.IoC.Tutorial.Step4
{
    public interface INotifier
    {
        void Notify(string notificationInfo);
    }

    public class EmailNotifier : INotifier
    {
        public void Notify(string notificationInfo)
        {
            Console.WriteLine("Sending email to {0}", notificationInfo);
        }
    }
}
