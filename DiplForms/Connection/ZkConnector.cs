using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooKeeperNet;

namespace test.Connection
{
    public class ZkConnector
    {
        private static ZooKeeper instance;

        public ZkConnector()
        {
            instance = new ZooKeeper("localhost", TimeSpan.FromMilliseconds(5000), new CreateConnectionWatcher());
        }

        public static ZooKeeper Instance
        {
            get
            {
                if (instance == null)
                    instance = new ZooKeeper("localhost", TimeSpan.FromMilliseconds(5000), new CreateConnectionWatcher());
                return instance;
            }
        }
    }
}
