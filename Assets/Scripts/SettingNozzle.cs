using UnityEngine;
using UnityEngine.EventSystems;

public class SettingNozzle : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] [Range(0,4)]  int countOfStaticPoints = 3;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] GameObject extinguisher;
    [SerializeField] Animation anim;

    Vector3[] pointsOfHose;
 
     void Start()
    {
        InitPoints();
    }

    private void InitPoints()
    {
        if (lineRenderer != null)
        {
            countOfStaticPoints = Mathf.Min(countOfStaticPoints,
                                    lineRenderer.positionCount);
            pointsOfHose = new Vector3[countOfStaticPoints];
            for (int i = 0; i < countOfStaticPoints; i++)
            {
                Vector3 worldPoint = lineRenderer.transform.TransformPoint(lineRenderer.GetPosition(i));
                pointsOfHose[i] = extinguisher.transform.InverseTransformPoint(worldPoint);
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GetComponent<Collider>().enabled = false;
        if (anim != null)
        {
            anim.Play("NozzleAnim");
        }
    }

    void LateUpdate()
    {
        UpdateHosePoints();
    }

    private void UpdateHosePoints()
    {
        if (lineRenderer != null)
        {
            for (int i = 0; i < countOfStaticPoints; i++)
            {
                Vector3 worldPoint = extinguisher.transform.TransformPoint(pointsOfHose[i]);
                lineRenderer.SetPosition(i, lineRenderer.transform.InverseTransformPoint(worldPoint));
            }
        }
    }

}
