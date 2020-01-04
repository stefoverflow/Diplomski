using DiplAPI.Connection;
using Master_Worker_Library.Client;
using Master_Worker_Library.Worker;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientForm
{
    public partial class Form1 : Form
    {
        Worker w;
        public Form1()
        {
            InitializeComponent();

            w = new Worker();
            w.EnterQueue();

            lblWorkerState.Text = "Worker state: " + w.GetWorkerState().ToString();

            //lblWorkerState.TextChanged = new EventHandler();
            
        }
    }
}
