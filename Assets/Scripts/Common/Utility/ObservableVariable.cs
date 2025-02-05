using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class ObservableVariable<T>
{
    [SerializeField]
    private T _value;

    [SerializeField]
    public event Action<T> OnValueChanged;

    public T Value
    {
        get => _value;
        set
        {
            if (!EqualityComparer<T>.Default.Equals(_value, value)) 
            {
                _value = value;
                OnValueChanged?.Invoke(_value);
            }
        }
    }

    public ObservableVariable(T initialValue) => _value = initialValue;

    public void Subscribe(Action<T> action) => OnValueChanged += action;

    public void Unsubscribe(Action<T> action) => OnValueChanged -= action;

    public void Clear() => OnValueChanged = null;
}


public enum VariableType
{
    Int,
    Float,
    String,
    Bool
}

public enum StatsType
{
    Null,
    Health,
    Attack,
    Defense
}


