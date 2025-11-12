using System.Security.Cryptography;

namespace ApiBackendParkingApp.Utilis
{
    public class DataBaseConfig : IDataBaseConfig
    {
        public DataBaseConfig(IConfiguration config)
        {
            DbServer = config.GetValue<string>("MSSQLCONNECTION:Server");
            DataBaseName = config.GetValue<string>("MSSQLCONNECTION:Database");
            DbTrustedConnection = config.GetValue<bool>("MSSQLCONNECTION:Trusted_Connection");
            DbTrustServerCertificate = config.GetValue<bool>("MSSQLCONNECTION:TrustServerCertificate");
        }

        public string DbServer { get;private set; }

        public string DataBaseName { get; private set; }

        public bool DbTrustedConnection { get; private set; }

        public bool DbTrustServerCertificate { get; private set; }
    }
    public interface IDataBaseConfig
    {
       string DbServer { get; }
        string DataBaseName { get; }
        bool DbTrustedConnection { get; }
        bool DbTrustServerCertificate { get; }
    }

}
