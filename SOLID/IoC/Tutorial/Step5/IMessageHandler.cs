using System;
using Castle.Core;

namespace SOLID.IoC.Tutorial.Step5
{
    public interface IMessageHandler
    {
        void Receive(string message);
    }

    public class MessageHandler : IMessageHandler, IStartable
    {
        public void Start()
        {
            Console.WriteLine("Starting Message Handler");
        }

        public void Stop()
        {
            Console.WriteLine("Stopping Message Handler");
        }

        public void Receive(string message)
        {
            Console.WriteLine("Received Message: '{0}'", message);
        }
    }

    public class NonStartableMessageHandler : IMessageHandler
    {
        public void Start()
        {
            Console.WriteLine("Starting Message Handler (Not implementing IStartable)");
        }

        public void Stop()
        {
            Console.WriteLine("Stopping Message Handler (Not implementing IStartable)");
        }

        public void Receive(string message)
        {
            Console.WriteLine("Received Message: '{0}'", message);
        }
    }
}