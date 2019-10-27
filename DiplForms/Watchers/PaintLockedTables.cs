using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test;
using ZooKeeperNet;

namespace DiplForms.Watchers
{
    class PaintLockedTables : IWatcher
    {
        Form1 form;
        int databaseschemax, databaseschemay;
        public PaintLockedTables(Form1 f, int dsx,int dsy)
        {
            databaseschemax = dsx;
            databaseschemay = dsy;
            form = f;
        }
        public void Process(WatchedEvent @event)
        {
            if (@event.Type == ZooKeeperNet.EventType.NodeChildrenChanged)
            {
                if (form.InvokeRequired)
                {
                    form.Invoke(new Action(() =>
                    {
                        form.ClearControls("configs/database_schema");
                        form.DisplayChildren(databaseschemax, databaseschemay, "/configs/database_schema", "configs/database_schema", KnownColor.ForestGreen);
                    }));
                }
                else
                {
                    form.ClearControls("configs/database_schema");
                    form.DisplayChildren(databaseschemax, databaseschemay, "/configs/database_schema", "configs/database_schema", KnownColor.ForestGreen);
                }
            }
        }
    }
}
