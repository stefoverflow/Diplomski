using DiplAPI.Connection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooKeeperNet;

namespace Master_Worker.Task_Folder.DownloadFileTask
{
    public class DownloadFileTask : ITask
    {
        [JsonProperty]
        string task_type = "DownloadFileTask";
        [JsonProperty]
        private Guid task_id;
        [JsonProperty]
        private Guid client_id;
        [JsonProperty]
        private DownloadFileTaskData input_data;

        public DownloadFileTask(Guid id, DownloadFileTaskData taskdata)
        {
            task_id = Guid.NewGuid();
            client_id = id;
            input_data = taskdata;
        }

        public Guid GetClientId()
        {
            return client_id;
        }

        public Guid GetTaskId()
        {
            return task_id;
        }

        public string GetTaskType()
        {
            return "DownloadFileTask";
        }

        public bool Run(string current_task_path)
        {
            try
            {
                ZkConnector.Instance.Create("/clients/" + GetClientId() + "/" + current_task_path,
                "KLIJENT JE TRAZIO DA SKINE FAJL".GetBytes(), Ids.OPEN_ACL_UNSAFE, CreateMode.Persistent);

                ZkConnector.Instance.Delete("/assigned_task/" + current_task_path, -1);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
    }
}
