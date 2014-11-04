using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Threading;

namespace CurtainDriver
{
    public delegate void PositionChanged(int newPosition);

    public class Curtain
    {
        public const string CMD_POSITION_GET = "p";
        public const string CMD_POSITION_SET = "s";
        public const string CMD_MOVE = "m";

        public const int POSITION_MIN = 0;
        public const int POSITION_MAX = 1050;

        public const string MOVE_LEFT = "114";
        public const string MOVE_RIGHT = "108";

        public SerialPort Port;

        public event PositionChanged OnPositionChanged;

        public Curtain(string name, int baudRate)
        {
            Port = new SerialPort(name, baudRate);
        }

        public bool IsConnected
        {
            get { return Port != null && Port.IsOpen; }
        }

        public bool Connect()
        {
            try
            {
                Port.Open();
            }
            catch (Exception)
            {
            }
            return IsConnected;
        }

        public void Disconnect()
        {
            try
            {
                Port.Close();
            }
            catch (Exception)
            {
            }
        }

        public int GetPosition()
        {
            Connect();
            int position = -1;

            if (!IsConnected)
            {
                Disconnect();
                return -1;
            }

            Port.WriteLine(CMD_POSITION_GET);
            string pos = Port.ReadLine();
            Disconnect();
            if (int.TryParse(pos, out position))
            {
                if (this.OnPositionChanged != null)
                    this.OnPositionChanged.BeginInvoke(position, null, null);
            }

            return position;
        }

        public void MoveToPosition(int newPosition, int speed = 5)
        {
            if (newPosition > POSITION_MAX || newPosition < POSITION_MIN)
            {
                return;
            }

            int currentPosition = GetPosition();
            if(currentPosition<0)
                return;

            int diff = currentPosition - newPosition;
            string direction = MOVE_LEFT;
            if (diff < 0)
            {
                direction = MOVE_RIGHT;
            }
            string command = CMD_MOVE + " " + direction + " " + speed + " " + Math.Abs(diff);

            Connect();
            Port.WriteLine(command);
            Disconnect();
        }

        public void StartPending(List<MovementRequest> list)
        {
            Thread t = new Thread(() =>
            {
                while (true)
                {
                    IOrderedEnumerable<MovementRequest> next = from x in list
                        where x.Day == DateTime.Now.DayOfWeek && x.Time >= DateTime.Now - DateTime.Today
                        orderby x.Time ascending
                        select x;

                    if (next.Any())
                    {
                        MovementRequest r = next.First();
                        Thread.Sleep(DateTime.Today + r.Time - DateTime.Now);
                        try
                        {
                            MoveToPosition(r.Position);
                        }
                        catch (Exception)
                        {
                        }
                        //Console.WriteLine(r.Position);
                    }
                    else
                    {
                        Thread.Sleep(DateTime.Today.AddDays(1) - DateTime.Now);
                    }
                }
                return;
            }) {IsBackground = true};
            t.Start();
        }
    }
}