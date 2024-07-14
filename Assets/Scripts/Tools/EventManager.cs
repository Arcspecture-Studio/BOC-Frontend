using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    static Dictionary<string, UnityEvent<object>> events = new();
    static Dictionary<string, int> eventCounts = new();

    public static void AddListener(string eventName, UnityAction<object> listener)
    {
        UnityEvent<object> unityEvent;
        if (events.TryGetValue(eventName, out unityEvent))
        {
            unityEvent.AddListener(listener);
        }
        else
        {
            unityEvent = new();
            unityEvent.AddListener(listener);
            events.Add(eventName, unityEvent);
        }
        int count;
        if (eventCounts.TryGetValue(eventName, out count))
        {
            count++;
            eventCounts[eventName] = count;
        }
        else
        {
            count = 1;
            eventCounts.Add(eventName, count);
        }
    }

    public static void RemoveListener(string eventName, UnityAction<object> listener)
    {
        UnityEvent<object> unityEvent;
        if (events.TryGetValue(eventName, out unityEvent))
        {
            unityEvent.RemoveListener(listener);
            eventCounts[eventName]--;
            if (eventCounts[eventName] == 0)
            {
                events.Remove(eventName);
                eventCounts.Remove(eventName);
            }
        }
    }

    public static void Invoke(string eventName, object parameter = null)
    {
        UnityEvent<object> unityEvent;
        if (events.TryGetValue(eventName, out unityEvent))
        {
            unityEvent.Invoke(parameter);
        }
    }
}