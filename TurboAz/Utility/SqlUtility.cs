using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurboAz.Utility
{
    class SqlUtility
    {
        private static SqlUtility sqlUtility;
        public string conString { get; set; }
        private SqlUtility()
        {
            conString = ConfigurationManager.ConnectionStrings["MainConString"].ConnectionString;
        }
        public static SqlUtility GetInstance()
        {
            if (sqlUtility==null)
            {
                sqlUtility = new SqlUtility();
            }
            return sqlUtility;
        }
        public DataTable GetDataWithAdapter(string _query)
        {
            SqlConnection sqlConnection = new SqlConnection(sqlUtility.conString);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(_query, sqlConnection);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
    }
}
