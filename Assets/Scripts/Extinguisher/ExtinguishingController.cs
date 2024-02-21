using System.Collections;
using UnityEngine;
using UnityEngine.WSA;

/// <summary>
/// Controls the process of extinguishing. 
/// Uses coroutine to control time of extinguishing (powder count).
/// </summary>
public class ExtinguishingController : MonoBehaviour
{
    [Header("Counting parameters")]
    // value shows how long the powder remains
    [SerializeField] float timeExtinguishing = 10f;
    // an effective angle between the direction of the extinguisher nozzle and
    // the nozzle-fire vector, which ensures
    // that the powder better reaches the fire and extinguishes it 
    [SerializeField] float pefectAngle = 0.12f;
    // maximum angle deviation at which extinguishing occurs. 
    [SerializeField] float maxAngleDeviation = 0.22f;
    [Header("Effects")]
    [SerializeField] ParticleSystem powder;

    Animation anim;
    AudioSource audioExtinguishing;
    HintManager hintController;
    ParticleSystem fire;

    // start to control time of extinguishing
    IEnumerator coroutineTimeCounting;

    // is true, as long as there is powder in the extinguisher
    bool isPowder;
    // the extinguishing efficiency coefficient, varies from 0 to 1, is 1
    // when the mutual positioning of extinguisher and fire gives
    // an angle equal to PerfectAngle, and is 0 when the angle is greater by less
    // than PerfectAngle by a given value of maxAngleDeviation
    float coefExtinguishing;
 
    void Start()
    {
        InitComponents();
        InitVars();
    }

    private void InitVars()
    {
        coroutineTimeCounting = decreaseTime();
        isPowder = true;
        coefExtinguishing = 0f;
    }

    private void InitComponents()
    {
        anim = GetComponent<Animation>();
        audioExtinguishing = GetComponent<AudioSource>();
        hintController = FindAnyObjectByType<HintManager>();
    }

    /// <summary>
    /// Find a Burning Object
    /// </summary>
    /// <returns>Position of the Fire</returns>
    Vector3 GetFirePosition()
    {
        Vector3 pos = new Vector3();
        GameObject burningObject = GameObject.FindGameObjectWithTag("Burning");
        if (burningObject != null)
        {
            fire = burningObject.GetComponentInChildren<ParticleSystem>();
            if (fire != null)
            {
                pos = fire.gameObject.transform.position;
            }
        }
        return pos;
    }

    public void StartExtinguishing()
    {
        if (anim != null)
        {
            anim.Stop();
            anim.Play("HandleDownAnim");
        }

        if (isPowder)
        {
            audioExtinguishing?.Play();
            powder.Play();
            setCoefExtinguishing();
            StartCoroutine(coroutineTimeCounting);
        }
    }

    public void StopExtinguishing()
    {
        if (anim != null)
        {
            audioExtinguishing?.Stop();
            anim.Stop();
            anim.Play("HandleUpAnim");
        }
        if (isPowder)
        {
            StopWorking();
        }
    }

    /// <summary>
    /// Decrease the time of extinguishing, 
    /// runs as a coroutine during the extinguishing process.
    /// </summary>
    IEnumerator decreaseTime()
    {
        while (isPowder)
        {
            timeExtinguishing -= Time.deltaTime;
            if (timeExtinguishing <= 0)
            {
                PowderRanOut();
            }
            yield return null;
        }
    }

    /// <summary>
    /// Decrease the time of extinguishing, 
    /// runs as a coroutine during the extinguishing process.
    /// </summary>
    void PowderRanOut()
    {
        timeExtinguishing = 0;
        StopWorking();
        isPowder = false;
        if (hintController != null)
        {
            hintController.ShowHint("PowderRanOut");
        }
    }

    /// <summary>
    /// stops Powder streaming and stops the extinguishing time decreasing
    /// </summary>
    void StopWorking()
    {
        powder.Stop();
        StopCoroutine(coroutineTimeCounting);
        coefExtinguishing = 0f;
    }

    /// <summary>
    /// Counts the extinguishing efficiency coefficient 
    /// which depends on the Fire and the Extinguisher positions
    /// </summary>
    void setCoefExtinguishing()
    {
        if (powder != null)
        {
            Vector3 powderPos = powder.gameObject.transform.position;
            Vector3 distance = powderPos - GetFirePosition(); ;
            float absY = Mathf.Abs(distance.y);
            float angle = Mathf.Atan(absY / Mathf.Abs(distance.x));
            float signAngle = distance.y / absY;
            float difAngle = Mathf.Min(Mathf.Abs(signAngle * angle - pefectAngle), maxAngleDeviation);
            float coef = 1 - difAngle / maxAngleDeviation;
            coefExtinguishing = coef;
        }
    }

    public float getCoefEstinguishing()
    {
        return coefExtinguishing;
    }

    public float getPowderCount()
    {
        return timeExtinguishing;
    }

}
