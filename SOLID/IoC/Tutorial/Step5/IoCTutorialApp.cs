using Castle.Windsor;
using SOLID.IoC.Tutorial.Step5.SpecificationPattern;
using SOLID.IoC.Tutorial.Step5.Specifications;

namespace SOLID.IoC.Tutorial.Step5
{
    [Application(Order = 5, Name = "Dependency Injection with Windsor")]
    public class IoCTutorialApp : IApplication
    {
        private static readonly WindsorContainer WindsorContainer;

        static IoCTutorialApp()
        {
            WindsorContainer = new WindsorContainer();

            WindsorContainer.Install(new WindsorInstaller());
        }

        public void Start()
        {
            #region FACTORY RESOLUTION
            //var gameManagerFactory = WindsorContainer.Resolve<IGameManagerFactory>();
            //IGameManager gameManager = gameManagerFactory.Create();
            #endregion

            var gameManager = WindsorContainer.Resolve<IGameManager>();

            gameManager.LogPlay(2, NotificationSpecification);

            WindsorContainer.Release(gameManager);
        }

        private ISpecification<Game> NotificationSpecification
            => PlayedOften.And(CorrectNumberOfPlayers).And(TheFirstGameInMyCollection.Not());
        private ISpecification<Game> PlayedOften => new MinimumNumberOfPlaysSpecification(10);
        private ISpecification<Game> CorrectNumberOfPlayers => new NumberOfPlayersSpecifications(3);
        private ISpecification<Game> TheFirstGameInMyCollection => new IdSpecification(1);
    }
}
