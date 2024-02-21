using UnityEngine;
using static UnityEngine.ParticleSystem;

/// <summary>
/// Correct the smoke level to match the level of the fire
/// </summary>
public class SmokeLevelCorrector : MonoBehaviour
{
    // smoke to fire startLifeTime ratio
    float coefSmokeToFireLifeTime;

    MainModule mainModuleSmoke;
    MainModule mainModuleFire;

    void Start()
    {
        mainModuleFire = transform.parent.GetComponent<ParticleSystem>().main;
        mainModuleSmoke = GetComponent<ParticleSystem>().main;
        InitCoefSmokeToFireLifeTime();
    }

    private void InitCoefSmokeToFireLifeTime()
    {
        float fireLifeTime = mainModuleFire.startLifetime.constantMax;
        if (fireLifeTime != 0)
        {
            coefSmokeToFireLifeTime = mainModuleSmoke.startLifetime.constantMax / fireLifeTime;
        }
        else
        {
            coefSmokeToFireLifeTime = 0;
        }
    }

    void Update()
    {
        CorrectSmokeLevel();
    }

    void CorrectSmokeLevel()
    {
        mainModuleSmoke.startLifetime = mainModuleFire.startLifetime.constantMax * 
                                            coefSmokeToFireLifeTime;
    }
}
