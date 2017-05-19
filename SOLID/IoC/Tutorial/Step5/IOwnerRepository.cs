using System;
using System.Collections.Generic;
using System.Data.OleDb;

namespace SOLID.IoC.Tutorial.Step5
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

    #region CACHED REPOSITORY
    public class CachedOwnerRepository : IOwnerRepository
    {
        private readonly IDictionary<long, string> _cachedEmail;

        private IOwnerRepository OwnerRepository { get; set; }

        public CachedOwnerRepository(IOwnerRepository wrappedOwnerRepository)
        {
            OwnerRepository = wrappedOwnerRepository;
            _cachedEmail = new Dictionary<long, string>();
        }

        public string GetEmailForOwnerOf(long playerId)
        {
            Console.WriteLine("Looking in Cache for Email Address");
            if (_cachedEmail.ContainsKey(playerId))
            {
                Console.WriteLine("Found email in Cache.");
                return _cachedEmail[playerId];
            }

            var email = OwnerRepository.GetEmailForOwnerOf(playerId);

            _cachedEmail[playerId] = email;

            return email;
        }
    }
    #endregion
}
