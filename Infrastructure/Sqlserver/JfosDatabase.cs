using Microsoft.Data.SqlClient;
using Domain.StaticValues;

namespace Infrastructure.Sqlserver;

public class JfosDatabase : Database
{
    public JfosDatabase()
    {
        var builder = new SqlConnectionStringBuilder()
        {
            DataSource             = DbConfig.DbHost,
            InitialCatalog         = DbConfig.DbName,
            UserID                 = DbConfig.DbUser,
            Password               = DbConfig.DbPass,
            IntegratedSecurity     = false,
            TrustServerCertificate = true,
        };
        Connection.ConnectionString = builder.ConnectionString;
    }
}