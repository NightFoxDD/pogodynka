using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Xamarin.Forms;

namespace pogodynka.Classes
{
    public class WeatherHourInfo
    {
        public string Day {  get; set; }
        public string Main_Temp {  get; set; }
        public string Wind_Speed {  get; set; }
        public string Weather_descritpion {  get; set; }
        public string Weather_Main {  get; set; }

        public ImageSource Weather_icon { get; set; }
        public WeatherHourInfo(List weatherInfo) 
        {
            Weather_icon = ImageSource.FromUri(new Uri("https://openweathermap.org/img/wn/" + weatherInfo.weather[0].icon + ".png"));
            Weather_descritpion = weatherInfo.weather[0].description;
            Main_Temp = (weatherInfo.main.temp - 273.15).ToString("F1");
            Day = convertDateTime(weatherInfo.dt).ToShortDateString() + ", " + convertDateTime(weatherInfo.dt).ToShortTimeString();
            Wind_Speed = weatherInfo.wind.speed.ToString();
            Weather_Main = weatherInfo.weather[0].main;
        }
        DateTime convertDateTime(long sec)
        {
            DateTime day = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc).ToLocalTime();
            day = day.AddSeconds(sec).ToLocalTime();
            return day;
        }
    }
}
