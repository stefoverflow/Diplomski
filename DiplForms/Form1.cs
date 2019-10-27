using DiplForms.Watchers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using test.Connection;
using ZooKeeperNet;

namespace test
{
    public partial class Form1 : Form
    {
        //treba stavim watch da kad ima u locks u tabelu da pocrveni levo tabelata
        //i treba desno u locks da se redjaju kako zakljucavaju da se nizu
        private int livenodesx, livenodesy, allnodesx, allnodesy;
        private int databaseschemax, databaseschemay, locksx, locksy;
        private int w, h;
        public Form1()
        {
            InitializeComponent();
            
            this.LoadStatic();
        }

        public void LoadStatic()
        {
            //DrawButton((int)(w * 0.4), (int)(h * 0.02), 105, 30, "server_config.json", KnownColor.ForestGreen, KnownColor.ForestGreen);
            //DrawButton((int)(w * 0.4), (int)(h * 0.1), 105, 30, "database_schema", KnownColor.ForestGreen, KnownColor.ForestGreen);
            if(this.InvokeRequired)
            {
                this.Invoke(new Action(() => { this.Controls.Clear(); }));
            }
            else
            {
                this.Controls.Clear();
            }
            this.w = this.Width;// = 683;
            this.h = this.Height;// = 600;
            DrawButton((int)(w * 0.5)-20, (int)(h * 0.1)-20, 40, 40, "/", "/", KnownColor.ForestGreen, KnownColor.ForestGreen);
            //"/"->"configs"
            DrawLine((int)(w * 0.2), (int)(h * 0.1), (int)(w * 0.3), 2, "/");
            DrawLine((int)(w * 0.2), (int)(h * 0.1), 2, (int)(h * 0.2)-30, "/");
            DrawButton((int)(w * 0.2)-30, (int)(h * 0.3)-30, 63, 63, "configs", "configs", KnownColor.ForestGreen, KnownColor.ForestGreen);
            //"/"->"live_nodes"
            DrawLine((int)(w * 0.4), (int)(h * 0.1) + 10, (int)(w * 0.1), 2, "live_nodes");
            DrawLine((int)(w * 0.4), (int)(h * 0.1) + 10, 2, (int)(h * 0.2) - 44, "live_nodes");
            this.livenodesx = (int)(w * 0.4) - 34;
            this.livenodesy = (int)(h * 0.3) - 34;
            DrawButton(livenodesx, livenodesy, 68, 68, "live_nodes", "live_nodes", KnownColor.ForestGreen, KnownColor.ForestGreen);
            //"/"->"all_nodes"
            DrawLine((int)(w * 0.5), (int)(h * 0.1) + 10, (int)(w * 0.1), 2, "all_nodes");
            DrawLine((int)(w * 0.6), (int)(h * 0.1) + 10, 2, (int)(h * 0.2) - 41, "all_nodes");
            this.allnodesx = (int)(w * 0.6) - 31;
            this.allnodesy = (int)(h * 0.3) - 31;
            DrawButton(allnodesx, allnodesy, 63, 63, "all_nodes", "all_nodes", KnownColor.ForestGreen, KnownColor.ForestGreen);
            //"/"->"locks"
            DrawLine((int)(w * 0.5), (int)(h * 0.1), (int)(w * 0.3), 2, "locks");
            DrawLine((int)(w * 0.8), (int)(h * 0.1), 2, (int)(h * 0.2)-32, "locks");
            this.locksx = (int)(w * 0.8) - 32;
            this.locksy = (int)(h * 0.3) - 32;
            DrawButton(locksx, locksy, 63, 63, "locks", "locks", KnownColor.ForestGreen, KnownColor.ForestGreen);
            //"configs"->"server_config.json"
            DrawLine((int)(w * 0.1), (int)(h * 0.3), (int)(w * 0.1), 2, "server_config.json");
            DrawLine((int)(w * 0.1), (int)(h * 0.3), 2, (int)(h * 0.1), "server_config.json");
            DrawButton((int)(w * 0.1)-60, (int)(h * 0.4), 105, 30, "server_config.json", "server_config.json", KnownColor.ForestGreen, KnownColor.ForestGreen);
            //"configs"->"database_schema"
            DrawLine((int)(w * 0.2), (int)(h * 0.3), 2, (int)(h * 0.2), "database_schema");
            this.databaseschemax = (int)(w * 0.2) - 50;
            this.databaseschemay = (int)(h * 0.5);
            DrawButton(databaseschemax, databaseschemay, 105, 30, "database_schema", "database_schema", KnownColor.ForestGreen, KnownColor.ForestGreen);

            LoadDynamic();
        }

        private void LoadDynamic()
        {
            DisplayChildren(livenodesx, livenodesy, "/live_nodes", "live_nodes_child", KnownColor.Orange);
            DisplayChildren(databaseschemax+15, databaseschemay, "/configs/database_schema", "configs/database_schema", KnownColor.ForestGreen);
            DisplayChildren(allnodesx, allnodesy, "/all_nodes", "all_nodes", KnownColor.ForestGreen);
            DisplayChildren(locksx, locksy, "/locks", "locks", KnownColor.ForestGreen);

        }

        public void DisplayLockedForTable(string tablename,int tablex,int tabley)
        {
            IEnumerable<string> locks = ZkConnector.Instance.GetChildren(tablename,
                new LockedNodeChangeWatcher(this,tablename, tablename,tablex,tabley))
                .OrderBy(node => node);


            int step = (int)(0.1 * h);
            //DrawLine(tablex + 35, tabley, 2, locks.Count()*step, tablename);// locks.Count()*step-tabley+step, tablename);
            foreach (var item in locks)
            {
                //DrawLine(tablex + 35, tabley + step - (int)(h * 0.1), 2, (int)(h * 0.1), tablename);
                DrawLine(tablex + 35, tabley + step + 25- (int)(h * 0.1), 2, (int)(h * 0.1)-25, tablename);
                DrawButton(tablex-20, tabley + step, 110, 25, item.ToString(),
                    tablename, KnownColor.MediumOrchid, KnownColor.MediumOrchid);
                step += (int)(0.1 * h);
            }
        }

