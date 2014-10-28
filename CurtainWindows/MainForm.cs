﻿using CurtainDriver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CurtainWindows
{
    public partial class frmMain : Form
    {
        public int BaudRate { get; set; }
        public string PortName { get; set; }

        //CurtainDriver.Curtain curtain = new CurtainDriver.Curtain("COM4", 9600);
        CurtainDriver.Curtain curtain;


        public frmMain()
        {


            InitializeComponent();
            this.BaudRate = 9600;
            this.PortName = "COM7";
            this.tbBaudRate.Text = this.BaudRate.ToString();
            this.tbPortName.Text = this.PortName;

            CheckConnection();

            string line;
            string[] linePrams;
            List<MovementRequest> mrl = new List<MovementRequest>();

            using (System.IO.StreamReader file = new System.IO.StreamReader(Environment.CurrentDirectory + "\\" + "MovementRequests.txt"))
            {
                while ((line = file.ReadLine()) != null)
                {
                    linePrams = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    if (linePrams.Length != 3)
                        continue;
                    MovementRequest mr = new MovementRequest();
                    mr.Position = Convert.ToInt32(linePrams[2]);
                    mr.Day = (DayOfWeek)Convert.ToInt32(linePrams[0]);
                    mr.Time = TimeSpan.Parse(linePrams[1]);
                    mrl.Add(mr);
                    MessageBox.Show(line);
                }

                file.Close();
            }

            if (mrl.Count > 0)
            {
                this.curtain.StartPending(mrl);
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            this.tbPosition.Text = trackBarPosition.Value.ToString();
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(trackBarPosition.Value.ToString());

            curtain.MoveToPosition(trackBarPosition.Value);

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            CheckConnection();
        }

        private void CheckConnection()
        {
            this.PortName = tbPortName.Text.Trim();
            int tmp;
            if (!int.TryParse(tbBaudRate.Text, out tmp))
            {
                toolStripStatusLabel.Text = "Connection failed !!!";
                toolStripStatusLabel.ForeColor = Color.Red;
                return;
            }
            this.BaudRate = tmp;
            if (this.curtain != null)
            {
                this.curtain._port.Dispose();
                this.curtain = null;
            }

            this.curtain = new CurtainDriver.Curtain(this.PortName, this.BaudRate);
            
            if (!curtain.Connect())
            {
                toolStripStatusLabel.Text = "Connection failed!!!";
                toolStripStatusLabel.ForeColor = Color.Red;
            }
            else
            {
                toolStripStatusLabel.Text = "Connected";
                toolStripStatusLabel.ForeColor = Color.Green;
                Thread.Sleep(1000);
                trackBarPosition.Value = curtain.GetPosition();
                this.tbPosition.Text = trackBarPosition.Value.ToString();
            }
        }
    }
}
