using Master_Worker.DataBaseConnection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Master_Worker.DataBaseConnection
{
    public class DataBase
    {
        public static SqlConnection conn = new SqlConnection
            ("Server=localhost,50125;Database=FileUpload;User Id=sa;Password=localdb");

        public DataBase(String cstring)
        {
            conn = new SqlConnection(cstring);
        }
        public DataBase()
        {
            
        }

        public static void SaveFile(string fileurl, string filename, string filepath)
        {
            string query = "insert into Files(FileUrl,FileName,FileData) values (@url,@name,@data)";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@url", fileurl);
            cmd.Parameters.AddWithValue("@name", filename);
            //cmd.Parameters.AddWithValue("@data", filedata);
            cmd.Parameters.AddWithValue("@data", SqlDbType.VarBinary).Value = File.ReadAllBytes(filepath);
            cmd.Connection = conn;
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static string GetFileName(string fileurl)
        {
            string query = "select FileName from Files where FileUrl=@url";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@url", fileurl);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            string fileName = "";
            if(dr.Read())
            {
                fileName = dr["FileName"].ToString();
            }

            conn.Close();
            return fileName;
        }

        public static DBFile GetFile(string fileurl)
        {
            string query = "select * from Files where FileUrl=@url";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@url", fileurl);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            byte[] data = null;
            string filename = "";
            while(dr.Read())
            {
                data = (byte[])dr["FileData"];
                filename = dr["FileName"].ToString();
            }
            DBFile file = new DBFile(fileurl, filename, data);
            conn.Close();
            return file;
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
            "WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_CATALOG = ";

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
