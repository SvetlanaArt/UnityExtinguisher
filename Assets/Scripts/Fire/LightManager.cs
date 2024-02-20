using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using static UnityEngine.ParticleSystem;

public class LightManager : MonoBehaviour
{
    [SerializeField] float flickerSpeed = 1f;
    [SerializeField] float intensityVariance = 2f;
    // light intencity to fire startLifeTime ratio
    float coefLightToFireLevel;

    float currentIntensityVariance;

    MainModule mainModuleFire;
    Light pointLight;
    

    void Start()
    {
        mainModuleFire = transform.parent.GetComponent<ParticleSystem>().main;
        pointLight = GetComponent<Light>();
        InitCoefLightToFireLevel();
        StartCoroutine(Flicker());
    }

    IEnumerator Flicker()
    {
        while (true)
        {
            currentIntensityVariance = Mathf.PingPong(Time.time, intensityVariance);
            yield return new WaitForSeconds(flickerSpeed);
        }
    }

    private void InitCoefLightToFireLevel()
    {
        float fireLifeTime = mainModuleFire.startLifetime.constantMax;
        if (fireLifeTime != 0)
        {
            coefLightToFireLevel = pointLight.intensity / fireLifeTime;
        }
        else
        {
            coefLightToFireLevel = 0;
        }

    }

    // Update is called once per frame
    void Update()
    {
        CorrectLightIntensity();
    }

    void CorrectLightIntensity()
    {
        pointLight.intensity = mainModuleFire.startLifetime.constantMax *
                                            coefLightToFireLevel + currentIntensityVariance;
    }


}
