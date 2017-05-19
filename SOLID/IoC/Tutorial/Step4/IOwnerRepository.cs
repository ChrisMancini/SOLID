using System.Data.OleDb;

namespace SOLID.IoC.Tutorial.Step4
{
    public interface IOwnerRepository
    {
        string GetEmailForOwnerOf(long playerId);
    }

    public class OwnerRepository : IOwnerRepository
    {
        #region IOwnerRepository Members

        public string GetEmailForOwnerOf(long playerId)
        {
            using (var connection = new OleDbConnection(Constants.ConnectionString))
            {
                using (OleDbCommand sqlCommand = connection.CreateCommand())
                {
                    connection.Open();
                    sqlCommand.CommandText =
                        $"SELECT O.Email FROM GameOwner GOw INNER JOIN Owner O ON O.Id = GOw.OwnerId WHERE GOw.GameId = {playerId}";

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

        #endregion
    }
}
