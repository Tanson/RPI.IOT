using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPI.IOT.GPIO;
using RPI.Sensor.Sensors.Temperature.Dht;

namespace RPI.Scheduler.Sensor
{
    public class DHT11 : ISensor
    {
        private ConnectorPin _pin;
        public ConnectorPin Pin
        {
            get
            {
                return _pin;
            }

            set
            {
                _pin = value;
            }
        }

        public T GetDate<T>()
        {
            string strTemplate = "";
            var driver = GpioConnectionSettings.GetBestDriver(GpioConnectionDriverCapabilities.CanChangePinDirectionRapidly);
            using (var pin = driver.InOut(_pin))
            using (var dhtConnection = new Dht11Connection(pin))
            {
                var data = dhtConnection.GetData();
                if(data!=null)
                //strTemplate = data.RelativeHumidity.Percent + "," + data.Temperature.DegreesCelsius + "," + data.AttemptCount;
                strTemplate = string.Format("{0:0.00}% humidity, {1:0.0}°C, {2} attempts", data.RelativeHumidity.Percent, data.Temperature.DegreesCelsius, data.AttemptCount);
              
            }
            return (T)Convert.ChangeType(strTemplate, typeof(T)); ;
        }
    }
}
