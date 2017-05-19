using System;
using System.Data.OleDb;

namespace SOLID.IoC.Tutorial.Step4
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
            Console.WriteLine("Getting Game {0}", id);

            var connection = new OleDbConnection(Constants.ConnectionString);

            var sqlCommand = connection.CreateCommand();

            // Don't do this...SQL Injection
            sqlCommand.CommandText = $"SELECT ID, GameName, Plays FROM Game WHERE ID = {id};";

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

        public void Save(Game game)
        {
            Console.WriteLine("Updating Game {0}", game.Id);

            var connection = new OleDbConnection(Constants.ConnectionString);
            var sqlCommand = connection.CreateCommand();
            connection.Open();
            sqlCommand.CommandText = $"UPDATE Game SET Plays = {game.Plays} WHERE Id = {game.Id}";

            sqlCommand.ExecuteNonQuery();
            connection.Close();
        }
    }
}