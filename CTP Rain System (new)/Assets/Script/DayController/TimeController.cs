using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TimeController : MonoBehaviour
{

    [SerializeField] private Light sun;
    [SerializeField] private float secondsInFullDay = 120f;

    [Range(0, 1)] [SerializeField] private float currentTimeOfDay = 0f;
    private float timeMultiplier = 1f;
    private float sunInitialIntensity = 1f;

    private int year = 364;

    private float rainTimer = 10f;

    public int dayOfYear = 0;
    public int currentYear = 2020;

    bool isRaining = false;

    public enum seasonEnum
    {
        Winter,
        Spring,
        Summer,
        Autumn
    }
    seasonEnum activeSeason;

    [System.Serializable]
    public struct Seasons
    {
        public string name;
        public float sunRise;
        public float sunSet;
        public float nightDuration;
        public float dayOfSeasonEnd;
        public float chanceOfRain;
        public float chanceOfSnow;
    }
    public Seasons[] seasons;

    void Start()
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
            RainChance();
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
            intensityMultiplier = Mathf.Clamp01((currentTimeOfDay - seasons[(int)activeSeason].sunRise) * (1 / 0.02f));
            //Debug.Log(seasons[(int)activeSeason].sunRise);
        }
        //SunSet
        else if (currentTimeOfDay >= seasons[(int)activeSeason].sunSet)
        {
            intensityMultiplier = Mathf.Clamp01(1 - ((currentTimeOfDay - seasons[(int)activeSeason].sunSet) * (1 / 0.02f)));
        }

        
        sun.intensity = sunInitialIntensity * intensityMultiplier;
    }

    void SeasonCheck()
    {
        //winter =89 days Autumn =90 days Spring =93 and Summer=94
        //winter starts 10 days before the new year, so Winter = 79days then set it back to winter after Autumn
        //Debug.Log(seasons[(int)activeSeason].dayOfSeasonEnd.ToString() + " " + dayOfYear.ToString());
        if(seasons[(int)activeSeason].dayOfSeasonEnd == dayOfYear)
        {
            if(activeSeason != seasonEnum.Autumn)
            {
                activeSeason++;
                //Debug.Log(activeSeason.ToString());
            }
            else
            {
                activeSeason = seasonEnum.Winter;
            }
        }
       
    }

    void RainChance()
    {
        if(!isRaining)
        {
            float temp = Random.Range(0f, 100f);
            if(temp < seasons[(int)activeSeason].chanceOfRain)
            {
                isRaining = true;
                
            }
            

        }
        else if (rainTimer > 0f)
        {
            rainTimer = Random.Range(12f,20f);
        }
        else
        {
            rainTimer -= Time.deltaTime;
            if(rainTimer < 0f)
            {
               isRaining = !isRaining;
            }
        }
    }
}
