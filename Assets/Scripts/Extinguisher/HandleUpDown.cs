
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Handle a click on the HandleTop Collider to start or stop extinguishing.
/// </summary>
public class HandleUpDown:  MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    ExtinguishingController extinguishingController;

     public void OnPointerDown(PointerEventData eventData)
    {
        extinguishingController.StartExtinguishing();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        extinguishingController.StopExtinguishing();
    }

}
