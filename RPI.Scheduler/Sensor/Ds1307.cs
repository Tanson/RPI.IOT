using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPI.IOT.GPIO;
using RPI.IOT.I2C;
using RPI.Sensor.Clocks.Ds1307;

namespace RPI.Scheduler.Sensor
{
    public class Ds1307 : ISensor,II2C
    {
        private Ds1307Connection clock;

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
            
            clock = new Ds1307Connection(i2cConnection);
            return (T)Convert.ChangeType(clock.GetDate(), typeof(T));
        }
    }
}
