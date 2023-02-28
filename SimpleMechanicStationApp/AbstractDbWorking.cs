using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMechanicStationApp
{
    abstract class AbstractDbWorking
    {
        public abstract SqlConnection GetConnection();
        public abstract SqlCommand GetCommand(SqlConnection con);
        public abstract SqlDataReader ExecuteCommand(SqlCommand cmd);
        public abstract bool SqlQueryOutput(SqlDataReader reader);
        public abstract SqlDataReader ExecuteSqlQuery();
    }
}
