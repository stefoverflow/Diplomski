using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZooKeeperNet;

namespace testForms.Distributed_Lock
{
    public class OrderWatcher : IWatcher
    {
        string query;
        public OrderWatcher(string q)
        {
            this.query = q;
        }
        public void Process(WatchedEvent @event)
        {
            if (@event.Type == ZooKeeperNet.EventType.NodeDeleted)
            {
                MessageBox.Show("Prethodni query su svi izvrsili i sad je na redu " + query);
            }
        }
    }
}
