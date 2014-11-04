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
    public partial class FrmMain : Form
    {
        //CurtainDriver.Curtain curtain = new CurtainDriver.Curtain("COM4", 9600);
        private Curtain _curtain;


        public FrmMain()
        {
            InitializeComponent();
            BaudRate = 9600;
            PortName = "COM7";
            tbBaudRate.Text = BaudRate.ToString(CultureInfo.InvariantCulture);
            tbPortName.Text = PortName;

            CheckConnection();
            _curtain.OnPositionChanged += CurtainPositionChangedHandler;

            var mrl = new List<MovementRequest>();

            using (var file = new StreamReader(Environment.CurrentDirectory + "\\" + "MovementRequests.txt"))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    string[] linePrams = line.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries);
                    if (linePrams.Length != 3)
                        continue;
                    var mr = new MovementRequest
                    {
                        Position = Convert.ToInt32(linePrams[2]),
                        Day = (DayOfWeek) Convert.ToInt32(linePrams[0]),
                        Time = TimeSpan.Parse(linePrams[1])
                    };
                    mrl.Add(mr);
                    //MessageBox.Show(line);
                }

                file.Close();
            }

            if (mrl.Count > 0)
            {
                _curtain.StartPending(mrl);
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

            _curtain.MoveToPosition(trackBarPosition.Value);
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
            if (_curtain != null)
            {
                _curtain.Port.Dispose();
                _curtain = null;
            }

            _curtain = new Curtain(PortName, BaudRate);

            if (!_curtain.Connect())
            {
                toolStripStatusLabel.Text = @"Connection failed!!!";
                toolStripStatusLabel.ForeColor = Color.Red;
            }
            else
            {
                toolStripStatusLabel.Text = @"Connected";
                toolStripStatusLabel.ForeColor = Color.Green;
                Thread.Sleep(1000);
                trackBarPosition.Value = _curtain.GetPosition();
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