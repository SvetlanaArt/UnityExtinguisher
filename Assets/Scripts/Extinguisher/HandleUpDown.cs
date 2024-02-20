
using UnityEngine;
using UnityEngine.EventSystems;

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
