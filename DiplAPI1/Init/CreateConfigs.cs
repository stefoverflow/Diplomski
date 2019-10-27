using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test.Connection;
using ZooKeeperNet;

namespace Backend.Init
{
    public class CreateConfigs : IStartup
    {
        public void Init()
        {
            if (ZkConnector.Instance.Exists("/configs", false) == null)
            {
                ZkConnector.Instance.Create
                    ("/configs", "Configuration of the cluster.".GetBytes(), Ids.OPEN_ACL_UNSAFE, CreateMode.Persistent);
            }
            if (ZkConnector.Instance.Exists("/configs/database_schema", false) == null)
            {
                ZkConnector.Instance.Create
                    ("/configs/database_schema", "Database schema.".GetBytes(), Ids.OPEN_ACL_UNSAFE, CreateMode.Persistent);
            }
            if (ZkConnector.Instance.Exists("/configs/server_config.json", false) == null)
            {
                ZkConnector.Instance.Create
                    ("/configs/server_config.json", "Server configuration of the cluster.".GetBytes(), Ids.OPEN_ACL_UNSAFE, CreateMode.Persistent);
            }
        }
    }
}
