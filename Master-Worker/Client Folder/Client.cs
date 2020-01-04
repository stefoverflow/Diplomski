using DiplAPI.Connection;
using Master_Worker.Client_Folder;
using Master_Worker.Task_Folder;
using Master_Worker.Task_Folder.DownloadFileTask;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using ZooKeeperNet;

namespace Master_Worker_Library.Client
{
    public class Client
    {
        public Guid client_uuid { get; }

        public Client()
        {
            client_uuid = Guid.NewGuid();
        }

        public string input_task(ITask task)
        {
            string json = JsonConvert.SerializeObject(task);
            
            string path = "";
            try
            {
                path = ZkConnector.Instance.Create("/clients/" + client_uuid, "".GetBytes(), Ids.OPEN_ACL_UNSAFE, CreateMode.Persistent);
            }
            catch(Exception e)
            {
                //nodeExists exception, ne moze da kreira 2 noda sa isti path
                path = "/clients/" + client_uuid;
            }

            ZkConnector.Instance.Exists("/clients/" + client_uuid, new FinishedTaskWatcher(this));

            //vratim samo task- kvo ide dalje redniti broj
            string input_task_path =  ZkConnector.Instance.Create("/input_task/task-", json.GetBytes(), Ids.OPEN_ACL_UNSAFE, CreateMode.PersistentSequential);
            string input_task_number = input_task_path.Split('/')[2];

            if (string.Compare(task.GetTaskType(), "UploadFileTask") == 0)
            {
                ZkConnector.Instance.GetChildren("/assigned_task", new UploadFileWatcher(this, input_task_number));
            }

            path = path + "/" + input_task_number;
            return path;
        }

        public Guid getClientUuid()
        {
            return client_uuid;
        }

        public void SendTCP(string filename, string IPA, Int32 PortN)
        {
            System.Threading.Thread.Sleep(3000);
            byte[] SendingBuffer = null;
            TcpClient client = null;
            NetworkStream netstream;
            int BufferSize = 1024;
            string name = filename.Split('\\').Last();
            try
            {
                client = new TcpClient(IPA, PortN);
                //lblStatus.Text = "Connected to the Server...\n";
                netstream = client.GetStream();
                FileStream Fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
                int NoOfPackets = Convert.ToInt32
             (Math.Ceiling(Convert.ToDouble(Fs.Length) / Convert.ToDouble(BufferSize)));
                int TotalLength = (int)Fs.Length, CurrentPacketLength, counter = 0;
                //
                //name = name + '*';
                //byte[] buffer = Encoding.ASCII.GetBytes(name);
                //netstream.Write(buffer, 0, System.Text.Encoding.ASCII.GetByteCount(name));
                for (int i = 0; i < NoOfPackets; i++)
                {
                    if (TotalLength > BufferSize)
                    {
                        CurrentPacketLength = BufferSize;
                        TotalLength = TotalLength - CurrentPacketLength;
                    }
                    else
                        CurrentPacketLength = TotalLength;
                    SendingBuffer = new byte[CurrentPacketLength];
                    Fs.Read(SendingBuffer, 0, CurrentPacketLength);
                    netstream.Write(SendingBuffer, 0, (int)SendingBuffer.Length);
                    //if (progressBar1.Value >= progressBar1.Maximum)
                        //progressBar1.Value = progressBar1.Minimum;
                    //progressBar1.PerformStep();
                }
                //byte[] url = null;
                //netstream.Read(url, 0, BufferSize);

                //lblStatus.Text = lblStatus.Text + "Sent " + Fs.Length.ToString() + " bytes to the server";
                Fs.Close();
                netstream.Close();
                client.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                //netstream.Close();
                //client.Close();
            }
        }
    }
}
