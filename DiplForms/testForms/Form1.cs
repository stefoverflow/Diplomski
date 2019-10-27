using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using test;
using test.Connection;
using testForms.Distributed_Lock;
using ZooKeeperNet;
using System.Management;
using System.Threading;

namespace testForms
{
    public partial class Form1 : Form
    {
        ZLock zl;
        string sqnode;
        public Form1()
        {
            InitializeComponent();
            zl = new ZLock("/locks/dbo.Product","");

            // jedna baza ne replicirana
            // vise servera tj REST API koji su sinhronizovani zajedno
            // bitno je da requestovi se izvrsavaju po redosledu dostizanja
            // idu sekvencijalni i privremeni na tabelu kad izvrsava svi REST zahtevi

            // najveci problem kvo ako je zakljucano 
            // nije problem da izvrsi querity ono ce je sinhronizovano
            // problem je kako da vrne lepo na rutu dole kod return 
            // kvo trebe i kad dojde red na njega

            // kako onda da se izvrsi rutata, kvo se desava, kako da kad okine watcherat vrnem 
            // sto izvrsi tamo a da do tad ga saceka 

            // bilo bi dobro kad bi se stavljali requesti u red i tako redom se i izvrsavaju
            // prema bazu i tad i izvrsi request kad se izvrse prethodni
            // al kako da saceka da se prethodniti izvrse ako je lockovano 
            // pa da dobije lock pa da on izvrsi

            AutoResetEvent waithandle = new AutoResetEvent(false);

            string q="query";
            OrderWatcher o = new OrderWatcher(q);
            //Task.WaitAll()
        }

        private void btnLock_Click(object sender, EventArgs e)
        {
            sqnode = zl.Lock(tbxQuery.Text);
        }

        private void btnUnlock_Click(object sender, EventArgs e)
        {
            zl.UnLock(sqnode);
        }
    }
}
