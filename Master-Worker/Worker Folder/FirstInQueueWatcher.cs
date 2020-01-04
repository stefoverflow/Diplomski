using DiplAPI.Connection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZooKeeperNet;

namespace Master_Worker_Library.Worker
{
    public class FirstInQueueWatcher : IWatcher
    {
        Worker worker;
        public FirstInQueueWatcher(Worker w)
        {
            worker = w;
        }
        public void Process(WatchedEvent @event)
        {
            if(@event.Type == ZooKeeperNet.EventType.NodeChildrenChanged)
            {
                worker.Work();
            }
        }
    }
}
