using System.Collections;
using UnityEngine;
using static UnityEngine.ParticleSystem;

/// <summary>
/// Manage the light intensity
/// </summary>
public class LightManager : MonoBehaviour
{
    [SerializeField] float flickerSpeed = 1f;
    [SerializeField] float intensityVariance = 2f;
 
    MainModule mainModuleFire;
    Light pointLight;

    // light intencity to fire startLifeTime ratio
    float coefLightToFireLevel;
    float currentIntensityVariance;

    void Start()
    {
        mainModuleFire = transform.parent.GetComponent<ParticleSystem>().main;
        pointLight = GetComponent<Light>();
        InitCoefLightToFireLevel();
        StartCoroutine(Flicker());
    }

    /// <summary>
    /// Create a flickering light effect
    /// </summary>
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

      void Update()
    {
        CorrectLightIntensity();
    }

    /// <summary>
    /// Adjust the intensity of the light to match the level of the fire
    /// </summary>
    void CorrectLightIntensity()
    {
        pointLight.intensity = mainModuleFire.startLifetime.constantMax *
                                            coefLightToFireLevel + currentIntensityVariance;
    }


}
