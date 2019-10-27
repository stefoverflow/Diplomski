using DiplAPI1.Connection.Database_connection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test.Connection;
using ZooKeeperNet;

namespace DiplAPI1
{
    public class QueryWatcher : IWatcher
    {
        public void Process(WatchedEvent @event)
        {
            string[] l = @event.Path.Split('/');
            ZkConnector.Instance.GetChildren("/configs/database_schema/" + l[l.Length - 1],
                new QueryWatcher());
            if (@event.Type == ZooKeeperNet.EventType.NodeChildrenChanged)
            {//opet watch event
                try
                {
                    string first = ZkConnector.Instance.GetChildren(@event.Path, false).Min();
                    ZkConnector.Instance.Create(@event.Path + "/" + first + "/ACKDIPLAPI222", "ACK".GetBytes(),
                        Ids.OPEN_ACL_UNSAFE, CreateMode.Persistent);

                    byte[] arr = ZkConnector.Instance.GetData(@event.Path + "/" + first,
                                false, ZkConnector.Instance.Exists(@event.Path + "/" + first, false));



                    DataBase.ExecuteCommandString(System.Text.Encoding.Default.GetString(arr));
                }
                catch (ZooKeeperNet.KeeperException e)
                {

                }

            }
        }
    }
}
