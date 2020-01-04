using DiplAPI.Connection;
using Master_Worker_Library.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooKeeperNet;

namespace Master_Worker.Client_Folder
{
    public class UploadFileWatcher : IWatcher
    {
        Client c;
        string taskpath;
        public UploadFileWatcher(Client client, string path)
        {
            c = client;
            taskpath = path;
        }
        public void Process(WatchedEvent @event)
        {
            //if(@event.Type == EventType.NodeCreated)
            //{
                List<string> childs = ZkConnector.Instance.GetChildren("/assigned_task", false).ToList();
                bool isAssigned = false;

                foreach(string child in childs)
                {
                    if(string.Compare(child, taskpath) == 0)
                    {
                        isAssigned = true;
                    }
                }
                if(isAssigned)
                {
                string workerip = System.Text.Encoding.Default.GetString(
                    ZkConnector.Instance.GetData("/assigned_task/" + taskpath, false, 
                    ZkConnector.Instance.Exists("/assigned_task/" + taskpath, false)));
                    c.SendTCP(@"C:\aow_drv.log",workerip, 8989);
                }
                else
                {
                    ZkConnector.Instance.GetChildren("/assigned_task", new UploadFileWatcher(c, taskpath));
                }
            //}
        }
    }
}
