using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{

    [SerializeField] private Light sun;
    [SerializeField] private float secondsInFullDay = 120f;

    [Range(0, 1)] [SerializeField] private float currentTimeOfDay = 0f;
    private float timeMultiplier = 1f;
    private float sunInitialIntensity;
    private int year = 364;

    public float dayOfYear = 0f;
    public float currentYear = 2020f;

    public struct Seasons
    {
        public float sunRise = 0.23f;
        public float SunSete = 0.73f; 
    }

    //Need Array of seasons
    //make a struct called seasons, make it serializable, Have two floats sunrise sunset. later on add % snow and rain, intensity and emission



    void start()
    {
        sunInitialIntensity = sun.intensity;
    }

    void Update()
    {
        UpdateSun();
        SeasonManager();

        currentTimeOfDay += (Time.deltaTime / secondsInFullDay) * timeMultiplier;

        if (currentTimeOfDay >= 1f)
        {
            currentTimeOfDay = 0f;
            dayOfYear++;
        }

        if (dayOfYear >= year)
        {
            dayOfYear = 0f;
            currentYear++;
        }
    }

    void UpdateSun()
    {
        sun.transform.localRotation = Quaternion.Euler((currentTimeOfDay * 360f) - 90, 170, 0);

        float intensityMultiplier = 1;
        //NightTime
        if (currentTimeOfDay <= 0.23f || currentTimeOfDay >= 0.75f)
        {
            intensityMultiplier = 0;
        }
        //SunRise
        else if (currentTimeOfDay <= 0.25f)
        {
            intensityMultiplier = Mathf.Clamp01((currentTimeOfDay - 0.23f) * (1 / 0.02f));
        }
        //SunSet
        else if (currentTimeOfDay >= 0.73f)
        {
            intensityMultiplier = Mathf.Clamp01(1 - ((currentTimeOfDay - 0.73f) * (1 / 0.02f)));
        }

        sun.intensity = sunInitialIntensity * intensityMultiplier;
    }

    //void SeasonManager()
    //{
    //    //winter =89 days Autumn =90 days Spring =93 and Summer=94
    //    //winter starts 10 days before the new year, so Winter = 79days then set it back to winter after Autumn

    //    if (dayOfYear <=79f)
    //    {
    //        CurrentSeason Spring;
    //    }

    //    else if (dayOfYear <= 172)
    //    {
    //        CurrentSeason Summer;
    //    }

    //    else if (dayOfYear <= 266)
    //    {
    //        CurrentSeason Autmn;
    //    }

    //    else if (dayOfYear <= 356)
    //    {
    //        CurrentSeason Winter;
    //    }
    //}
}
