using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using test;
using ZooKeeperNet;

namespace DiplForms.Watchers
{
    public class NodeChangeWatcher : IWatcher
    {
        private Form1 form;
        private int nodex, nodey;
        private string nodename, controlname;
        KnownColor color;
        public NodeChangeWatcher(Form1 f, int nx, int ny, string node, string cn, KnownColor c)
        {
            form = f;
            nodex = nx;
            nodey = ny;
            nodename = node;
            controlname = cn;
            color = c;
        }
        public void Process(WatchedEvent @event)
        {
            if(@event.Type == ZooKeeperNet.EventType.NodeChildrenChanged)
            {
                if (form.InvokeRequired)
                {
                    form.Invoke(new Action(() => {
                        form.ClearControls(controlname);
                        form.DisplayChildren(nodex, nodey, nodename, controlname, color);
                    }));
                }
                else
                {
                    form.ClearControls(controlname);
                    form.DisplayChildren(nodex, nodey, nodename, controlname, color);
                }
            }
        }
    }
}
