using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using CurtainDriver;

namespace CurtainWindows
{
    public partial class frmMain : Form
    {
        //CurtainDriver.Curtain curtain = new CurtainDriver.Curtain("COM4", 9600);
        private Curtain curtain;


        public frmMain()
        {
            InitializeComponent();
            BaudRate = 9600;
            PortName = "COM7";
            tbBaudRate.Text = BaudRate.ToString(CultureInfo.InvariantCulture);
            tbPortName.Text = PortName;

            CheckConnection();
            curtain.OnPositionChanged += CurtainPositionChangedHandler;

            string line;
            string[] linePrams;
            var mrl = new List<MovementRequest>();

            using (var file = new StreamReader(Environment.CurrentDirectory + "\\" + "MovementRequests.txt"))
            {
                while ((line = file.ReadLine()) != null)
                {
                    linePrams = line.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries);
                    if (linePrams.Length != 3)
                        continue;
                    var mr = new MovementRequest();
                    mr.Position = Convert.ToInt32(linePrams[2]);
                    mr.Day = (DayOfWeek) Convert.ToInt32(linePrams[0]);
                    mr.Time = TimeSpan.Parse(linePrams[1]);
                    mrl.Add(mr);
                    //MessageBox.Show(line);
                }

                file.Close();
            }

            if (mrl.Count > 0)
            {
                curtain.StartPending(mrl);
            }
        }

        public int BaudRate { get; set; }
        public string PortName { get; set; }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            tbPosition.Text = trackBarPosition.Value.ToString(CultureInfo.InvariantCulture);
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
            PortName = tbPortName.Text.Trim();
            int tmp;
            if (!int.TryParse(tbBaudRate.Text, out tmp))
            {
                toolStripStatusLabel.Text = @"Connection failed !!!";
                toolStripStatusLabel.ForeColor = Color.Red;
                return;
            }
            BaudRate = tmp;
            if (curtain != null)
            {
                curtain.Port.Dispose();
                curtain = null;
            }

            curtain = new Curtain(PortName, BaudRate);

            if (!curtain.Connect())
            {
                toolStripStatusLabel.Text = @"Connection failed!!!";
                toolStripStatusLabel.ForeColor = Color.Red;
            }
            else
            {
                toolStripStatusLabel.Text = @"Connected";
                toolStripStatusLabel.ForeColor = Color.Green;
                Thread.Sleep(1000);
                trackBarPosition.Value = curtain.GetPosition();
                tbPosition.Text = trackBarPosition.Value.ToString(CultureInfo.InvariantCulture);
            }
        }


        private void CurtainPositionChangedHandler(int newPosition)
        {
            trackBarPosition.Value = newPosition;
            tbPosition.Text = newPosition.ToString(CultureInfo.InvariantCulture);
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
            }
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show(@"Really close?", @"Confirm program termination", MessageBoxButtons.YesNo) ==
                DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}