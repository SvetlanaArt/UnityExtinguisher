using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class HintManager : MonoBehaviour
{
    [SerializeField] Hints[] hintsSet;
    [SerializeField] Text hintText;
    // Start is called before the first frame update

    string currentEventName;
    int numHintSet;

    void Start()
    {
        numHintSet = 0;
        ShowHint("Start");
    }

    public void ShowHint(string eventName)
    {
        hintText.text = hintsSet[numHintSet].getEventHint(eventName);
        currentEventName = eventName;
    }

    public void OnLanguageChanged(Dropdown change)
    {
        numHintSet = change.value;
        ShowHint(currentEventName);
    }

 }
