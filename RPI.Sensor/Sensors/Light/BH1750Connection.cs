﻿using RPI.IOT.I2C;
using RPI.IOT.RPISystem.Timers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPI.Sensor.Sensors.Light
{/// <summary>
 ///  sample:
 ///   BH1750Connection light = new BH1750Connection(i2cConnection);
    //            light.Reset();
    //            light.SetOn();
    //   var data=light.GetData();
    /// </summary>
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
            HighResolutionTimer.Sleep(TimeSpanUtility.FromMicroseconds(150 * 1000));
            byte[] readBuf = Connection.Read(2);

            var valf = readBuf[0] << 8;
            valf |= readBuf[1];
            //  return valf / 1.2 * (69 / 69) / 1;
            return valf;
            // var valf = ((readBuf[0] << 8) | readBuf[1]) / 1.2;
            // return valf;

            // return Math.Round(valf / (2 * 1.2), 2);

        }

    }
}
