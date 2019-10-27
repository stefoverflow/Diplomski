using DiplAPI.Connection;
using DiplAPI.DistributeLock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZooKeeperNet;

namespace DiplAPI1
{
    public class LockedTableWatcher : IWatcher
    {
        string lockedname;
        string path;
        public LockedTableWatcher(string t,string p)
        {
            lockedname = t;
            path = p;
        }
        public void Process(WatchedEvent @event)
        {
            ZooKeeperNet.EventType e = @event.Type;
            if (@event.Type == ZooKeeperNet.EventType.NodeChildrenChanged)
            {
                IEnumerable<string> allack = ZkConnector.Instance.GetChildren
                    (lockedname, new LockedTableWatcher(lockedname,path));
                int livenodescount = ZkConnector.Instance.GetChildren("/live_nodes", false).Count();

                if (allack.Count() == livenodescount)
                {
                    foreach (var ack in allack)
                    {
                        ZkConnector.Instance.Delete
                            (lockedname + "/" + ack.ToString(), -1);
                    }
                    ZkConnector.Instance.Delete(lockedname, -1);
                    //ZLock.UnLock(lockedname);
                    string[] l = @event.Path.Split('/');
                    ZLock.UnLock(path);
                }
            }
        }
    }
}
