using System;

//using System.Diagnostics.Contracts;

namespace SOLID.IoC.Tutorial.Step4
{
    [Application(Order = 4, Name = "Interfaces and Dependency Injection")]
    public class IoCTutorialApp : IApplication
    {
        public void Start()
        {
            var gameManager = new GameManager(new GameRepository(), new OwnerRepository(), new EmailNotifier());
            gameManager.AddGamePlay(1);
        }
    }

    public class GameManager
    {
        public GameManager(IGameRepository gameRepository, IOwnerRepository ownerRepository, INotifier notifier)
        {
            GameRepository = gameRepository;
            OwnerRepository = ownerRepository;
            Notifier = notifier;
        }

        protected INotifier Notifier { get; set; }
        protected IOwnerRepository OwnerRepository { get; set; }
        protected IGameRepository GameRepository { get; set; }

        public void AddGamePlay(long gameId)
        {
            // Get Game 
            Game game = GetGame(gameId);

            // Increase # of Plays
            LogPlay(game);

            // Notify Owners if Plays is > 10
            NotifyOwnersIfNecessary(game);
        }

        private Game GetGame(long gameId)
        {
            //Contract.Requires(gameId > 0);

            return GameRepository.Get(gameId);
        }

        private void LogPlay(Game game)
        {
            //Contract.Requires(game != null);

            Console.WriteLine($"Increasing Game Plays to {game.Plays + 1}");

            game.Plays++;

            // Update Record
            GameRepository.Save(game);
        }

        private void NotifyOwnersIfNecessary(Game game)
        {
            Console.WriteLine("Notifying Owner, if necessary");

            // Closed for Modification...
            if (game.Plays >= 10)
            {
                var ownerEmail = OwnerRepository.GetEmailForOwnerOf(game.Id);

                Notifier.Notify(ownerEmail);
            }
        }
    }

    public class Game
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long Plays { get; set; }
    }
}
