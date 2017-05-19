using System;
using System.Diagnostics.Contracts;

//using System.Diagnostics.Contracts;

namespace SOLID.IoC.Tutorial.Step3
{
    [Application(Order = 3, Name = "Extract Class (SRP)")]
    public class IoCTutorialApp : IApplication
    {
        public void Start()
        {
            var playerManager = new GameManager();
            playerManager.LogPlay(1);
        }
    }

    public class GameManager
    {
        public GameManager()
        {
            GameRepository = new GameRepository();
            OwnerRepository = new OwnerRepository();
            EmailNotifier = new EmailNotifier();
        }

        protected EmailNotifier EmailNotifier { get; set; }
        protected OwnerRepository OwnerRepository { get; set; }
        protected GameRepository GameRepository { get; set; }

        public void LogPlay(long gameId)
        {
            // Get Game 
            var game = GetGame(gameId);

            // Increase # of Plays
            UpdateGame(game);

            // Notify Owners if Plays is > 10
            NotifyOwnersIfNecessary(game);
        }

        private Game GetGame(long gameId)
        {
            return GameRepository.Get(gameId);
        }

        private void UpdateGame(Game game)
        {
            Console.WriteLine($"Increasing Game Plays to {game.Plays + 1}");

            game.Plays++;

            // Update Record
            GameRepository.Save(game);
        }

        private void NotifyOwnersIfNecessary(Game game)
        {
            Console.WriteLine("Notifying Owner, if necessary");

            if (game.Plays >= 10)
            {
                var ownerEmail = OwnerRepository.GetEmailForOwnerOf(game.Id);

                EmailNotifier.Notify(ownerEmail);
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
