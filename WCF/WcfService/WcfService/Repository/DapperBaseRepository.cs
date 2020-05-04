using Dapper;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WcfService.Repository
{
    public abstract class DapperBaseRepository
    {
        private static readonly ConnectionStringSettings ConnectionSet = ConfigurationManager.ConnectionStrings["DBConnect"];
        private IDbConnection DBConnection;

        protected DapperBaseRepository(IDbConnection objConnect = null)
        {
            if (null != objConnect)
            {
                DBConnection = objConnect;
            }
        }

        protected IDbConnection GetDBConnection()
        {
            if (string.IsNullOrWhiteSpace(ConnectionSet.ConnectionString))
            {
                throw new Exception("ConnectionString 必須先在 Web.config 設定!");
            }

            if (null == DBConnection || string.IsNullOrWhiteSpace(DBConnection.ConnectionString))
            {
                DBConnection = new SqlConnection(ConnectionSet.ConnectionString);
                SimpleCRUD.SetDialect(SimpleCRUD.Dialect.SQLServer);

                DBConnection.Open();
            }

            return DBConnection;
        }
    }
}