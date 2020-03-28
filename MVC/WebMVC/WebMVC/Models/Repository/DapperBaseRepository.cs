using Dapper;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WebMVC.Models.Repository
{
    public abstract class DapperBaseRepository
    {
        private static readonly ConnectionStringSettings ConnectionSet = ConfigurationManager.ConnectionStrings["DBConnect"];
        private IDbConnection Connection;

        protected DapperBaseRepository(IDbConnection gConnection = null)
        {
            if (gConnection != null)
            {
                Connection = gConnection;
            }
        }

        protected IDbConnection GetOpenConnection()
        {
            if (string.IsNullOrWhiteSpace(ConnectionSet.ConnectionString))
            {
                throw new Exception("ConnectionString 必須先在 Web.config 設定!");
            }

            if (null == Connection || string.IsNullOrWhiteSpace(Connection.ConnectionString))
            {
                Connection = new SqlConnection(ConnectionSet.ConnectionString);
                SimpleCRUD.SetDialect(SimpleCRUD.Dialect.SQLServer);

                Connection.Open();
            }

            return this.Connection;
        }
    }
}