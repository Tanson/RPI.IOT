using RPI.IOT.GPIO;
using RPI.Sensor.Devices.PiFaceDigital;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RPI.IOT.Test
{
    class Program
    {
        static DataConfig config = new DataConfig();
        static bool Sr501Status = false;
        static long time = 60 * 5;
        static long runcount = 0;
        static System.Threading.Timer threadTimer;
        static System.Threading.Timer threadSr501;
        static double bh1750Data = 0;
        static string TEMPData = "";
        static string HUMIData = "";
        static void Main(string[] args)
        {
            LoadConfig();

            RPI.Scheduler.Sensor.BH1750 bh1750 = new Scheduler.Sensor.BH1750();

            Scheduler.Sensor.DHT11 dht11 = new Scheduler.Sensor.DHT11();
            dht11.Pin = (ConnectorPin)config.DHT11PIN;

            ConnectorPin sr501Pin = (ConnectorPin)config.HCSR501PIN;
            var HCSR501 = GpioConnectionSettings.DefaultDriver;
            HCSR501.Allocate(sr501Pin.ToProcessor(), PinDirection.Input);
            //threadTimer= new System.Threading.Timer(new TimerCallback(callback), null, 1000, 1000 );
            while (true)
            {
                if (runcount >= time || runcount == 0)
                {
                    runcount = 1;
                    double data = bh1750.GetDate<double>();
                   // if(bh1750Data != data)
                   // {
                        bh1750Data = data;
                        string bh1750json = @"{""Type"":""BH1750"", ""PointName"":""" + config.PointName + "\", \"LUX\":" + data + "}";
                        AsyncCallSave(bh1750json);
                   // }
                    

                    string[] datas = dht11.GetValue();
                    if (string.IsNullOrEmpty(datas[0])) datas[0] = "0";
                    if (string.IsNullOrEmpty(datas[1])) datas[1] = "0";
                  //  if(HUMIData != datas[1] && TEMPData != datas[0])
                   // {
                        HUMIData = datas[1];
                        TEMPData = datas[0];
                        string dht11json = @"{""Type"":""DHT11"", ""PointName"":""" + config.PointName + "\", \"TEMP\":\"" + datas[0] + "\",\"HUMI\":\"" + datas[1] + "\"}";
                        AsyncCallSave(dht11json);
                   // }
                }
                runcount++;

                bool srstatus = HCSR501.Read(sr501Pin.ToProcessor());
                if (Sr501Status != srstatus)
                {
                    Sr501Status = srstatus;
                    string status = Sr501Status ? "1" : "0";
                    string sr501json = @"{""Type"":""SR501"", ""PointName"":"" " + config.PointName + "\", \"Status\":\"" + status + "\"}";
                    AsyncCallSave(sr501json);
                }
                if (srstatus)
                {

                }
                Thread.Sleep(1000);
            }
            //var led1 = ConnectorPin.P1Pin18.Output();
            //var connection = new GpioConnection(led1);
            //connection.Open();
            //for (var i = 0; i < 100; i++)
            //{
            //    connection.Toggle(led1);
            //    System.Threading.Thread.Sleep(250);
            //}

                //ConnectorPin sr501Pin = ConnectorPin.P1Pin23;
                //var HCSR501 = GpioConnectionSettings.DefaultDriver;
                //HCSR501.Allocate(sr501Pin.ToProcessor(), PinDirection.Input);

                //Scheduler.Sensor.DHT11 dht11 = new Scheduler.Sensor.DHT11();
                //dht11.Pin = ConnectorPin.P1Pin18;

                //RPI.Scheduler.Sensor.BH1750 bh1750 = new Scheduler.Sensor.BH1750();


                //int data = bh1750.GetDate<int>();
                //string bh1750json = @"{""Type"" =""BH1750"", ""PointName"" = " + config.PointName + ", \"LUX\" = " + data + "}";

                //string[] datas = dht11.GetValue();
                //string dht11json = @"{""Type"" =""DHT11"", ""PointName"" = " + config.PointName + ", \"TEMP\"=\"" + datas[0] + "\",\"HUMI\"=\""+ datas[1] + "\"}";

                //var isHigh = HCSR501.Read(sr501Pin.ToProcessor());
                //string status = isHigh ? "1" : "0";
                //string sr501json = @"{""Type"" =""SR501"", ""PointName"" = " + config.PointName + ", \"Status\"=\"" + status + "\"}";
                //Console.WriteLine("reading");


        }
        private static async Task<HttpResult> AsyncPostHtml(string url, string data)
        {
            HttpHelper http = new HttpHelper();
            HttpItem item = new HttpItem();
            item.URL = url;
            item.Method = "post";
            item.Allowautoredirect = false;
            item.Encoding = Encoding.Default;

            item.PostDataType = PostDataType.String;
            item.Postdata = data;
            item.ResultCookieType = ResultCookieType.CookieCollection;
            return http.GetHtml(item);
        }
        private static async Task<HttpResult> AsyncGetHtml(string url)
        {
            //Console.WriteLine(url);
            HttpHelper http = new HttpHelper();
            HttpItem item = new HttpItem();
            item.URL = url;
            item.Method = "get"; 
            item.Allowautoredirect = true;
            item.Encoding = Encoding.Default;
           // item.CookieCollection = Cookies; ;
            item.ResultCookieType = ResultCookieType.CookieCollection;
            return http.GetHtml(item);
        }
        private static async void AsyncCallSave(string json)
        {
            //Console.WriteLine("json:" + json);
            //Console.WriteLine(string.Format(config.Url, System.Web.HttpUtility.UrlEncode(json)));
             var rq=await AsyncGetHtml(string.Format(config.Url, System.Web.HttpUtility.UrlEncode(json)));
            //var rq = await AsyncGetHtml(string.Format(config.Url, json));
            //Console.WriteLine("status:" + rq.StatusCode);
            //Console.WriteLine("html:" + rq.Html);
        }
        private static void LoadConfig()
        {
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "config.xml"))
            {
                string xml;
                StreamReader reader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "config.xml");
                xml = reader.ReadToEnd();
                reader.Close();
                reader.Dispose();
                config = SerializeXml.DeserializeGetObject<DataConfig>(xml);
            }else
            {
                config = new DataConfig();
                StreamWriter writer = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "config.xml", false);
                writer.Write(SerializeXml.SerializeObjectToXML(config));
                writer.Close();
                writer.Dispose();
            }
        }
    }
}
