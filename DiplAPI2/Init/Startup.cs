using Backend.Init;
using test.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooKeeperNet;

namespace Backend
{
    public class Startup
    {
        public static List<IStartup> startupStrategies = new List<IStartup>();

        public Startup()
        {
            startupStrategies.Add(new CreateAllNode());
            startupStrategies.Add(new CreateConfigs());
            startupStrategies.Add(new CreateLiveNode());
            startupStrategies.Add(new CreateLocks());
            startupStrategies.Add(new CreateDatabaseTableNodes());
            

            foreach(var strategy in startupStrategies)
            {
                strategy.Init();
            }
            
        }

        public List<IStartup> Startups
        {
            get
            {
                return startupStrategies;
            }
        }
    }
}
