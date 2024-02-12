using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[CreateAssetMenu(menuName = "Hints", fileName = "New Hint Set")]
public class Hints : ScriptableObject
{
    [Serializable]
    struct eventHint
    {
        public string eventName;
        public string hint;
    }
    [SerializeField] eventHint[] eventHints;
    
    Dictionary<string, string> eventHintsDic;

 
    public void Init()
    {
        if (eventHints != null)
        {
            eventHintsDic = eventHints.ToDictionary(a => a.eventName, a => a.hint);
        }
    }
 
    public string ClearHint() 
    {
        return "";
    }

    public string getEventHint(string eventName)
    {
        if (eventHintsDic == null)
            Init();
        if (eventHintsDic != null && eventHintsDic.ContainsKey(eventName))
            return eventHintsDic[eventName];
        else
            return ClearHint();
    }
 }
