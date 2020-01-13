using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSEditor : MonoBehaviour
{

    public ParticleSystem rainPS;
    public GameObject HRB;

    // Start is called before the first frame update
    void Start()
    {

        ParticleSystem rainPS = GetComponent<ParticleSystem>();
        var emission = rainPS.main;

        rain. = 100.0f;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {

            
        }
    }
}
