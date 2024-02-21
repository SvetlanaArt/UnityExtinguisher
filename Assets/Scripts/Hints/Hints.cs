using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// To create object containing data of hints and their event names. 
/// Provides iterface to get a hint from the dictionary using an event name as a key.
/// </summary>

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

    /// <summary>
    /// Get a hint
    /// </summary>
    /// <param name="eventName"> Name of event</param>
    /// <returns>Text of a hint</returns>
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
