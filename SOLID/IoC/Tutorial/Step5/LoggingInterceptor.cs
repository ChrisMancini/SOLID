using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Castle.DynamicProxy;

namespace SOLID.IoC.Tutorial.Step5
{
    public class LoggingInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            string methodCall =
                $"{invocation.TargetType.Name}.{invocation.Method}({string.Join(", ", invocation.Arguments.ToArray())})";

            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine($"+++ {methodCall}");

            Console.ResetColor();

            var stopWatch = new Stopwatch();

            stopWatch.Start();

            // Notable invocation Properties/Methods
            // invocation.ReturnValue - Get/Set the Return Value
            // invocation.SetArgumentValue(); - Get/Set a Parameter value.

            invocation.Proceed();

            stopWatch.Stop();

            Console.ForegroundColor = ConsoleColor.Yellow;

            var returnValue = DumpObject(invocation.ReturnValue);

            Console.WriteLine($"--- {methodCall} : {returnValue} - {stopWatch.Elapsed}");

            Console.ResetColor();
        }

        private static string DumpObject(object returnValue)
        {
            if (returnValue == null)
            {
                return "[NULL]";
            }

            Type objType = returnValue.GetType();

            if (objType == typeof(string) || objType.IsPrimitive || !objType.IsClass)
            {
                return returnValue.ToString();
            }

            var memoryStream = new MemoryStream();
            var serializer = new DataContractSerializer(objType);

            serializer.WriteObject(memoryStream, returnValue);

            return Encoding.UTF8.GetString(memoryStream.ToArray());
        }
    }
}
