using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test.Connection;
using ZooKeeperNet;

namespace Backend.Init
{
    public class CreateAllNode : IStartup
    {
        public void Init()
        {
            if (ZkConnector.Instance.Exists("/all_nodes", false) == null)
            {
                ZkConnector.Instance.Create
                ("/all_nodes", "All nodes.".GetBytes(), Ids.OPEN_ACL_UNSAFE, CreateMode.Persistent);
            }
            //if(Startup.Instance.Exists("/all_nodes/node" + Startup.Instance.Id, false) == null)
            //{
            //    Startup.Instance.Create
            //   ("/all_nodes/node" + Startup.Instance.Id, Startup.Instance.SessionId.ToString().GetBytes(), Ids.OPEN_ACL_UNSAFE, CreateMode.Persistent);
            //}
        }
    }
}
