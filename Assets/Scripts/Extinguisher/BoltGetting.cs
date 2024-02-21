using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;


/// <summary>
/// Handles events of the Bolt getting out.
/// </summary>
public class BoltGetting: MonoBehaviour, IPointerClickHandler

{
    [SerializeField] Animation anim;

    AudioShotPlayer player;
  
    void Start()
    {
        player = FindAnyObjectByType<AudioShotPlayer>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (anim != null)
        {
            anim.Play("BoltOutAnim");
            anim = null;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (player != null)
        {
            player.PlayBoltFallSound(transform.position);
        }
    }


}
