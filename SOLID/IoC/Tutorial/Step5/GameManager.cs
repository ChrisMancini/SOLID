using System;
using SOLID.IoC.Tutorial.Step5.SpecificationPattern;

namespace SOLID.IoC.Tutorial.Step5
{
    public interface IGameManager
    {
        void LogPlay(long gameId, ISpecification<Game> notificationSpecification = null);
    }

    public class GameManager : IGameManager
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

        public void LogPlay(long gameId, ISpecification<Game> notificationSpecification = null)
        {
            // Get Game 
            Game game = GetGame(gameId);

            // Increase # of Plays
            UpdateGame(game);

            // Notify if specification is satisfied
            NotifyOwnersIfNecessary(game, notificationSpecification);
        }

        private Game GetGame(long playerId)
        {
            return GameRepository.Get(playerId);
        }

        private void UpdateGame(Game game)
        {
            Console.WriteLine($"Increasing Game Plays to {game.Plays + 1}");

            game.Plays++;

            // Update Record
            GameRepository.Save(game);
        }

        private void NotifyOwnersIfNecessary(Game game, ISpecification<Game> specification)
        {
            Console.WriteLine("Notifying Owner, if necessary");

            // Open/Closed Principle
            if (specification.IsSatisfiedBy(game))
            {
                var ownerEmail = OwnerRepository.GetEmailForOwnerOf(game.Id);

                Notifier.Notify(ownerEmail);
            }
        }
    }
}