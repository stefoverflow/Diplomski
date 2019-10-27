using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZooKeeperNet;

namespace test
{
    public class CreateConnectionWatcher : IWatcher
    {
        private readonly ManualResetEventSlim _connected = new ManualResetEventSlim(false);

        public void Process(WatchedEvent @event)
        {
            if (@event == null) throw new ApplicationException("bad state");
            if (@event.State != KeeperState.SyncConnected)
                throw new ApplicationException("cannot connect");
            else
                _connected.Set();
            _connected.Wait();
        }
    }
}
