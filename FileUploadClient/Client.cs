using DiplAPI.Connection;
using Master_Worker.Client_Folder;
using Master_Worker.Task_Folder;
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

        public Guid getClientUuid()
        {
            return client_uuid;
        }
        
    }
}
