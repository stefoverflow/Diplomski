using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test.Connection;
using ZooKeeperNet;

namespace Backend.Init
{
    public class CreateLocks : IStartup
    {
        public void Init()
        {
            if (ZkConnector.Instance.Exists("/locks", false) == null)
            {
                ZkConnector.Instance.Create
                    ("/locks", "Distributed lock.".GetBytes(), Ids.OPEN_ACL_UNSAFE, CreateMode.Persistent);
            }
        }
    }
}