        public void ClearControls(string name)
        {
            IEnumerable<Button> buttons = this.Controls.OfType<Button>().Where(cname => String.Compare(cname.Name, name) == 0);
            int butoncount = buttons.Count();
            foreach(Button item in buttons)
            {
                this.Controls.Remove(item);
            }
            IEnumerable<Label> labels = this.Controls.OfType<Label>().Where(cname => String.Compare(cname.Name, name) == 0);
            int labelcount = labels.Count();
            foreach (Label item in labels)
            {
                this.Controls.Remove(item);
            }
            //foreach (Control item in this.Controls.OfType<Button>())
            //{
            //    if (item.Name == name)
            //        this.Controls.Remove(item);
            //}
            foreach (Control item in this.Controls.OfType<Label>())
            {
                if (item.Name == name)
                    this.Controls.Remove(item);
            }
        }

        public void DisplayChildren(int nodex, int nodey, string node, string name, KnownColor color)
        {
            NodeChangeWatcher watch = new NodeChangeWatcher
                (this, nodex, nodey, node, name, color);
            IEnumerable<string> livenodes = ZkConnector.Instance.GetChildren(node, watch);

            int step = nodey + (int)(h * 0.2);
            int linecounter = 1;
            step += (livenodes.Count() / 3) * (int)(h * 0.1);
            int wrow = -60;
            int db = -40;
            foreach (var item in livenodes)
            {
                if (String.Compare(node, "/configs/database_schema") == 0)
                {
                    IEnumerable<string> tablelockers = ZkConnector.Instance.GetChildren("/locks/"+item.ToString(), new PaintLockedTables(this, databaseschemax + 15, databaseschemay));
                    if (tablelockers.Count() > 0)
                    {
                        DrawButton(nodex + wrow, step, 60, 25, item.ToString(), name, KnownColor.Red, KnownColor.Red);
                        //i deca
                        IEnumerable<string> list = ZkConnector.
                            Instance.GetChildren("/configs/database_schema/" + item.ToString(), false);
                        foreach (string child in list)
                        {
                            DrawButton(nodex + wrow, step + (int)(h * 0.05), 60, 25, child, item.ToString(), KnownColor.Aqua, KnownColor.Aqua);
                            IEnumerable<string> listack = ZkConnector.
                                Instance.GetChildren("/configs/database_schema/" + 
                                item.ToString()+"/"+child.ToString(), false);
                            foreach(string ack in listack)
                            {
                                DrawButton(nodex + wrow + 45, step + (int)(h * 0.05), 60, 25, ack, item.ToString(), KnownColor.Azure, KnownColor.Azure);
                            }
                        }
                    }
                    else
                    {
                        DrawButton(nodex + wrow, step, 60, 25, item.ToString(), name, color, color);
                    }
                }
                else
                {
                    DrawButton(nodex + wrow, step, 60, 25, item.ToString(), name, color, color);
                }
                if (String.Compare(node, "/locks") == 0) 
                {
                    DisplayLockedForTable("/locks/"+item, nodex + wrow, step);
                }
                
                if (linecounter % 3 == 0)
                {
                    DrawLine(nodex + 34, step - 10, 68, 2, name);
                    DrawLine(nodex + 34 + 68, step - 10, 2, 10, name);
                    step -= (int)(h * 0.1);
                    wrow = -60;
                }
                else
                {
                    wrow += 60;
                }
                if ((linecounter - 2) % 6 == 0 || linecounter == 2)
                {
                    DrawLine(nodex + 34, nodey, 2, step - nodey, name);
                }
                if ((linecounter - 1) % 3 == 0 || linecounter == 1)
                {
                    DrawLine(nodex + 34, nodey, 2, step - nodey - 10, name);
                    DrawLine(nodex - 34, step - 10, 68, 2, name);
                    DrawLine(nodex - 34, step - 10, 2, 10, name);
                }

                linecounter++;
            }
        }

        private void LockNode()
        {
        }

        private RoundButton DrawButton(int x, int y, int width, int height, String text, string name, KnownColor color, KnownColor bcolor)
        {
            RoundButton rbtn = new RoundButton();
            rbtn.Name = name;
            rbtn.Width = width;
            rbtn.Height = height;
            rbtn.Location = new Point(x, y);
            rbtn.FlatStyle = FlatStyle.Flat;
            rbtn.FlatAppearance.BorderColor = Color.FromKnownColor(bcolor);
            rbtn.BackColor = Color.FromKnownColor(color);
            rbtn.Text = text;
            rbtn.BringToFront();
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => { this.Controls.Add(rbtn); }));
            }
            else
            {
                this.Controls.Add(rbtn);
            }

            return rbtn;
        }

        private void DrawLine(int x,int y, int width, int height, string name)
        {
            Label lbl = new Label();
            //lbl.Text = "--->";
            lbl.Name = name;
            lbl.SendToBack();
            lbl.BorderStyle = BorderStyle.None;
            lbl.BackColor = Color.FromKnownColor(KnownColor.ForestGreen);
            lbl.AutoSize = false;
            lbl.Width = width;
            lbl.Height = height;
            lbl.Location = new Point(x, y);
            lbl.SendToBack();
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => { this.Controls.Add(lbl); }));
            }
            else
            {
                this.Controls.Add(lbl);
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            this.Controls.Clear();
            this.LoadStatic();
        }
    }
}
