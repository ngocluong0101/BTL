using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace BTL_QuanLyQuanNet
{
    class ConnectDatabase
    {
        public ConnectDatabase() {}
        SqlConnection conn = null;
        public void ketnoiSQL()
        {
            string ConnectString = @"Data Source=LAPTOP-5E2VOLNM\SQLEXPRESS;Initial Catalog=BTL_QuanLyQuanNet;Integrated Security=True";
            conn = new SqlConnection(ConnectString);
        }

        public void moKN()
        {
            ketnoiSQL();
            conn.Open();
        }
        public void dongKN()
        {
            conn.Close();
        }
        public SqlConnection GetConnection()
        {
            return conn;
        }
        
    }
}
