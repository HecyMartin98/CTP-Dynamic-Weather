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

    private float rainDuration;
    private float rainCounterCD;

    public int dayOfYear = 0;
    public int currentYear = 2020;

    public ParticleSystem rain;

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
        
        if(rainDuration > 0f && rain.isEmitting)
        {
            rainDuration -= Time.deltaTime;
            Debug.Log("rain Duration" + rainDuration);
        }
        else if (rain.isEmitting)
        {
            rain.Stop();
        }


        if (rainCounterCD > 0f && !rain.isEmitting)
        {
            rainCounterCD -= Time.deltaTime;
           
        }
        else if (!rain.isEmitting)
        {
            RainChance();
            rainCounterCD = 9f;
        }

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
            //Debug.Log(seasons[(int)activeSeason].sunRise);
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
        
            float temp = Random.Range(0f, 100f);
            if (temp <= seasons[(int)activeSeason].chanceOfRain)
            {
                rain.Play();
                rainDuration = Random.Range(10f, 15f);
            }
        


        //if(!isRaining && rainCounterCD <= 0f)
        //{
        //    float temp = Random.Range(0f, 100f);
        //    Debug.Log(Random.Range(0f, 100f) + " " + ("chance of rain"));
        //    if(temp <= seasons[(int)activeSeason].chanceOfRain)
        //    {
        //        isRaining = true;
        //        rain.gameObject.SetActive(true);
        //        rainTimer -= Time.deltaTime;
        //    }


        //}
        //else if (rainTimer == 0f && isRaining == false)
        //{
        //    rainTimer = 10f;
        //    Debug.Log(("rain timer") + rainTimer);
        //}
        //else
        //{

        //    if (rainTimer < 0f)
        //    {
        //       isRaining = !isRaining;
        //        Debug.Log("is it raining ?" + isRaining);
        //        rain.gameObject.SetActive(false);
        //    }
        //}
    }  
}

