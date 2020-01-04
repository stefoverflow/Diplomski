using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master_Worker.Task_Folder.UploadFileTask
{
    public class UploadFileTaskData
    {
        [JsonProperty]
        private string _clientip;
        [JsonProperty]
        private string _filename;

        public UploadFileTaskData(string clientip, string filename)
        {
            Clientip = clientip;
            Filename = filename;
        }

        public string Clientip { get => _clientip; set => _clientip = value; }
        public string Filename { get => _filename; set => _filename = value; }
    }
}
