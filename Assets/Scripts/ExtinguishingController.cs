using System.Collections;
using UnityEngine;

public class ExtinguishingController : MonoBehaviour
{

    [Header("Counting parameters")]
    [SerializeField] float timeExtinguishing = 10f;
    [SerializeField] float pefectAngle = 0.12f;
    [SerializeField] float maxAngleDeviation = 0.22f;
    [Header("Effects")]
    [SerializeField] ParticleSystem powder;

    Animation anim;
    AudioSource audioExtinguishing;
  
    IEnumerator coroutineTimeCounting;
    Vector3 firePosition;
    ParticleSystem fire;
  
    bool isPowder;
    float coefExtinguishing;
 
    HintManager hintController;

    void Start()
    {
        Init();
        firePosition = GetFirePosition();
    }

    void Init()
    {
        anim = GetComponent<Animation>();
        audioExtinguishing = GetComponent<AudioSource>();
        coroutineTimeCounting = decreaseTime();
        isPowder = true;
        coefExtinguishing = 0f;
        hintController = FindAnyObjectByType<HintManager>();
    }

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

    void StopWorking()
    {
        powder.Stop();
        StopCoroutine(coroutineTimeCounting);
        coefExtinguishing = 0f;
    }

    void setCoefExtinguishing()
    {
        if (powder != null)
        {
            Vector3 powderPos = powder.gameObject.transform.position;
            Vector3 distance = powderPos - firePosition;
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
