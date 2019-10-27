using DiplAPI1;
using DiplAPI1.Connection.Database_connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test.Connection;
using ZooKeeperNet;

namespace Backend.Init
{
    public class CreateDatabaseTableNodes : IStartup
    {
        public void Init()
        {
            List<string> tablenames = DataBase.GetDatabaseTableNames();

            foreach(string name in tablenames)
            {
                if (ZkConnector.Instance.Exists("/configs/database_schema/" + name, false) == null)
                {
                    ZkConnector.Instance.Create
                        ("/configs/database_schema/" + name, "schema".GetBytes(), Ids.OPEN_ACL_UNSAFE, CreateMode.Persistent);
                }
                if (ZkConnector.Instance.Exists("/locks/" + name, false) == null)
                {
                    ZkConnector.Instance.Create
                        ("/locks/" + name, "schema".GetBytes(), Ids.OPEN_ACL_UNSAFE, CreateMode.Persistent);
                }
                //ZkConnector.Instance.GetChildren("/configs/database_schema/"+name,
                //new QueryWatcher());
            }
        }
    }
}
