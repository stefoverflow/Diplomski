using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test.Connection;
using ZooKeeperNet;

namespace Backend.Init
{
    public class CreateLiveNode : IStartup
    {
        public void Init()
        {
            if (ZkConnector.Instance.Exists("/live_nodes", false) == null)
            {
                ZkConnector.Instance.Create
                    ("/live_nodes", "Live nodes.".GetBytes(), Ids.OPEN_ACL_UNSAFE, CreateMode.Persistent);
            }
        }
    }
}
