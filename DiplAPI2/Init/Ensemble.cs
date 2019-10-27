using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooKeeperNet;
using test.Connection;
using DiplAPI1;
using DiplAPI1.Connection.Database_connection;

namespace Backend
{
    public class Ensemble
    {

        public Ensemble()
        {
            Startup s = new Startup();

            if (ZkConnector.Instance.Exists("/live_nodes/node" + ZkConnector.Instance.Id, false) == null)
            {
                ZkConnector.Instance.Create
                ("/live_nodes/node" + ZkConnector.Instance.Id, ZkConnector.Instance.Id.ToString().GetBytes(), Ids.OPEN_ACL_UNSAFE, CreateMode.Ephemeral);
            }
            if (ZkConnector.Instance.Exists("/all_nodes/node" + ZkConnector.Instance.Id, false) == null)
            {
                ZkConnector.Instance.Create
               ("/all_nodes/node" + ZkConnector.Instance.Id, ZkConnector.Instance.Id.ToString().GetBytes(), Ids.OPEN_ACL_UNSAFE, CreateMode.Persistent);
            }

            List<string> tablenames = DataBase.GetDatabaseTableNames();
            foreach(string name in tablenames)
            {
                ZkConnector.Instance.GetChildren("/configs/database_schema/" + name,
                new QueryWatcher());
            }
            
        }
    }
}
