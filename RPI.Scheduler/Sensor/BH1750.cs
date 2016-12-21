using RPI.IOT.GPIO;
using RPI.IOT.I2C;
using RPI.Sensor.Sensors.Light;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPI.Scheduler.Sensor
{
    public class BH1750 : ISensor,II2C
    {
        public I2cDriver driver
        {
            get
            {
                return new I2cDriver(ConnectorPin.P1Pin03.ToProcessor(), ConnectorPin.P1Pin05.ToProcessor());
            }
        }

        public I2cDeviceConnection i2cConnection
        {
            get
            {
                return driver.Connect(0x23);
            }

        }
        public T GetDate<T>()
        {
            BH1750Connection light = new BH1750Connection(i2cConnection);
            light.Reset();
            light.SetOn();
            return (T)Convert.ChangeType(light.GetData(), typeof(T));
        }
    }
}
