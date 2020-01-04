using DiplAPI.Connection;
using Master_Worker.DataBaseConnection;
using Master_Worker.Task_Folder;
using Master_Worker.Task_Folder.DownloadFileTask;
using Master_Worker.Task_Folder.UploadFileTask;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using ZooKeeperNet;

namespace Master_Worker_Library.Worker
{
    public enum WorkerState
    {
        QUEUE_ENTERED = 0,
        ASSIGNING_TASK = 1,
        I_HAVE_TASK = 2,
        TASK_ASSIGNED = 3,
        EXECUTED = 4,
        FINISHED = 5
    }

    public class Worker
    {
        private Guid my_uuid;
        public string queue_id { get; set; }//worker-my_uuid-queue_id
        public ITask current_task;//sadrzi client_id i  task_id
        public string current_task_path;
        private int task_output;
        private WorkerState worker_state;
        public WorkerState Worker_State
        {
            get { return worker_state; }
            set
            {
                this.worker_state = value;
                PrintState();
            }
        }
                       
        public Worker()
        {
            my_uuid = Guid.NewGuid();
            Worker_State = WorkerState.FINISHED;
        }

        public void PrintState()
        {
            Console.WriteLine("Worker state: " + worker_state.ToString());
            if (current_task != null)
            {
                Console.WriteLine("Task type: " + current_task.GetTaskType());
                //Console.WriteLine(current_task);
            }
            else
            {
                Console.WriteLine("Task type: No task assigned!");
            }
        }

        public Guid GetWorkerUuid()
        {
            return my_uuid;
        }

        public WorkerState GetWorkerState()
        {
            return this.Worker_State;
        }

        public void EnterQueue()
        {
            this.queue_id = ZkConnector.Instance.Create("/worker_queue/worker-" + my_uuid + "_", "".GetBytes(),
                Ids.OPEN_ACL_UNSAFE, CreateMode.EphemeralSequential);
            ZkConnector.Instance.GetChildren("/worker_queue", new FirstInQueueWatcher(this));
            this.current_task = null;
            Worker_State = WorkerState.QUEUE_ENTERED;
            this.Work();
        }

        public void ExecuteTask()
        {
            current_task.Run(current_task_path);
            Worker_State = WorkerState.FINISHED;
            this.EnterQueue();
        }

        public void Work()
        {
            if(Worker_State == WorkerState.ASSIGNING_TASK)
            {
                //goto ;
            }
            this.current_task = null;
            List<string> worker_childs = ZkConnector.Instance.GetChildren("/worker_queue", false).ToList();
            List<string> task_childs = ZkConnector.Instance.GetChildren("/input_task", false).ToList();
            string first_task = task_childs.Min();
            //string min = worker_childs.Min();
            //string first_worker = string.Concat("/worker_queue/", worker_childs.Max());
            string first_worker = string.Concat("/worker_queue/", ReturnMin(worker_childs));
            string queueid = this.queue_id;
            if(String.Compare(first_worker, this.queue_id) == 0)//worker je prvi u redu cekanja
            {
                //worker zna da je prvi u redu cekanja
                ASSIGNING_TASK:
               
                if(task_childs.Count()>0)//ima task koji preuzima
                {
                    Worker_State = WorkerState.ASSIGNING_TASK;
                    

                    string task = System.Text.Encoding.Default.GetString(
                        ZkConnector.Instance.GetData("/input_task/" + first_task, false,
                        ZkConnector.Instance.Exists("/input_task/" + first_task, false)));
                    
                    if ('U' == task[14])
                    {//znaci da oce da uploaduje, worker slusa na portu
                        this.current_task = JsonConvert.DeserializeObject<UploadFileTask>(task);
                        ZkConnector.Instance.Create("/assigned_task/" + first_task,
                        this.GetLocalIPAdress().ToString().GetBytes(), Ids.OPEN_ACL_UNSAFE, CreateMode.Persistent);
                    }
                    else
                    {//znaci da klijent skida fajl, klijent slusa na portu
                        this.current_task = JsonConvert.DeserializeObject<DownloadFileTask>(task);
                        string filename = DataBase.GetFileName(current_task.GetData());
                        ZkConnector.Instance.Create("/assigned_task/" + first_task,
                        filename.GetBytes(), Ids.OPEN_ACL_UNSAFE, CreateMode.Persistent);
                    }
                    this.current_task_path = first_task;
                //workeru je dodeljen zadatak, kreiran je znode u assigned_task grani
                //ali nije napustio red cekanja
                I_HAVE_TASK:
                    Worker_State = WorkerState.I_HAVE_TASK;

                    ZkConnector.Instance.Delete("/input_task/" + first_task, -1);
                    ZkConnector.Instance.Delete(this.queue_id, -1);

                    //zadatak je dodeljen i worker izlazi iz reda
                    TASK_ASSIGNED:
                    Worker_State = WorkerState.TASK_ASSIGNED;

                    this.ExecuteTask();
                }
                else
                {
                    ZkConnector.Instance.GetChildren("/input_task", new FirstInQueueWatcher(this));
                }
            }

            ZkConnector.Instance.GetChildren("/worker_queue", new FirstInQueueWatcher(this));
        }

        private string ReturnMin(List<string> wlist)
        {
            string min;
            min = wlist.First();
            foreach(string el in wlist)
            {
                if (String.Compare(min.Split('_')[1], el.Split('_')[1]) > 0){
                    min = el;
                }
            }
            return min;
        }

        private string getMyPublicIP()
        {
            string externalip = new WebClient().DownloadString("http://icanhazip.com");
            return externalip;
        }

        private string GetLocalIPAdress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}
