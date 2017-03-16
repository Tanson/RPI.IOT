using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPI.IOT.Test
{
    public class DataConfig
    {
        public int DHT11PIN { get; set; }= 6;
        public int HCSR501PIN { get; set; } = 5;
        public string PointName { get; set; } = "客厅";
        public string Url { get; set; } = "http://192.100.100.8:88/API/MonitorData.ashx?data={0}";
    }
}
