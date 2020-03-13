using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSEditor : MonoBehaviour
{

    public ParticleSystem rainPS;
    public ParticleSystem.EmissionModule emissionModule;

    public Material skybox1;

    public Light drtLight;
    public GameObject rainSystem;
    public GameObject snowSystem;
    //public GameObject fogSystem;

    


    void Start()
    {
        rainSystem.SetActive(false);
        snowSystem.SetActive(false);

        RenderSettings.fog = false;

        rainPS = GetComponent<ParticleSystem>();
        emissionModule = rainPS.emission;

        //GetValue();
        //SetValue();

    }

    void Update()
    {
      
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            rainSystem.SetActive(true);
            snowSystem.SetActive(false);
            
           // fogSystem.SetActive(true);
            RenderSettings.fog = true;
            drtLight.intensity = 0.25f;
            RenderSettings.skybox = skybox1;
            //emissionModule.rateOverTime = 60;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.tag == "Player")
        {
            rainSystem.SetActive(false);
            snowSystem.SetActive(true);
        }
        
    }

   

    //void GetValue()
    //{
    //    print(string.Format("The constant values are: min {0} max {1}.", emissionModule.rate.constantMin, emissionModule.rate.constantMax));
    //}

    //void SetValue()
    //{
    //    emissionModule.rate = new ParticleSystem.MinMaxCurve(0.0f, 10.0f);
    //}


}
