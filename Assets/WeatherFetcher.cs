using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class WeatherFetcher : MonoBehaviour
{
    public string ApiKey = "774233a13a975bc8fcd4b6e894482ce9";
    public string City = "Tokyo";
    public string Country = "Japan";

    // Use this for initialization
    void Start()
    {
        var url = $"http://api.openweathermap.org/data/2.5/weather?q={City},{Country}&appid={ApiKey}";
        var request = (HttpWebRequest) WebRequest.Create(url);
        using (var response = (HttpWebResponse)request.GetResponse())
        using (var stream = response.GetResponseStream())
        using (var reader = new StreamReader(stream))
        {
            var json = reader.ReadToEnd();
            var dict = JObject.Parse(json);
            Debug.Log(dict["weather"][0]["id"]);
            Debug.Log(dict["weather"][0]["main"]);
        }
    }


// Update is called once per frame
    void Update() { }
}