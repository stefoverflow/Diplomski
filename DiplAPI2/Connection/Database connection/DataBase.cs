using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DiplAPI1.Connection.Database_connection
{
    public class DataBase
    {
        private static string databasename = "'Replica1'";
        public static SqlConnection conn = new SqlConnection
            ("Server=localhost,50125;Database=Replica1;User Id=sa;Password=localdb");

        public DataBase(String cstring)
        {
            conn = new SqlConnection(cstring);
        }
        public DataBase()
        {
            conn = new SqlConnection("Server=localhost,50125;Database=Replica1;User Id=sa;Password=localdb");
        }

        public static void ExecuteNonQuery(SqlCommand comm)
        {
            comm.Connection = conn;
            conn.Open();
            comm.ExecuteNonQuery();
            conn.Close();
        }

        public static void ExecuteCommandString(string comm)
        {
            string[] q = comm.Split(';');
            string query = q[0];
            SqlCommand command = new SqlCommand(query, conn);

            for (int i = 1; i < q.Count() - 1; i += 2)
            {
                string name = q[i];
                string value = q[i + 1];
                command.Parameters.AddWithValue(name, value);
            }

            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
        }

        public static List<string> GetDatabaseTableNames()
        {
            string query = "SELECT TABLE_NAME " +
            "FROM INFORMATION_SCHEMA.TABLES " +
            "WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_CATALOG = " + databasename;

            SqlCommand command = new SqlCommand(query, conn);
            conn.Open();
            SqlDataReader reader = command.ExecuteReader();
            List<string> tableList = new List<string>();

            while (reader.Read())
            {
                string[] temp = new string[reader.FieldCount];

                for (int i = 0; i < reader.FieldCount; i++)
                    temp[i] = Convert.ToString(reader.GetValue(i));

                tableList.Add(string.Join("", temp));
            }

            reader.Close();
            conn.Close();

            return tableList;
        }

        public static string[] GetRow(SqlCommand comm)
        {
            comm.Connection = conn;
            conn.Open();
            SqlDataReader reader = comm.ExecuteReader();

            string[] dataList = new string[reader.FieldCount];
            if (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                    dataList[i] = Convert.ToString(reader.GetValue(i));
            }

            reader.Close();
            conn.Close();

            return dataList;
        }

        public static List<string[]> GetTableRows(SqlCommand comm)
        {
            comm.Connection = conn;
            conn.Open();
            SqlDataReader reader = comm.ExecuteReader();
            List<string[]> dataList = new List<string[]>();

            while (reader.Read())
            {
                string[] temp = new string[reader.FieldCount];

                for (int i = 0; i < reader.FieldCount; i++)
                    temp[i] = Convert.ToString(reader.GetValue(i));

                dataList.Add(temp);
            }

            reader.Close();
            conn.Close();

            return dataList;
        }
    }
}
