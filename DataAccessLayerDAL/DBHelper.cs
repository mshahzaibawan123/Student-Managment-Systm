using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataAccessLayerDAL
{
    public class DBHelper
    {
        public static SqlConnection GetConnection()
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-573DLSK\\SQLEXPRESS;Initial Catalog=StudentManagment;Integrated Security=True");
            return con;
        }

    }
}
