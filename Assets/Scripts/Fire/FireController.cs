using UnityEngine;
using static UnityEngine.ParticleSystem;

/// <summary>
/// Controls the fire level
/// </summary>
public class FireController : MonoBehaviour
{
    [SerializeField] ParticleSystem fire;
    [SerializeField] ExtinguishingController extinguishController;
    [Header("Counting parameters")]
    // extinguishing time if the player sets the extinguisher right
    [SerializeField] float timeFire = 6f;
    [SerializeField] float speedFireUp = 0.2f;

    MainModule mainModuleFire;
    AudioSource audioFire;
    HintManager hintController;

    // current level
    float fireLevel;
    // maximum level from start
    float maxFireLevel;

    void Start()
    {
        InitComponents();
        InitFireLevel();
    }
    private void InitComponents()
    {
        audioFire = GetComponent<AudioSource>();
        hintController = FindAnyObjectByType<HintManager>();
    }

    private void InitFireLevel()
    {
        mainModuleFire = fire.main;
        maxFireLevel = mainModuleFire.startLifetime.constantMax;
        fireLevel = maxFireLevel;
    }

     void Update()
    {
        ChangeFireLevel();
        audioFire.volume = fireLevel / maxFireLevel;
    }

    /// <summary>
    /// Change the fire level which depends on the extinguishing process
    /// </summary>
    void ChangeFireLevel()
    {
        if (fire.isPlaying)
        {
            float coef = extinguishController.getCoefEstinguishing();
            if (coef > 0)
            {
                FireDown(coef);
            }
            else if (fireLevel < maxFireLevel)
            {
                FireUp();
            }
            mainModuleFire.startLifetime = fireLevel;
        }
    }

    /// <summary>
    /// Increase the fire level
    /// </summary>
    void FireUp()
    {
        fireLevel += speedFireUp * Time.deltaTime;
        fireLevel = Mathf.Min(maxFireLevel, fireLevel);
    }

    /// <summary>
    /// Decrease the fire level
    /// </summary>
    /// <param name="coef"> The extinguishing efficiency coefficient </param>
    void FireDown(float coef)
    {
        float speedOfFireDown = (maxFireLevel / timeFire) * coef;
        fireLevel -= speedOfFireDown * Time.deltaTime;
        fireLevel = Mathf.Max(0, fireLevel);
        if (fireLevel <= 0)
        {
            FireStop();
        }
    }
    /// <summary>
    /// Stop the fire (Particle System, sound) and show a hint about this event
    ///</summary>
    void FireStop()
    {
        fire.Stop();
        audioFire.Stop();
        if (hintController != null)
        {
            hintController.ShowHint("FirePutOut");
        }
        GameObject.Destroy(gameObject);
    }
    public float getFireLevel()
    {
        return fireLevel;
    }

    public float getMaxFireLevel()
    {
        return maxFireLevel;
    }
}
