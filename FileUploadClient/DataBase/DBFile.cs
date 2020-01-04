using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master_Worker.DataBaseConnection
{
    public class DBFile
    {
        string _fileurl;
        string _filename;
        byte[] _filedata;

        public string Fileurl { get => _fileurl; set => _fileurl = value; }
        public string Filename { get => _filename; set => _filename = value; }
        public byte[] Filedata { get => _filedata; set => _filedata = value; }

        public DBFile(string fileurl, string filename, byte[] filedata)
        {
            Fileurl = fileurl;
            Filename = filename;
            Filedata = filedata;
        }
    }
}
