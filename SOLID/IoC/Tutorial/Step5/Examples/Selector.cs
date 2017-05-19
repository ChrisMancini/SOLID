namespace SOLID.IoC.Tutorial.Step5.Examples
{
    //public class CustomSelector : ITypedFactoryComponentSelector
    //{
    //    public TypedFactoryComponent SelectComponent(MethodInfo method, Type type, object[] arguments)
    //    {
    //        if (CreatingAConnectionBroker(method))
    //        {
    //            return new ConnectionBrokerSelector().SelectComponent(method, type, arguments);
    //        }

    //        if (CreatingAnErrorMessageBuilder(method))
    //        {
    //            return new ErrorMessageBuilderFactorySelector().SelectComponent(method, type, arguments);
    //        }

    //        return new DefaultTypedFactoryComponentSelector().SelectComponent(method, type, arguments);
    //    }

    //    private static bool CreatingAnErrorMessageBuilder(MethodInfo method)
    //    {
    //        return typeof(IErrorMessageBuilder).IsAssignableFrom(method.ReturnType);
    //    }

    //    private static bool CreatingAConnectionBroker(MethodInfo method)
    //    {
    //        return typeof(IConnectionBroker).IsAssignableFrom(method.ReturnType);
    //    }
    //}

    //public class ConnectionBrokerSelector : ITypedFactoryComponentSelector
    //{
    //    public TypedFactoryComponent SelectComponent(MethodInfo method, Type type, object[] arguments)
    //    {
    //        if (arguments[0] is ConnectionBrokerContext)
    //        {
    //            var connectionBrokerContext = arguments[0] as ConnectionBrokerContext;

    //            if (connectionBrokerContext != null)
    //            {
    //                var additionalArguments = new Dictionary<string, object> { { "connectionBrokerContext", arguments[0] } };

    //                return new TypedFactoryComponent(connectionBrokerContext.Name, typeof(IConnectionBroker),
    //                                                 additionalArguments);
    //            }
    //        }

    //        return new DefaultTypedFactoryComponentSelector().SelectComponent(method, type, arguments);
    //    }
    //}

}
