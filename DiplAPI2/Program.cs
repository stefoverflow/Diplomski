using Backend;
using DiplAPI.Connection;
using DiplAPI1;
using DiplAPI1.Connection.Database_connection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using ZooKeeperNet;

namespace DiplAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Ensemble e = new Backend.Ensemble();
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
            foreach (string name in tablenames)
            {
                ZkConnector.Instance.GetChildren("/configs/database_schema/" + name,
                new QueryWatcher());
            }
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
