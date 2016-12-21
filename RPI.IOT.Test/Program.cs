using RPI.IOT.GPIO;
using RPI.Sensor.Devices.PiFaceDigital;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPI.IOT.Test
{
    class Program
    {
        static void Main(string[] args)
        {

            //var led1 = ConnectorPin.P1Pin18.Output();
            //var connection = new GpioConnection(led1);
            //connection.Open();
            //for (var i = 0; i < 100; i++)
            //{
            //    connection.Toggle(led1);
            //    System.Threading.Thread.Sleep(250);
            //}

            Console.WriteLine(DateTime.Now.ToString());
            RPI.Scheduler.Sensor.BH1750 bh1750 = new Scheduler.Sensor.BH1750();
            int data = bh1750.GetDate<int>();
            Console.WriteLine(data);


            Console.WriteLine(DateTime.Now.ToString());
            Scheduler.Sensor.DHT11 dht11 = new Scheduler.Sensor.DHT11();
            dht11.Pin = ConnectorPin.P1Pin18;
            // var dht11Data = dht11.GetDate<string>();
           
            Console.WriteLine(dht11.GetDate<string>());
            Console.WriteLine(DateTime.Now.ToString());
        }
    }
}
