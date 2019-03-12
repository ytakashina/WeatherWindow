using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowController : MonoBehaviour
{
    public Material WindowMaterial;
    public Texture TextureClearDay;
    public Texture TextureCloudsDay;
    public Texture TextureRainDay;

    // Use this for initialization
    void Start()
    {
        var weatherFetcher = GameObject.Find("WeatherFetcher").GetComponent<WeatherFetcher>();
        switch (weatherFetcher.Weather)
        {
            case "Clear":
                WindowMaterial.SetTexture("ClearDay", TextureClearDay);
                break;
            case "Clouds":
            case "Atmosphere":
                WindowMaterial.SetTexture("CloudsDay", TextureCloudsDay);
                break;
            case "Rain":
            case "Snow":
            case "Drizzle":
            case "Thunderstorm":
                WindowMaterial.SetTexture("RainDay", TextureRainDay);
                break;
        }
        //        
    }

    // Update is called once per frame
    void Update() { }
}