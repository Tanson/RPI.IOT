using RPI.IOT.GPIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPI.Scheduler
{
    public interface ISensor
    {
        T GetDate<T>();
    }
}
