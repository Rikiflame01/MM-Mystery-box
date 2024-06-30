using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    private Dictionary<string, UnityEvent> eventDictionary;
    private static EventManager eventManager;

    public static EventManager Instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!eventManager)
                {
                    Debug.LogError("There needs to be one active EventManager script on a GameObject in your scene.");
                }
                else
                {
                    eventManager.Init();
                }
            }

            return eventManager;
        }
    }

    void Init()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, UnityEvent>();
        }
    }

    public static void StartListening(string eventName, UnityAction listener)
    {
        UnityEvent thisEvent = null;
        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            Instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, UnityAction listener)
    {
        if (eventManager == null) return;
        UnityEvent thisEvent = null;
        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string eventName)
    {
        UnityEvent thisEvent = null;
        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke();
        }
    }

    // Define item swap events
    public static void StartListeningSwapItem0With1(UnityAction listener) => StartListening("SwapItem0With1", listener);
    public static void TriggerSwapItem0With1() => TriggerEvent("SwapItem0With1");

    public static void StartListeningSwapItem1With2(UnityAction listener) => StartListening("SwapItem1With2", listener);
    public static void TriggerSwapItem1With2() => TriggerEvent("SwapItem1With2");

    public static void StartListeningSwapItem2With0(UnityAction listener) => StartListening("SwapItem2With0", listener);
    public static void TriggerSwapItem2With0() => TriggerEvent("SwapItem2With0");

    public static void StartListeningSwapItem0With4(UnityAction listener) => StartListening("SwapItem0With4", listener);
    public static void TriggerSwapItem0With4() => TriggerEvent("SwapItem0With4");
}
