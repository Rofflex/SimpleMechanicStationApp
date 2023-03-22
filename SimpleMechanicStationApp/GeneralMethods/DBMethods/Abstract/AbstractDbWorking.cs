using System.Collections.Generic;
using System.Data.SqlClient;

    abstract class AbstractDbWorking
    {
        public abstract SqlConnection GetConnection();
        public abstract SqlCommand GetCommand(SqlConnection con);
        public abstract bool SqlQueryOutput(SqlCommand cmd);
        public abstract SqlCommand ExecuteSqlQuery();
        public abstract List<string> SqlQueryOutput(SqlCommand cmd, List<string> ValuesList);
    }

