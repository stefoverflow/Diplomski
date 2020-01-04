using DiplAPI.Connection;
using Master_Worker.DataBaseConnection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
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

        public string GetData()
        {
            return input_data.Fileurl;
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
            Console.WriteLine("____________________________");
            bool status = false;
            System.Threading.Thread.Sleep(5000);
            byte[] SendingBuffer = null;
            TcpClient client = null;
            NetworkStream netstream;
            int BufferSize = 1024;
            try
            {
                client = new TcpClient(input_data.Clientip, 8989);
                netstream = client.GetStream();
                string filename = DataBaseConnection.DataBase.GetFileName(input_data.Fileurl);
                //input_data.Fileurl;
                //Console.WriteLine("File name: " + input_data.Fileurl);
                //FileStream Fs = new FileStream( filename, FileMode.Open, FileAccess.Read);
                DBFile file = DataBaseConnection.DataBase.GetFile(input_data.Fileurl);
                //file.
                FileStream fscreate = File.Create(file.Filename);
                fscreate.Write(file.Filedata, 0, file.Filedata.Count());
                fscreate.Close();
                FileStream Fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
                int NoOfPackets = Convert.ToInt32
             (Math.Ceiling(Convert.ToDouble(file.Filedata.Length) / Convert.ToDouble(BufferSize)));
                Console.WriteLine("Sending file...");
                int TotalLength = (int)file.Filedata.Length, CurrentPacketLength, counter = 0;
                //
                for (int i = 0; i < NoOfPackets; i++)
                {
                    if (TotalLength > BufferSize)
                    {
                        CurrentPacketLength = BufferSize;
                        TotalLength = TotalLength - CurrentPacketLength;
                    }
                    else
                    {
                        CurrentPacketLength = TotalLength;

                    }
                    SendingBuffer = new byte[CurrentPacketLength];
                    //SendingBuffer.co
                    //BinaryWriter()
                    Fs.Read(SendingBuffer, 0, CurrentPacketLength);
                    netstream.Write(SendingBuffer, 0, (int)SendingBuffer.Length);
                    //netstream.Write(file.Filedata,NoOfPackets*CurrentPacketLength, CurrentPacketLength);
                }
                Console.WriteLine("File sended successfully!");
                Fs.Close();
                //fscreate.Close();
                netstream.Close();
                client.Close();
                File.Delete(filename);
                status = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                status = false;
            }
            finally
            {
                //netstream.Close();
                //client.Close();
            }

            try
            {
                ZkConnector.Instance.Create("/clients/" + GetClientId() + "/" + current_task_path,
                status.ToString().GetBytes(), Ids.OPEN_ACL_UNSAFE, CreateMode.Persistent);

                ZkConnector.Instance.Delete("/assigned_task/" + current_task_path, -1);
            }
            catch (Exception e)
            {
                status = false;
            }
            return status;
        }
    }
}
