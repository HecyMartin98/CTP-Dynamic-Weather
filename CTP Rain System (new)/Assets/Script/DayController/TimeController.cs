using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeController : MonoBehaviour
{

    [SerializeField] private Light sun;
    [SerializeField] private float secondsInFullDay = 120f;

    [Range(0, 1)] [SerializeField] private float currentTimeOfDay = 0f;
    private float timeMultiplier = 1f;
    private float sunInitialIntensity = 1f;
    private int year = 364;

    public int dayOfYear = 0;
    public int currentYear = 2020;

    public enum seasonEnum
    {
        Winter,
        Spring,
        Summer,
        Autumn
    }
    seasonEnum activeSeason;

    [Serializable]
    public struct Seasons
    {
        public string name;
        public float sunRise;
        public float sunSet;
        public float nightDuration;
        public float dayOfSeasonStart;
        public float chanceOfRain;
        public float chanceOfSnow;
    }
    public Seasons[] seasons;

    void start()
    {
        
        sunInitialIntensity = sun.intensity;
    }

    void Update()
    {
        UpdateSun();
        

        currentTimeOfDay += (Time.deltaTime / secondsInFullDay) * timeMultiplier;
        //Increases day
        if (currentTimeOfDay >= 1)
        {
            currentTimeOfDay = 0;
            dayOfYear++;
            SeasonCheck();
        }

        if (dayOfYear >= year)
        {
            dayOfYear = 0;
            currentYear++;
        }
    }
    float intensityMultiplier = 1f;
    void UpdateSun()
    {
        sun.transform.localRotation = Quaternion.Euler((currentTimeOfDay * 360f) - 90, 170, 0);

       
        
        //SunRise
        if (currentTimeOfDay <= seasons[(int)activeSeason].sunRise)
        {
            intensityMultiplier = Mathf.Clamp01((currentTimeOfDay - 0.23f) * (1 / 0.02f));
        }
        //SunSet
        else if (currentTimeOfDay >= seasons[(int)activeSeason].sunSet)
        {
            intensityMultiplier = Mathf.Clamp01(1 - ((currentTimeOfDay - 0.73f) * (1 / 0.02f)));
        }

        
        sun.intensity = sunInitialIntensity * intensityMultiplier;
    }

    void SeasonCheck()
    {
        //winter =89 days Autumn =90 days Spring =93 and Summer=94
        //winter starts 10 days before the new year, so Winter = 79days then set it back to winter after Autumn
            
        if(seasons[(int)activeSeason].dayOfSeasonStart == dayOfYear)
        {
            activeSeason++;
        }
       
    }
}
