using Newtonsoft.Json;

namespace Master_Worker.Task_Folder.DownloadFileTask
{
    public class DownloadFileTaskData
    {
        [JsonProperty]
        private string _clientip;
        [JsonProperty]
        private string _fileurl;
        
        public DownloadFileTaskData(string clientip,string fileurl)
        {
            Clientip = clientip;
            Fileurl = fileurl;
        }

        public string Clientip { get => _clientip; set => _clientip = value; }
        public string Fileurl { get => _fileurl; set => _fileurl = value; }
    }
}
