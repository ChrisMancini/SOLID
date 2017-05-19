using System;
using System.Collections.Generic;
using System.Linq;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Application = System.Tuple<string, SOLID.IApplication>;

namespace SOLID
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var container = new WindsorContainer())
            {
                container.Register(Types.FromAssemblyContaining<IApplication>().BasedOn<IApplication>().Unless(t => t.IsAbstract).WithService.FirstInterface());

                List<Application> apps = GetApplications(container);

                Application app = null;

                do
                {
                    Console.WriteLine("============================================");
                    for (int index = 0; index < apps.Count; index++)
                    {
                        Console.WriteLine("{0} - {1}", index, apps[index].Item1);
                    }

                    var consoleKeyInfo = Console.ReadKey();

                    Console.WriteLine();

                    app = null;

                    if (char.IsDigit(consoleKeyInfo.KeyChar))
                    {
                        var index = char.GetNumericValue(consoleKeyInfo.KeyChar);

                        if (index < apps.Count)
                        {
                            app = apps[(int)index];
                        }
                    }

                    Console.WriteLine("============================================");
                    if (app != null)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("***{0}***", app.Item1);
                        Console.ResetColor();
                        app.Item2.Start();
                    }
                } while (app != null);

                Console.ReadKey();
            }
        }

        private static List<Application> GetApplications(WindsorContainer container)
        {
            var applications = container.ResolveAll<IApplication>();

            return (from application in applications
                    from attribute in application.GetType().GetCustomAttributes(typeof(ApplicationAttribute), false)
                    let appAttribute = attribute as ApplicationAttribute
                    where appAttribute.Available
                    orderby appAttribute.Order
                    select new Application(appAttribute.Name, application)).ToList();
        }
    }
}
