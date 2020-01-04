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

        public static string GetFileName(string fileurl)
        {
            string query = "select FileName from Files where FileUrl=@url";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@url", fileurl);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            string fileName = "";
            if (dr.Read())
            {
                fileName = dr["FileName"].ToString();
            }

            conn.Close();
            return fileName;
        }
    }
}
