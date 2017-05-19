using System;
using System.Data.OleDb;

namespace SOLID.IoC.Tutorial.Step5
{
    public interface IGameRepository
    {
        Game Get(long id);
        void Save(Game game);
    }

    public class GameRepository : IGameRepository
    {
        public Game Get(long id)
        {
            Console.WriteLine($"Getting Game {id}");

            Game game = null;

            using (var connection = new OleDbConnection(Constants.ConnectionString))
            {
                using (var sqlCommand = connection.CreateCommand())
                {
                    sqlCommand.CommandText = $"SELECT ID, GameName, MinNumberOfPlayers, MaxNumberOfPlayers, Plays FROM Game WHERE ID = {id};";

                    connection.Open();
                    using (OleDbDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader != null)
                        {
                            if (dataReader.HasRows)
                            {
                                dataReader.Read();

                                game = new Game
                                {
                                    Id = dataReader.GetInt32(0),
                                    Name = dataReader.GetString(1),
                                    MinNumberOfPlayers = dataReader.GetInt32(2),
                                    MaxNumberOfPlayers = dataReader.GetInt32(3),
                                    Plays = dataReader.GetInt32(4)
                                };
                            }
                        }

                    }
                }
            }

            return game;
        }

        public void Save(Game game)
        {
            Console.WriteLine("Updating Game {0}", game.Id);

            using (var connection = new OleDbConnection(Constants.ConnectionString))
            {
                using (var sqlCommand = connection.CreateCommand())
                {
                    connection.Open();
                    sqlCommand.CommandText = $"UPDATE Game SET Plays = {game.Plays} WHERE Id = {game.Id}";

                    sqlCommand.ExecuteNonQuery();
                }
            }
        }
    }
}