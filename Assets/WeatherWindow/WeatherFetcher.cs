using Newtonsoft.Json.Linq;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Assertions;

public class WeatherFetcher : MonoBehaviour
{
    public TextAsset ConfigFile;
    public float RequestInterval = 60.0f;
    public string Weather;
    public float Latitude;
    public float Longtitude;
    private JToken _apiKeyWeather;

    // Use this for initialization
    void Start()
    {
        _apiKeyWeather = JObject.Parse(ConfigFile.text)["ApiKeyWeather"];
        StartCoroutine(FetchGeolocation());
        StartCoroutine(RepeatFetchWeather());
    }

    private static IEnumerator HandleWebRequest(UnityWebRequest request)
    {
        yield return request.SendWebRequest();
        if (request.isNetworkError)
            Debug.LogError($"Error: {request.error}");
        if (request.isHttpError)
            Debug.LogError($"Status code: {request.responseCode}");
    }

    private IEnumerator FetchGeolocation()
    {
        const string uriGeolocation = "https://ipinfo.io/json";
        using (var request = UnityWebRequest.Get(uriGeolocation))
        {
            yield return HandleWebRequest(request);
            var dictGeolocation = JObject.Parse(request.downloadHandler.text);
            var location = ((string) dictGeolocation["loc"]).Split(',');
            Latitude = float.Parse(location[0]);
            Longtitude = float.Parse(location[1]);
        }
    }

    private IEnumerator FetchWeather()
    {
        Assert.IsTrue(Latitude >= -90 && Latitude <= 90, $"Invalid Latitude: {Latitude}. Latitude must be in [-90, 90].");
        Assert.IsTrue(Longtitude >= -180 && Longtitude <= 180, $"Invalid Longtitude: {Longtitude}. Longtitude must be in [-180, 180].");
        var uriWeather = $"http://api.openweathermap.org/data/2.5/weather?lat={Latitude}&lon={Longtitude}&appid={_apiKeyWeather}";
        using (var request = UnityWebRequest.Get(uriWeather))
        {
            yield return HandleWebRequest(request);
            var dictWeather = JObject.Parse(request.downloadHandler.text);
            Weather = (string) dictWeather["weather"][0]["main"];
        }
    }


    private IEnumerator RepeatFetchWeather()
    {
        while (true)
        {
            StartCoroutine(FetchWeather());
            yield return new WaitForSeconds(RequestInterval);
        }
    }

    // Update is called once per frame
    void Update() { }
}