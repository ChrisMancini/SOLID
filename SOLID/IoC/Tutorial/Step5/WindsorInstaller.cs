using System;
using Castle.Core;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace SOLID.IoC.Tutorial.Step5
{
    public class WindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer windsorContainer, IConfigurationStore store)
        {
            windsorContainer.AddFacility<TypedFactoryFacility>();

            windsorContainer.Register(
                Component.For<IGameManagerFactory>().AsFactory(),
                Component.For<IGameManager>().ImplementedBy<GameManager>().LifeStyle.Transient,
                Component.For<IGameRepository>().ImplementedBy<GameRepository>().LifeStyle.Transient,
                Component.For<INotifier>().ImplementedBy<EmailNotifier>().LifeStyle.Transient//,
                //Component.For<INotifier>().ImplementedBy<SmsNotifier>().LifeStyle.Transient
                );

            windsorContainer.Register(
                Component.For<IOwnerRepository>().ImplementedBy<OwnerRepository>().LifeStyle.Transient);

            #region ALTERNATE REGISTRATIONS

            #region COMPOSITE PATTERN REGISTRATION
            //windsorContainer.Kernel.Resolver.AddSubResolver(new CollectionResolver(windsorContainer.Kernel));

            //windsorContainer.Register(
            //    Component.For<INotifier>().ImplementedBy<CompositeNotifier>().LifestyleTransient(),
            //    Component.For<INotifier>().ImplementedBy<EmailNotifier>().LifestyleTransient(),
            //    Component.For<INotifier>().ImplementedBy<SmsNotifier>().LifestyleTransient());
            #endregion

            #region DECORATOR PATTERN REGISTRATION

            //windsorContainer.Register(
            //    Component.For<IOwnerRepository>().ImplementedBy<CachedOwnerRepository>().LifestyleSingleton(),
            //    Component.For<IOwnerRepository>().ImplementedBy<OwnerRepository>().LifestyleSingleton());

            #endregion

            #region ALLTYPES REGISTRATION

            //windsorContainer.Register(
            //    Classes.FromAssemblyContaining<INotifier>().BasedOn<INotifier>().Configure(r =>
            //                                                                                    {
            //                                                                                        r.LifeStyle.Is(LifestyleType.Transient);

            //                                                                                        int indexOf = r.Implementation.Name.IndexOf("Notifier", StringComparison.Ordinal);

            //                                                                                        if (indexOf <= -1)
            //                                                                                            return;

            //                                                                                        r.Named(r.Implementation.Name.Substring(0, indexOf));
            //                                                                                    }).WithService.FromInterface());

            #endregion

            #region AOP REGISTRATION

            //windsorContainer.Register(Component.For<LoggingInterceptor>().LifeStyle.Transient,
            //                          Component.For<IGameManagerFactory>().AsFactory().LifeStyle.Transient.Interceptors(InterceptorReference.ForType<LoggingInterceptor>()).Anywhere,
            //                          Component.For<IGameManager>().ImplementedBy<GameManager>().LifeStyle.Transient.Interceptors(InterceptorReference.ForType<LoggingInterceptor>()).Anywhere,
            //                          Component.For<IGameRepository>().ImplementedBy<GameRepository>().LifeStyle.Transient.Interceptors(InterceptorReference.ForType<LoggingInterceptor>()).Anywhere,
            //                          Component.For<IOwnerRepository>().ImplementedBy<OwnerRepository>().LifeStyle.Transient.Interceptors(InterceptorReference.ForType<LoggingInterceptor>()).Anywhere,
            //                          Component.For<INotifier>().ImplementedBy<EmailNotifier>().LifeStyle.Transient.Interceptors(InterceptorReference.ForType<LoggingInterceptor>()).Anywhere);
            #endregion

            #endregion
        }
    }
}