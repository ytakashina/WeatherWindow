using System.Collections;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class WeatherFetcher : MonoBehaviour
{
    public TextAsset ConfigFile;
    public string Weather;
    public string Latitude;
    public string Longtitude;

    // Use this for initialization
    IEnumerator Start()
    {
        const string uriGeolocation = "https://ipinfo.io/json";
        using (var www = new WWW(uriGeolocation))
        {
            yield return www;
            var dictGeolocation = JObject.Parse(www.text);
            var location = ((string) dictGeolocation["loc"]).Split(',');
            Latitude = location[0];
            Longtitude = location[1];
        }

        var apiKeyWeather = JObject.Parse(ConfigFile.text)["ApiKeyWeather"];
        var uriWeather = $"http://api.openweathermap.org/data/2.5/weather?lat={Latitude}&lon={Longtitude}&appid={apiKeyWeather}";
        using (var www = new WWW(uriWeather))
        {
            yield return www;
            var dictWeather = JObject.Parse(www.text);
            Weather = (string) dictWeather["weather"][0]["main"];
        }
    }


    // Update is called once per frame
    void Update() { }
}