using UnityEngine;
using static UnityEngine.ParticleSystem;

public class FireController : MonoBehaviour
{
    [SerializeField] ParticleSystem fire;
    [SerializeField] ExtinguishingController extinguishController;
    [Header("Counting parameters")]
    [SerializeField] float timeFire = 6f;
    [SerializeField] float fireLevel = 1.2f;
    [SerializeField] float maxFireLevel = 1.2f;
    [SerializeField] float speedFireUp = 0.2f;

    MainModule mainModuleFire;
    AudioSource audioFire;
    HintManager hintController;

    void Start()
    {
        Init();
    }

    private void Init()
    {
        fireLevel = maxFireLevel;
        mainModuleFire = fire.main;
        audioFire = GetComponent<AudioSource>();
        hintController = FindAnyObjectByType<HintManager>();
    }

    void Update()
    {
        ChangeFireLevel();
        audioFire.volume = fireLevel / maxFireLevel;
    }

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

    void FireUp()
    {
        fireLevel += speedFireUp * Time.deltaTime;
        fireLevel = Mathf.Min(maxFireLevel, fireLevel);
    }

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
