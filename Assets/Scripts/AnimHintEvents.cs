using UnityEngine;

/// <summary>
/// Contains methods that starts by animation events. 
/// </summary>
public class AnimHintEvents : MonoBehaviour
{
    [Header("Bolt Event")]
    [SerializeField] Collider nozzleCollider;
    [SerializeField] Transform bolt;
    [SerializeField] Animation anim;
    [Header("Nozzle Event")]
    [SerializeField] Collider handleCollider;

    AudioShotPlayer player;
    HintManager hintController;

    // allows to know when it is possible to show a hint about using handle "SliderSet" 
    bool isHandleEnabled;


    void Start()
    {
        InitEvents();
    }

    void InitEvents()
    {
        player = FindAnyObjectByType<AudioShotPlayer>();
        hintController = FindAnyObjectByType<HintManager>();
        isHandleEnabled = false;
    }

    /// <summary>
    /// The Bolt begins to fall down.
    /// Method starts at the end of the "BoltOutAnim" animation. 
    /// </summary>
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

    /// <summary>
    /// Enable the Nozzle Collider so a player can click on it.
    /// Method starts at the end of the "NozzleAnim" animation
    /// </summary>
    public void EnableHandle()
    {
        handleCollider.enabled = true;
        isHandleEnabled = true;
    }
    /// <summary>
    /// Plays a corresponding sound using AudioPlayer.
    /// Mathod starts at the start of the "BoltOutAnim" animation: 
    /// </summary>
    public void BoltGetOut()
    {
        if (player != null)
            player.PlayBoltGetSound(transform.position);
    }
    /// <summary>
    /// Method starts at the end or at the start of several animations
    /// to show a hint with "eventName". 
    /// </summary>
    /// <param name="eventName"> The event name to define a hint </param>
    public void ShowHint(string eventName)
    {
        if (hintController != null)
        {
            hintController.ShowHint(eventName);
        }
    }

    /// <summary>
    /// Method starts after using UI>SliderExtinguisher and 
    /// shows hint of using the extinguishing handle 
    /// </summary>
    public void ShowSliderHint()
    {
        if (isHandleEnabled)
        {
            ShowHint("SliderSet");
            isHandleEnabled = false;
        }
    }

    /// <summary>
    /// Method starts at the start of the "HandleUpAnim" and 
    /// the "HandleDownAnim" animations: plays a corresponding 
    /// sound using AudioPlayer.
    /// </summary>
    public void HandleUpDown()
    {
        if (player != null)
            player.PlayHandleGetSound(transform.position);
      }

}
