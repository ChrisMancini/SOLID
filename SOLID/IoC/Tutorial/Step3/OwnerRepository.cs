using System.Data.OleDb;

namespace SOLID.IoC.Tutorial.Step3
{
    public class OwnerRepository 
    {
        public string GetEmailForOwnerOf(long gameId)
        {
            using (var connection = new OleDbConnection(Constants.ConnectionString))
            {
                using (OleDbCommand sqlCommand = connection.CreateCommand())
                {
                    connection.Open();
                    sqlCommand.CommandText =
                        $"SELECT O.Email FROM GameOwner GOw INNER JOIN Owner O ON O.Id = GOw.OwnerId WHERE GOw.GameId = {gameId}";

                    string email = string.Empty;

                    using (OleDbDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader != null)
                        {
                            if (dataReader.HasRows)
                            {
                                dataReader.Read();

                                email = dataReader.GetString(0);
                            }
                        }
                    }

                    return email;
                }
            }
        }
    }
}
