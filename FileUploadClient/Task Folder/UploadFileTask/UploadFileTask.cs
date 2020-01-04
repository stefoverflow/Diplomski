using DiplAPI.Connection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ZooKeeperNet;

namespace Master_Worker.Task_Folder.UploadFileTask
{
    public class UploadFileTask : ITask
    {
        [JsonProperty]
        string task_type = "UploadFileTask";
        [JsonProperty]
        private Guid task_id;
        [JsonProperty]
        private Guid client_id;
        [JsonProperty]
        private UploadFileTaskData input_data;

        public UploadFileTask(Guid id, UploadFileTaskData inputdata)
        {
            task_id = Guid.NewGuid();
            client_id = id;
            input_data = inputdata;
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
            return "UploadFileTask";
        }

        public bool Run(string current_task_path)
        {
            Console.WriteLine("____________________________");
            TcpListener Listener = null;
            int BufferSize = 1024;
            string fileurl = "";
            try
            {
                Listener = new TcpListener(IPAddress.Any ,8989);
                Listener.Start();
                Console.WriteLine("Tcp listener started...");
            }
            catch (Exception ex)
            {
            }
            byte[] RecData = new byte[BufferSize];
            int RecBytes;
            string Status;
            for (; ; )
            {
                TcpClient client = null;
                NetworkStream netstream = null;
                Status = string.Empty;
                try
                {
                    
                    if (Listener.Pending())
                    {
                        Console.WriteLine("Incoming connection...");
                        client = Listener.AcceptTcpClient();
                        netstream = client.GetStream();
                        Console.WriteLine("Connected to a client!");

                        string initialDirectory = @"D:\primljeno\";

                        string SaveFileName = input_data.Filename;
                        int totalrecbytes = 0;

                        FileStream Fs = new FileStream
                            (initialDirectory + SaveFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);

                        Console.WriteLine("File transfer started...");
                        while ((RecBytes = netstream.Read(RecData, 0, RecData.Length)) > 0)
                        {
                            Fs.Write(RecData, 0, RecBytes);
                            totalrecbytes += RecBytes;
                        }
                        
                        Console.WriteLine("Total bytes received: " + totalrecbytes);
                        Console.WriteLine("File succesfully received! File url is: " + fileurl);
                        //BinaryReader br = new BinaryReader(Fs);
                        //byte[] array = br.ReadBytes((int)Fs.Length);
                        //DataBase.SaveFile(fileurl, SaveFileName, array);
                        Fs.Close();
                        netstream.Close();
                        client.Close();
                        goto End;
                    }
                }
                catch (Exception ex)
                {
                    //PrintRed(_console, ex.Message);
                }
            }
            End: Listener.Stop();
            try
            {
                ZkConnector.Instance.Create("/clients/" + GetClientId() + "/" + current_task_path,
                fileurl.GetBytes(), Ids.OPEN_ACL_UNSAFE, CreateMode.Persistent);

                ZkConnector.Instance.Delete("/assigned_task/" + current_task_path, -1);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }
    }
}
