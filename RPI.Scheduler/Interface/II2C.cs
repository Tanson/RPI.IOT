using RPI.IOT.I2C;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPI.Scheduler
{
    public interface II2C
    {
        I2cDriver driver { get;}
        I2cDeviceConnection i2cConnection { get;  }
    }
}
