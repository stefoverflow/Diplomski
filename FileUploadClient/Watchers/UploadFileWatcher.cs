using DiplAPI.Connection;
using FileUploadClient;
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
        Form1 form;

        public UploadFileWatcher(Client client, string path, Form1 f)
        {
            c = client;
            taskpath = path;
            form = f;
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
                if(form.InvokeRequired)
                {
                        
                    form.Invoke(new Action(() =>
                    {
                        form.SendTCP(workerip, 8989);
                    }));
                }
                else
                {
                    form.SendTCP(workerip, 8989);
                }
                    
                }
                else
                {
                    ZkConnector.Instance.GetChildren("/assigned_task", new UploadFileWatcher(c, taskpath, form));
                }
            //}
        }
    }
}
