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
                return new I2cDriver(ProcessorPin.Pin02, ProcessorPin.Pin03);
            }
        }

        public I2cDeviceConnection i2cConnection
        {
            get
            {
                return driver.Connect(0x68);
            }

        }
        public T GetDate<T>()
        {
            BH1750Connection light = new BH1750Connection(i2cConnection);
            light.SetOn();
            return (T)Convert.ChangeType(light.GetData(), typeof(T));
        }
    }
}
