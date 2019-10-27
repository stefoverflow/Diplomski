using DiplAPI.Connection;
using DiplAPI1;
using DiplAPI1.Connection.Database_connection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooKeeperNet;

namespace DiplAPI.DistributeLock
{
    public class OrderWatcher : IWatcher
    {
        SqlCommand comm;
        string path;
        string tablename;
        public OrderWatcher(SqlCommand q, string path, string t)
        {
            this.comm = q;
            this.path = path;
            this.tablename = t;
        }
        public void Process(WatchedEvent @event)
        {
            if (@event.Type == ZooKeeperNet.EventType.NodeDeleted)
            {
                //izvrsi query koji je na redu kad dodje na red
                //DataBase.ExecuteNonQuery(comm);
                string par = "";
                foreach (SqlParameter c in comm.Parameters)
                {
                    par += ";";
                    par += c.ParameterName;
                    par += ";";
                    par += c.SqlValue;
                }
                //DataBase.ExecuteNonQuery(command);
                string locked = ZkConnector.Instance.Create("/configs/database_schema/" + tablename + "/",
                    Encoding.ASCII.GetBytes(comm.CommandText + par), Ids.OPEN_ACL_UNSAFE, CreateMode.PersistentSequential);
                ZkConnector.Instance.GetChildren(locked, new LockedTableWatcher(locked,path));
                //string locked = ZkConnector.Instance.Create("/configs/database_schema/" + tablename + "/",
                //    Encoding.ASCII.GetBytes(comm.ToString()), Ids.OPEN_ACL_UNSAFE, CreateMode.PersistentSequential);
                //ZkConnector.Instance.Exists(locked, new LockedTableWatcher(tablename));
                //System.Threading.Thread.Sleep(1500);
                //ZLock.UnLock(path);
            }
        }
    }
}
