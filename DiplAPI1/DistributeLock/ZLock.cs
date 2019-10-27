using DiplAPI.Connection;
using DiplAPI1;
using DiplAPI1.Connection.Database_connection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ZooKeeperNet;

namespace DiplAPI.DistributeLock
{
    public class ZLock
    {
        private String parentPath;
        private byte[] parentNodeData;

        public ZLock(String path, String data)
        {
            this.parentPath = path;

            parentNodeData = Encoding.ASCII.GetBytes(data);

            if (ZkConnector.Instance.Exists(parentPath, false) == null)
            {
                ZkConnector.Instance.Create
                (parentPath, parentNodeData, Ids.OPEN_ACL_UNSAFE, CreateMode.Persistent);
            }
        }

        public String Lock(SqlCommand command,string tablename)
        {
            command.CommandType = System.Data.CommandType.Text;

            String path = ZkConnector.Instance.Create
                (parentPath + "/" + "lock-", Encoding.ASCII.GetBytes(command.CommandText), Ids.OPEN_ACL_UNSAFE, CreateMode.EphemeralSequential);
            ZkConnector.Instance.GetChildren(path, false);
            IEnumerable<String> childs = ZkConnector.Instance.GetChildren(parentPath, false);
            
            if (String.Compare(parentPath + "/" + childs.Min(), path) == 0)
            {   //execute query
                string par="";
                foreach(SqlParameter comm in command.Parameters)
                {
                    par += ";";
                    par += comm.ParameterName;
                    par += ";";
                    par += comm.SqlValue;
                }
                //DataBase.ExecuteNonQuery(command);
                string locked = ZkConnector.Instance.Create("/configs/database_schema/" + tablename+"/",
                    Encoding.ASCII.GetBytes(command.CommandText + par), Ids.OPEN_ACL_UNSAFE, CreateMode.PersistentSequential);
                ZkConnector.Instance.GetChildren(locked, new LockedTableWatcher(locked,path));
                //System.Threading.Thread.Sleep(1500);
                //ZkConnector.Instance.Create("/configs/database_schema/" + tablename, command.); ;
                ///UnLock(path);
                return path;
            }

            int number = Int32.Parse(Regex.Match(path, @"0*[0-9][0-9]*").Value);

            ZkConnector.Instance.Exists(parentPath + "/lock-" + parse(number - 1), new OrderWatcher(command, path, tablename));
            return path;
        }

        public static void UnLock(String pathNode)
        {
            try
            {
                ZkConnector.Instance.Delete(pathNode, -1);
            }
            catch(ZooKeeperNet.KeeperException.NoNodeException)
            {
                //vec obrisan
            }           
        }

        public static String parse(int number)
        {
            String parsed = "";
            int count = 10 - number.ToString().Length;
            while (count != 0)
            {
                parsed = parsed + "0";
                --count;
            };
            parsed = parsed + number.ToString();
            return parsed;
        }
    }
}
