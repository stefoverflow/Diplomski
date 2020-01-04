using DiplAPI.Connection;
using FileUploadClient;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;
using ZooKeeperNet;

namespace Master_Worker_Library.Client
{
    public class FinishedTaskWatcher : IWatcher
    {
        Client client;
        Form1 form;

        public FinishedTaskWatcher(Client c, Form1 f)
        {
            client = c;
            form = f;
        }

        public void Process(WatchedEvent @event)
        {
            string result = System.Text.Encoding.Default.GetString(ZkConnector.Instance.GetData(@event.Path,false,
                ZkConnector.Instance.Exists("/clients/" + client.client_uuid, false)));

            if(form.InvokeRequired)
            {
                form.Invoke(new Action(() =>
                {
                    form.SetUrl(result);
                }));
            }
            else
            {
                form.SetUrl(result);
            }
            
            //Console.WriteLine(result);

            ZkConnector.Instance.Delete("/clients/" + client.client_uuid, -1);
        }
    }
}
