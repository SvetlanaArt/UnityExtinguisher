using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("Extinguisher")]
    [SerializeField] Slider extinguisherPosition;
    [SerializeField] float maxPosition;
    [SerializeField] float minPosition;
    [SerializeField] GameObject extinguisher;
    [Header("Powder")]
    [SerializeField] Slider powderCount;
    [SerializeField] ExtinguishingController extinguishingController;
    [Header("Fire")]
    [SerializeField] Slider fireLevel;
    [SerializeField] FireController fireController;

    void Start()
    {
        InitExtingPos();
        InitPowderCount();
        InitFireLevel();
    }

    private void InitExtingPos()
    {
        extinguisherPosition.maxValue = maxPosition;
        extinguisherPosition.minValue = minPosition;
        extinguisherPosition.value = minPosition;
        changeExtingPosition(extinguisherPosition.value);
    }

    private void InitPowderCount()
    {
        powderCount.maxValue = extinguishingController.getPowderCount();
        powderCount.minValue = 0f;
        powderCount.value = powderCount.maxValue;
    }

    private void InitFireLevel()
    {
        fireLevel.maxValue = fireController.getMaxFireLevel();
        fireLevel.minValue = 0f;
        fireLevel.value = fireController.getFireLevel();
    }


    void Update()
    {
        UpdateUIValues();
    }

    void UpdateUIValues()
    {
        powderCount.value = extinguishingController.getPowderCount();
        fireLevel.value = fireController.getFireLevel();
    }

    public void changeExtingPosition(float value)
    {
        Vector3 position = extinguisher.transform.position;
        position.y = value;
        extinguisher.transform.position = position;
    }
}
