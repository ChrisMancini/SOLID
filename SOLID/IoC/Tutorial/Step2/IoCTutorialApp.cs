using System;
using System.Data.OleDb;

namespace SOLID.IoC.Tutorial.Step2
{
    [Application(Order = 2, Name = "Extract Method")]
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
        const string ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\git\LunchAndLearn\SOLID\SOLID\IoC\Tutorial\BoardGames.accdb;Persist Security Info=False;";

        public void AddGamePlay(long gameId)
        {
            // Get Game 
            Game game = GetGame(gameId);

            // Increase # of Plays
            UpdateGame(game);

            // Notify Owners if Plays is > 10
            NotifyOwnersIfNecessary(game);
        }

        private Game GetGame(long gameId)
        {
            Console.WriteLine($"Getting Game {gameId}");

            var connection = new OleDbConnection(ConnectionString);
            var sqlCommand = connection.CreateCommand();

            // Don't do this...SQL Injection
            sqlCommand.CommandText = $"SELECT ID, GameName, Plays FROM Game WHERE ID = {gameId};";

            connection.Open();
            var dataReader = sqlCommand.ExecuteReader();

            Game game = null;

            if (dataReader != null)
            {
                if (dataReader.HasRows)
                {
                    dataReader.Read();

                    game = new Game
                                 {
                                     Id = dataReader.GetInt32(0),
                                     Name = dataReader.GetString(1),
                                     Plays = dataReader.GetInt32(2)
                                 };
                }
            }

            connection.Close();

            return game;
        }

        private void UpdateGame(Game game)
        {
            if (game == null)
            {
                return;
            }

            Console.WriteLine($"Increasing Game Plays to {game.Plays + 1}");

            game.Plays++;

            // Update Record
            Console.WriteLine($"Updating Game {game.Id}");

            var connection = new OleDbConnection(ConnectionString);
            var sqlCommand = connection.CreateCommand();
            connection.Open();
            sqlCommand.CommandText = $"UPDATE Game SET Plays = {game.Plays} WHERE Id = {game.Id}";

            sqlCommand.ExecuteNonQuery();
            connection.Close();
        }

        private void NotifyOwnersIfNecessary(Game game)
        {
            Console.WriteLine("Notifying Owner, if necessary");
            if (game.Plays >= 10)
            {
                var connection = new OleDbConnection(ConnectionString);
                var sqlCommand = connection.CreateCommand();

                // Check for owners
                connection.Open();
                sqlCommand.CommandText =
                    $"SELECT O.Email FROM GameOwner GOw INNER JOIN Owner O ON O.Id = GOw.OwnerId WHERE GOw.GameId = {game.Id}";

                var dataReader = sqlCommand.ExecuteReader();

                if (dataReader != null)
                {
                    if (dataReader.HasRows)
                    {
                        dataReader.Read();

                        string email = dataReader.GetString(0);

                        Console.WriteLine("Sending email to {0}", email);
                    }
                }

                connection.Close();
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
