using RPI.IOT.I2C;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPI.Sensor.Sensors.Light
{
    public class BH1750Connection
    {
        public I2cDeviceConnection Connection { get; set; }
        public BH1750Connection(I2cDeviceConnection connection)
        {
            Connection = connection;
        }
     
        public void SetOff()
        {
            Connection.Write(0x00);
        }
        public void SetOn()
        {
            Connection.Write(0x01);
        }
        public void Reset()
        {
            Connection.Write(0x07);
        }
        
        public double GetData()
        {
            Connection.Write(0x10);
            var res = Connection.ReadByte();
            var data = ((res >> 8) & 0xff) | (res << 8) & 0xff00;
            return Math.Round(data / (2 * 1.2), 2);
        }
    }
}
