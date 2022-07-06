using System.Text.Json;
using System.Text.Json.Serialization;
using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using System.Collections.Generic;

namespace SerializeToFile
{
    public class WeatherForecast
    {
        public DateTimeOffset Date { get; set; }
        public int TemperatureCelsius { get; set; }
        public string? Summary { get; set; }
        public int? Night { get; set; }
        public List<string> Names { get; set; }
    }

    public class Program
    {
        public static void Main()
        {
            WeatherForecast wf = new WeatherForecast
            {
                Date = DateTime.Parse("2019-08-01"),
                TemperatureCelsius = 25,
                Summary = "Hot",
                Night =  12323445,
                Names = new List<string>() {
                    "dami",
                    "thimo",
                    "sam",
                    "fedor",
                    "rick",
                    "jorn",
                    "victor",
                    "piet",
                    "henk"
                    }
            };

            string fileName = "WeatherForecast.json";
            //string jsonString = JsonSerializer.Serialize(weatherForecast);
            using (StreamWriter file = new StreamWriter(fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, wf);
            }     
            
            

            Debug.Log(File.ReadAllText(fileName));
        }

        public static void Des()
        {
            string fileName = "WeatherForecast.json";
            string Data = File.ReadAllText(fileName);


            WeatherForecast wef = JsonConvert.DeserializeObject<WeatherForecast>(Data);

            Debug.Log(wef.Date);
        }
    }
}
// output:
//{"Date":"2019-08-01T00:00:00-07:00","TemperatureCelsius":25,"Summary":"Hot"}