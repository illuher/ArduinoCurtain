using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
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
    }
}
