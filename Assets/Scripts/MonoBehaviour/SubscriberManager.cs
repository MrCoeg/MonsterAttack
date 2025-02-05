using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubscriberManager : MonoBehaviour
{
    public List<GameObject> subscriberObjects = new List<GameObject>();

    private static SubscriberManager _instance;

    public static SubscriberManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SubscriberManager>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("SubscriberManager");
                    _instance = obj.AddComponent<SubscriberManager>();
                }
            }
            return _instance;
        }
    }

    public static void ScanAllSubscribersInScene()
    {
        Instance.subscriberObjects.Clear();
        var subscribers = GameObject.FindGameObjectsWithTag("Subscriber");
        foreach (var subscriber in subscribers)
        {
            Instance.subscriberObjects.Add(subscriber.gameObject);
        }
    }

    public static void BindTextObservableSubscriber<T>(SubscriberEnum subscriberEnum, ObservableVariable<T> observableVariable)
    {
        var subscriberObject = GetSubscriberObject(subscriberEnum);
        var textComponent = subscriberObject.GetComponent<TMPro.TextMeshProUGUI>();
        observableVariable.Clear();
        observableVariable.OnValueChanged += (value) => textComponent.text = value.ToString();
        textComponent.text = observableVariable.Value.ToString();
    }

    public static void BindTextsObservableSubscriber<T>(Dictionary<SubscriberEnum, ObservableVariable<T>> subscribers)
    {
        foreach (var pair in subscribers)
        {
            BindTextObservableSubscriber(pair.Key, pair.Value);
        }
    }

    public static void UnbindObservableSubscriber<T>(SubscriberEnum subscriberEnum, Action<T> action)
    {
        var subscriberObject = GetSubscriberObject(subscriberEnum);
        var observableVariable = subscriberObject.GetComponent<ObservableVariable<T>>();
        observableVariable.Unsubscribe(action);
    }

    public static GameObject GetSubscriberObject(SubscriberEnum subsriberEnum)
    {
        var obj = Instance.subscriberObjects.Find(x => x.name == subsriberEnum.ToString());
        return obj;
    }

    internal static void BindObservableSubscriber<T>(SubscriberEnum attackText, ObservableVariable<T> health)
    {
        throw new NotImplementedException();
    }
}
