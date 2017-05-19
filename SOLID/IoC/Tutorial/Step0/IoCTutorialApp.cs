using System;

namespace SOLID.IoC.Tutorial.Step0
{
    [Application(Order = 0, Name = "Shell")]
    public class IoCTutorialApp : IApplication
    {
        public void Start()
        {
            var gameManager = new GameManager();
            gameManager.AddGamePlay(1);
        }
    }

    public class GameManager
    {
        public void AddGamePlay(long gameId)
        {
            // Get Game
            Console.WriteLine($"Getting Game {gameId}");
            // Increase # of Plays
            Console.WriteLine("Increasing Game Plays");
            // Update Record
            Console.WriteLine($"Updating Game '{gameId}'");
            // Notify Owners if Specification is satisfied
            Console.WriteLine("Notifying Owner, if necessary");
        }
    }
}
