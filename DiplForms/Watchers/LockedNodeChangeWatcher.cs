using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using test;
using ZooKeeperNet;

namespace DiplForms.Watchers
{
    public class LockedNodeChangeWatcher : IWatcher
    {
        string tablename;
        int nodex;
        int nodey;
        Form1 form;
        string controlname;

        public LockedNodeChangeWatcher(Form1 f, string t, string cname, int nx, int ny)
        {
            tablename = t;
            nodex = nx;
            nodey = ny;
            form = f;
            controlname = cname;
        }
        public void Process(WatchedEvent @event)
        {
            if (@event.Type == ZooKeeperNet.EventType.NodeChildrenChanged)
            {
                if (form.InvokeRequired)
                {
                    form.Invoke(new Action(() =>
                    {
                        form.ClearControls(controlname);
                        form.DisplayLockedForTable(tablename, nodex, nodey);
                    }));
                }
                else
                {
                    form.ClearControls(controlname);
                    form.DisplayLockedForTable(tablename, nodex, nodey);
                }
            }
        }
    }
}
