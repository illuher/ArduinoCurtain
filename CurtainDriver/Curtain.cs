using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Threading;

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

        public Curtain(string name, int baudRate)
        {
            _port = new SerialPort(name, baudRate);
        }

        public bool IsConnected
        {
            get { return _port == null ? false : _port.IsOpen; }
        }

        public bool Connect()
        {
            try
            {
                _port.Open();
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
                _port.Close();
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

            _port.WriteLine(CMD_POSITION_GET);
            string pos = _port.ReadLine();
            Disconnect();
            int.TryParse(pos, out position);

            return position;
        }

        public void MoveToPosition(int newPosition, int speed = 5)
        {
            if (newPosition > POSITION_MAX || newPosition < POSITION_MIN)
            {
                return;
            }

            int diff = GetPosition() - newPosition;
            string direction = MOVE_LEFT;
            if (diff < 0)
            {
                direction = MOVE_RIGHT;
            }
            string command = CMD_MOVE + " " + direction + " " + speed + " " + Math.Abs(diff);

            Connect();
            _port.WriteLine(command);
            Disconnect();
        }

        public void StartPending(List<MovementRequest> list)
        {
            var t = new Thread(() =>
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
            });
            t.IsBackground = true;
            t.Start();
        }
    }
}