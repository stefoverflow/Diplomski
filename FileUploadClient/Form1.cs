using DiplAPI.Connection;
using Master_Worker.Client_Folder;
using Master_Worker.DataBaseConnection;
using Master_Worker.Task_Folder;
using Master_Worker.Task_Folder.DownloadFileTask;
using Master_Worker.Task_Folder.UploadFileTask;
using Master_Worker_Library.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZooKeeperNet;

namespace FileUploadClient
{
    public partial class Form1 : Form
    {
        string filePath;
        string fileName;
        string ClientFolderPath;
        Client c;
        public Form1()
        {
            InitializeComponent();

            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;

            lblFileName.Text = "";
            lblStatus.Text = "";
            lblDownloadStatus.Text = "";
            btnUpload.Enabled = false;
            tbxUploadUrl.Enabled = false;

            panelMain.BringToFront();
            panelUpload.SendToBack();
            panelDownload.SendToBack();

            c = new Client();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            DialogResult rslt = openFileDialog1.ShowDialog();
            if (rslt == DialogResult.OK)
            {
                btnUpload.Enabled = true;
                filePath = openFileDialog1.FileName;
                fileName = filePath.Split('\\').Last();
                lblFileName.Text = fileName;
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            UploadFileTaskData uftd = new UploadFileTaskData(getMyPublicIP(), fileName);
            UploadFileTask uft = new UploadFileTask(c.client_uuid, uftd);
            input_task(uft);
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            DownloadFileTaskData dftd = new DownloadFileTaskData(GetLocalIPAdress(), tbxDownloadUrl.Text);
            DownloadFileTask dft = new DownloadFileTask(c.client_uuid, dftd);
            input_task(dft);
        }

        public void SendTCP( string IPA, Int32 PortN)
        {
            System.Threading.Thread.Sleep(3000);
            byte[] SendingBuffer = null;
            TcpClient client = null;
            NetworkStream netstream;
            int BufferSize = 1024;
            try
            {
                client = new TcpClient(IPA, PortN);
                lblStatus.Text = "Connected to the Server...\n";
                netstream = client.GetStream();
                FileStream Fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                int NoOfPackets = Convert.ToInt32
             (Math.Ceiling(Convert.ToDouble(Fs.Length) / Convert.ToDouble(BufferSize)));
                int TotalLength = (int)Fs.Length, CurrentPacketLength, counter = 0;
                //
                progressBar1.Minimum = 0;
                progressBar1.Maximum = (int)Fs.Length;
                progressBar1.Step = BufferSize;
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
                    Fs.Read(SendingBuffer, 0, CurrentPacketLength);
                    netstream.Write(SendingBuffer, 0, (int)SendingBuffer.Length);
                    if (progressBar1.Value >= progressBar1.Maximum)
                        progressBar1.Value = progressBar1.Minimum;
                    progressBar1.PerformStep();
                }

                lblStatus.Text = "File uploaded successfully!";
                Fs.Close();
                netstream.Close();
                client.Close();

            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //netstream.Close();
                //client.Close();
            }
        }

