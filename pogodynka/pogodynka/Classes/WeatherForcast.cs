using System;
using System.Collections.Generic;
using System.Text;

namespace pogodynka.Classes
{
    public class WeatherForcast
    {
        public List<List> list {  get; set; }
        
    }
    public class Main
    {
        public double temp { get; set; }
    }
    public class Weather
    {
        public string main {  get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }
    public class Wind
    {
        public double speed { get; set; }
    }
    public class List
    {
        public long dt {  get; set; }
        public Main main { get; set; }
        public List<Weather> weather { get; set; }
        public Wind wind { get; set; }
    }
}
