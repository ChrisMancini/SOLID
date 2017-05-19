using System;
using System.IO;
using Castle.Core;
using Castle.Core.Internal;
using Castle.Windsor;

namespace SOLID.IoC.Tutorial.Step5
{
    public class DependencyGraphWriter
    {
        private readonly IWindsorContainer _container;
        private readonly TextWriter _writer;

        public DependencyGraphWriter(IWindsorContainer container, TextWriter writer)
        {
            _container = container;
            _writer = writer;
        }

        public void Output()
        {
            var graphNodes = _container.Kernel.GraphNodes;

            //foreach (var graphNode in graphNodes)
            //{
            //    if (graphNode.Dependers.Length != 0) continue;
            //    Console.WriteLine();
            //    WalkGraph(graphNode, 0);
            //}
        }

        private void WalkGraph(IVertex node, int level)
        {
            var componentModel = node as ComponentModel;
            if (componentModel != null)
            {
                //_writer.WriteLine("{0}{1} -> {2} Named: {3}",
                //                 new string('\t', level),
                //                 componentModel.Service.FullName,
                //                 componentModel.Implementation.FullName,
                //                 componentModel.Name);
            }

            if (level < 2)
            {
                foreach (var childNode in node.Adjacencies)
                {
                    WalkGraph(childNode, level + 1);
                }
            }
        }
    }
}