        public void ReceiveTCP(string filename, int portN)
        {
            TcpListener Listener = null;
            int BufferSize = 1024;
            try
            {
                Listener = new TcpListener(IPAddress.Any, portN);
                Listener.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("Kod listener: " + ex.Message);
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
                    string message = "Accept the Incoming File ";
                    string caption = "Incoming Connection";
                    //MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    //DialogResult result;
                    openFileDialog1.FileName = filename;
                    if (Listener.Pending())
                    {
                        client = Listener.AcceptTcpClient();
                        netstream = client.GetStream();
                        Status = "Connected to a client\n";
                        //result = MessageBox.Show(message, caption, buttons);

                        //if (result == System.Windows.Forms.DialogResult.Yes)
                        //{
                            string SaveFileName = string.Empty;
                        //SaveFileDialog DialogSave = new SaveFileDialog();
                        //DialogSave.Filter = "All files (*.*)|*.*";
                        //DialogSave.RestoreDirectory = true;
                        //DialogSave.Title = "Where do you want to save the file?";
                        //DialogSave.InitialDirectory = @"C:\Downloads";
                        // if (DialogSave.ShowDialog() == DialogResult.OK)
                        CreateDownloadDirectory();
                        SaveFileName = @"C:\FileUpload\" + filename;//DialogSave.FileName;
                            if (SaveFileName != string.Empty)
                            {
                                int totalrecbytes = 0;
                                FileStream Fs = new FileStream
                                (SaveFileName, FileMode.OpenOrCreate, FileAccess.Write);
                                while ((RecBytes = netstream.Read
                                (RecData, 0, RecData.Length)) > 0)
                                {
                                    Fs.Write(RecData, 0, RecBytes);
                                    totalrecbytes += RecBytes;
                                }
                                Fs.Close();
                            //}
                            netstream.Close();
                            client.Close();
                            lblDownloadStatus.Text = "File downloaded at C:\\FileUpload folder!";
                            goto End;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    MessageBox.Show(ex.Message);
                    //netstream.Close();
                    //client.Close();
                }
            }
        End: Listener.Stop();
        }

        private string getMyPublicIP()
        {
            string externalip = new WebClient().DownloadString("http://icanhazip.com");
            return externalip;
        }

        private void input_task(ITask task)
        {
            string json = JsonConvert.SerializeObject(task);

            string path = "";
            try
            {
                path = ZkConnector.Instance.Create("/clients/" + c.client_uuid, "".GetBytes(), Ids.OPEN_ACL_UNSAFE, CreateMode.Persistent);
            }
            catch (Exception e)
            {
                //nodeExists exception, ne moze da kreira 2 noda sa isti path
                path = "/clients/" + c.client_uuid;
            }

            //vratim samo task- kvo ide dalje redniti broj
            string input_task_path = ZkConnector.Instance.Create("/input_task/task-", json.GetBytes(), Ids.OPEN_ACL_UNSAFE, CreateMode.PersistentSequential);
            string input_task_number = input_task_path.Split('/')[2];

            if (string.Compare(task.GetTaskType(), "UploadFileTask") == 0)
            {
                ZkConnector.Instance.GetChildren("/assigned_task", new UploadFileWatcher(c, input_task_number, this));
            }
            else
            {
                //ReceiveTCP(8989);
                ZkConnector.Instance.GetChildren("/assigned_task", new Watchers.DownloadFileWatcher(c, input_task_number, this));
            }

            path = path + "/" + input_task_number;
            ClientFolderPath = path;
            ZkConnector.Instance.Exists(ClientFolderPath, new FinishedTaskWatcher(c, this));
        }

        public void SetUrl(string url)
        {
            tbxUploadUrl.Enabled = true;
            tbxUploadUrl.Text = url;
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

        public void CreateDownloadDirectory()
        {
            string path = @"C:\FileUpload";

            try
            {
                if(Directory.Exists(path))
                {
                    return;
                }
                else
                {
                    Directory.CreateDirectory(path);
                }
            }
            catch (Exception e)
            {

            }
        }


        //forma
        private void btnOpenUploadPanel_Click(object sender, EventArgs e)
        {
            panelMain.SendToBack();
            panelUpload.BringToFront();
            panelDownload.SendToBack();
        }

        private void btnOpenDownloadPanel_Click(object sender, EventArgs e)
        {
            panelMain.SendToBack();
            panelDownload.BringToFront();
            panelUpload.SendToBack();
        }

        private void btnBackUpload_Click(object sender, EventArgs e)
        {
            panelUpload.SendToBack();
            panelMain.BringToFront();
            panelDownload.SendToBack();
        }

        private void btnBackDownload_Click(object sender, EventArgs e)
        {
            panelDownload.SendToBack();
            panelMain.BringToFront();
            panelDownload.SendToBack();
        }
    }
}
