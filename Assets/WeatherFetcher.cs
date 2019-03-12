using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class WeatherFetcher : MonoBehaviour
{
    public string ApiKeyWeather = "774233a13a975bc8fcd4b6e894482ce9";
    public string ApiKeyGeoIp = "420786de36a519f50bdd36ea19c3d8d1a470ace60a6a8e764ac70e20";
    public string City = "Tokyo";
    public string Country = "Japan";
    public string Weather;
    public string Latitude;
    public string Longtitude;

    string SendHttpRequest(string uri)
    {
        var request = (HttpWebRequest) WebRequest.Create(uri);
        using (var response = (HttpWebResponse) request.GetResponse())
        using (var stream = response.GetResponseStream())
        using (var reader = new StreamReader(stream))
        {
            if ((int) response.StatusCode != 200)
            {
                throw new Exception($"Bad status code: {response.StatusCode}.");
            }
            return reader.ReadToEnd();
        }
    }

    // Use this for initialization
    void Start()
    {
        var uriGeoIp = $"https://api.ipdata.co/?api-key={ApiKeyGeoIp}";
        var jsonGeoIp = SendHttpRequest(uriGeoIp);
        var dictGeoIp = JObject.Parse(jsonGeoIp);
        Latitude = (string) dictGeoIp["latitude"];
        Longtitude = (string) dictGeoIp["longtitude"];

        Debug.Log("executed");

//        var uriWeather = $"http://api.openweathermap.org/data/2.5/weather?q={City},{Country}&appid={ApiKeyWeather}";
        var uriWeather = $"http://api.openweathermap.org/data/2.5/weather?lat={Latitude}&lon={Longtitude}&appid={ApiKeyWeather}";
        var jsonWeather = SendHttpRequest(uriWeather);
        var dictWeather = JObject.Parse(jsonWeather);
        Weather = (string) dictWeather["weather"][0]["main"];
    }


    // Update is called once per frame
    void Update() { }
}