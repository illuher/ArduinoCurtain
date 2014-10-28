using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CurtainDriver
{
    public class Curtain
    {
        public const string CMD_POSITION_GET = "p";
        public const string CMD_POSITION_SET = "s";
        public const string CMD_MOVE = "m";

        public const int POSITION_MIN = 0;
        public const int POSITION_MAX = 1050;

        public const string MOVE_LEFT = "114";
        public const string MOVE_RIGHT = "108";

        public SerialPort _port;

        public bool IsConnected { get { return this._port == null ? false : this._port.IsOpen; } }

        public Curtain(string name, int baudRate)
        {
            this._port = new SerialPort(name, baudRate);
        }

        public bool Connect()
        {
            try
            {
                this._port.Open();
            }
            catch (Exception)
            {
            }
            return this.IsConnected;
        }

        public void Disconnect()
        {
            try
            {

                this._port.Close();
            }
            catch (Exception)  { }
        }

        public int GetPosition()
        {
            this.Connect();
            int position = -1;

            if (!this.IsConnected)
            {
                this.Disconnect();
                return -1;
            }

            this._port.WriteLine(Curtain.CMD_POSITION_GET);
            string pos = this._port.ReadLine();
            this.Disconnect();
            int.TryParse(pos, out position);

            return position;
        }

        public void MoveToPosition(int newPosition, int speed = 5)
        {
            if (newPosition > Curtain.POSITION_MAX || newPosition < Curtain.POSITION_MIN)
            {
                return;
            }
           
            int diff = this.GetPosition() - newPosition;
            string direction = Curtain.MOVE_LEFT;
            if (diff < 0)
            {
                direction = Curtain.MOVE_RIGHT;
            }
            string command = Curtain.CMD_MOVE + " " + direction + " " + speed + " " + Math.Abs(diff);

            this.Connect();
            this._port.WriteLine(command);
            this.Disconnect();
        }

        public void StartPending(List<MovementRequest> list)
        {
            Thread t = new Thread(()=>{
                while (true)
                {
                    var next = from x in list
                               where x.Day == DateTime.Now.DayOfWeek && x.Time >= DateTime.Now - DateTime.Today
                               orderby x.Time ascending
                               select x;

                    if (next != null && next.Count() > 0)
                    {
                        MovementRequest r = next.First();
                        Thread.Sleep(DateTime.Today + r.Time - DateTime.Now);
                        try
                        {
                            this.MoveToPosition(r.Position);
                        }
                        catch (Exception) { }
                        Console.WriteLine(r.Position);
                    }
                    else
                    {
                        Thread.Sleep(DateTime.Today.AddDays(1) - DateTime.Now);
                    }
                }
            });
            t.IsBackground = true;
            t.Start();
        }


    }
}
