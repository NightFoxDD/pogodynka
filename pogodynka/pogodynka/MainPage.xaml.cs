using Newtonsoft.Json;
using pogodynka.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
namespace pogodynka
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        string APIKey = "bf1976788e22f85a60dce47366370577";
        private void Button_Clicked(object sender, EventArgs e)
        {
            getWeather();
            SB.Text = "";
        }
        public void getWeather()
        {
            if(SB.Text == "")
            {
                return;
            }
            using(WebClient  web = new WebClient())
            {
                string url_current = string.Format("https://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}", SB.Text, APIKey);
                string json_current = "";
                string url_longTerm = string.Format("http://api.openweathermap.org/data/2.5/forecast?q={0}&appid={1}", SB.Text, APIKey);
                string json_longTerm = "";
                try
                {
                    json_current = web.DownloadString(url_current);
                    json_longTerm = web.DownloadString(url_longTerm);
                }
                catch (Exception) { DisplayAlert("sadf","asdf","ok"); return; }
                WeatherInfo.Root  Info_current = JsonConvert.DeserializeObject<WeatherInfo.Root>(json_current);
                WeatherForcast Weather_LongTerm = JsonConvert.DeserializeObject<WeatherForcast>(json_longTerm);
                List<WeatherHourInfo> Info_LongTerm = new List<WeatherHourInfo>();
                foreach (List info in Weather_LongTerm.list)
                {
                    Info_LongTerm.Add(new WeatherHourInfo(info));
                }
                CV_Weather_LongTerm.ItemsSource = Info_LongTerm;
                I_WeatherIcon.Source = ImageSource.FromUri(new Uri("https://openweathermap.org/img/wn/" + Info_current.weather[0].icon + ".png"));
                Lbl_Temp.Text = (Info_current.main.temp - 273.15).ToString("F1");
                Lbl_Max_Temp.Text = (Info_current.main.temp_max - 273.15).ToString("F1");
                Lbl_Min_Temp.Text = (Info_current.main.temp_min - 273.15).ToString("F1");
                SearchedCity.Text = SB.Text;
                Condition.Text = Info_current.weather[0].main;
                Sunset.Text = convertDateTime(Info_current.sys.sunset).ToShortTimeString();
                Sunrise.Text = convertDateTime(Info_current.sys.sunrise).ToShortTimeString();
                Windspeed.Text = Info_current.wind.speed.ToString();
                Pressure.Text = Info_current.main.pressure.ToString();
            }
        }
        DateTime convertDateTime(long sec)
        {
            DateTime day = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc).ToLocalTime();
            day = day.AddSeconds(sec).ToLocalTime();
            return day;
        }
    }
}
