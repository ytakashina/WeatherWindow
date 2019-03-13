using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowController : MonoBehaviour
{
    public WeatherFetcher WeatherFetcher;
    public Material GlassMaterial;
    public Texture TextureClearDay;
    public Texture TextureCloudsDay;
    public Texture TextureRainDay;
    public Texture TextureRaindrop;

    // Use this for initialization
    void Start()
    {
        WeatherFetcher = FindObjectOfType<WeatherFetcher>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (WeatherFetcher.Weather.ToLower())
        {
            case "clear":
                GlassMaterial.SetTexture("_MainTex", TextureClearDay);
                GlassMaterial.SetTexture("_MetallicGlossMap", null);
                break;
            case "clouds":
            case "atmosphere":
                GlassMaterial.SetTexture("_MainTex", TextureCloudsDay);
                GlassMaterial.SetTexture("_MetallicGlossMap", null);
                break;
            case "rain":
            case "snow":
            case "drizzle":
            case "thunderstorm":
                GlassMaterial.SetTexture("_MainTex", TextureRainDay);
                GlassMaterial.SetTexture("_MetallicGlossMap", TextureRaindrop);
                break;
        }
    }
}