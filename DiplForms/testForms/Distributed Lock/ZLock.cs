using test.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooKeeperNet;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using testForms.Distributed_Lock;

namespace test
{
    public class ZLock
    {
        private String parentPath;
        private byte[] parentNodeData;

        public ZLock(String path, String data)
        {
            this.parentPath = path;

            parentNodeData = Encoding.ASCII.GetBytes(data);

            if (ZkConnector.Instance.Exists(parentPath, false) == null)
            {
                ZkConnector.Instance.Create
                (parentPath, parentNodeData, Ids.OPEN_ACL_UNSAFE, CreateMode.Persistent);
            }
        }

        public String Lock(String query)
        {
            String path = ZkConnector.Instance.Create
                (parentPath + "/" + "lock-", Encoding.ASCII.GetBytes(query), Ids.OPEN_ACL_UNSAFE, CreateMode.EphemeralSequential);
            ZkConnector.Instance.GetChildren(path, false);
            //IEnumerable<String> childs = ZkConnector.Instance.GetChildren(parentPath, false);
            //GetChildren vraca relativnu putanju,bez parent i zato mu parent stavljam jer gore path vraca apsolutnu
            while (String.Compare(parentPath + "/" + ZkConnector.Instance.GetChildren(parentPath, false).Min(),path) != 0)
            {
                //do nothing
            }
            IEnumerable<String> childs = ZkConnector.Instance.GetChildren(parentPath, false);
            if (String.Compare(parentPath + "/" + childs.Min(), path) == 0)
            {
                //execute query
                MessageBox.Show("ZAKLJUCAVA " + path + "     i izvrsava query ="+ query);
                   
                return path;
            }
            else//znaci da nije on na redu i ima pre toga query
            {

            }

            //int number = Int32.Parse(Regex.Match(path, @"0*[0-9][0-9]*").Value);

            //ZkConnector.Instance.Exists(parentPath + "/lock-" + parse(number - 1), new OrderWatcher(query));
            return path;
        }

        public void UnLock(String pathNode)
        {
            ZkConnector.Instance.Delete(pathNode, -1);
            MessageBox.Show("OTKLJUCAN JE " + parentPath);
        }

        public static String parse(int number)
        {
            String parsed = "";
            int count = 10 - number.ToString().Length;
            while (count != 0)
            {
                parsed = parsed + "0";
                --count;
            };
            parsed = parsed + number.ToString();
            return parsed;
        }
    }
}
