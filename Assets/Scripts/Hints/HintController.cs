using UnityEngine;
using UnityEngine.UI;

public class HintController : MonoBehaviour
{
    [SerializeField] Hints hintsSet;
    [SerializeField] Text hintText;
    // Start is called before the first frame update

    void Start()
    {
        ShowHint("Start");
    }

    public void ShowHint(string eventName)
    {
        hintText.text = hintsSet.getEventHint(eventName);
    }

    public void HideHint()
    {
        hintText.text = "";
    }
}
