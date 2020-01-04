using DiplAPI.Connection;
using System;
using System.Collections.Generic;
using System.Text;
using ZooKeeperNet;

namespace Master_Worker_Library.Client
{
    public class FinishedTaskWatcher : IWatcher
    {
        Client client;

        public FinishedTaskWatcher(Client c)
        {
            client = c;
        }

        public void Process(WatchedEvent @event)
        {
            
            string result = System.Text.Encoding.Default.GetString(ZkConnector.Instance.GetData("/clients/" + client.client_uuid,false,
                ZkConnector.Instance.Exists("/clients/" + client.client_uuid, false)));

            //Console.WriteLine(result);

            ZkConnector.Instance.Delete("/clients/" + client.client_uuid, -1);
        }
    }
}
