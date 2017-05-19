using System;

namespace SOLID.IoC.Tutorial.Step3
{
    public class EmailNotifier 
    {
        public void Notify(string notificationInfo)
        {
            Console.WriteLine("Sending email to {0}", notificationInfo);
        }
    }
}
