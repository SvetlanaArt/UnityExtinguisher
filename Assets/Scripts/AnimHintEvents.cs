using UnityEngine;

public class AnimHintEvents : MonoBehaviour
{
    [Header("Bolt Event")]
    [SerializeField] Collider nozzleCollider;
    [SerializeField] Transform bolt;
    [SerializeField] Animation anim;
    [Header("Nozzle Event")]
    [SerializeField] Collider handleCollider;

    AudioShotPlayer player;
    HintController hintController;

    bool isExtingNeedPositioning;


    void Start()
    {
        InitEvents();
    }

    void InitEvents()
    {
        player = FindAnyObjectByType<AudioShotPlayer>();
        hintController = FindAnyObjectByType<HintController>();
        isExtingNeedPositioning = false;
    }

    public void BoltFallDown()
    {
        anim.Stop();
        bolt.SetParent(null);
        Rigidbody body = bolt.GetComponent<Rigidbody>();
        if (body != null)
        {
            body.isKinematic = false;
        }
        nozzleCollider.enabled = true;
    }

    public void EnableUchvyt()
    {
        handleCollider.enabled = true;
        isExtingNeedPositioning = true;
    }

    public void BoltGetOut()
    {
        if (player != null)
            player.PlayBoltGetSound(transform.position);
    }

    public void ShowHint(string eventName)
    {
        if (hintController != null)
        {
            hintController.ShowHint(eventName);
        }
    }

    public void ShowSliderHint()
    {
        if (isExtingNeedPositioning)
        {
            ShowHint("SliderSet");
            isExtingNeedPositioning = false;
        }
    }

    public void HandleUpDown()
    {
        if (player != null)
            player.PlayHandleGetSound(transform.position);
      }

}
