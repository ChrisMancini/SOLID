using System;
using System.Data.OleDb;

namespace SOLID.IoC.Tutorial.Step1
{
    [Application(Order = 1, Name = "Monolithic")]
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
            var connectionString =
                @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\git\LunchAndLearn\SOLID\SOLID\IoC\Tutorial\BoardGames.accdb;Persist Security Info=False;";

            // Get Game
            Console.WriteLine($"Getting Game {gameId}");

            var connection = new OleDbConnection(connectionString);
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
          
            // Increase # of Plays

            if (game != null)
            {
                Console.WriteLine("Increasing Game Plays");
                game.Plays++;
            }

            // Update Record
            Console.WriteLine("Updating Game {0}", gameId);
            connection.Open();
            sqlCommand.CommandText = $"UPDATE Game SET Plays = {game.Plays} WHERE Id = {game.Id}";

            sqlCommand.ExecuteNonQuery();
            connection.Close();

            // Notify Owners if Plays is > 10
            Console.WriteLine("Notifying Owner, if necessary");
            if (game.Plays >= 10)
            {
                // Check for owners
                connection.Open();
                sqlCommand.CommandText =
                    $"SELECT O.Email FROM GameOwner GOw INNER JOIN Owner O ON O.Id = GOw.OwnerId WHERE GOw.GameId = {game.Id}";

                dataReader = sqlCommand.ExecuteReader();

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
