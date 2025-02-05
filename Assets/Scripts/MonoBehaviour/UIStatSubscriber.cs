using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using TMPro;
using Common.Utility;

public enum StatValueType
{
    Int,
    Float,
    String
}

public class UIStatSubscriber : MonoBehaviour
{
    [SerializeField] private MonoBehaviour targetScript; 
    [SerializeField] private StatValueType statValueType; 
    [VariableSelector] public string selectedVariable;

    private TextMeshProUGUI _targetText;

    private void Awake()
    {
        _targetText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        Subscribe();
    }

    public void Subscribe()
    {
        if (targetScript == null || string.IsNullOrEmpty(selectedVariable)) return;

        var selectors = selectedVariable.Split('.');

        object currentObject = targetScript;

        FieldInfo field = null;

        for (int i = 0; i < selectors.Length; i++)
        {
            if (currentObject == null) break;

            field = currentObject.GetType().GetFields(
                   BindingFlags.Public |
                   BindingFlags.NonPublic |
                   BindingFlags.Instance
                   ).FirstOrDefault(f => f.Name == selectors[i]);

            if (field == null) break;
            if (i < selectors.Length - 1)
                currentObject = field.GetValue(currentObject);



        }

        object value = field.GetValue(currentObject);

        switch (statValueType)
        {
            case StatValueType.Int:
                var observableInt = (ObservableVariable<int>)value;
                _targetText.text = observableInt.Value.ToString();
                observableInt.OnValueChanged += OnValueChanged;
                break;
            case StatValueType.Float:
                var observableFloat = (ObservableVariable<float>)value;
                observableFloat.OnValueChanged += OnValueChanged;
                break;
            case StatValueType.String:
                var observableString = (ObservableVariable<string>)value;
                observableString.OnValueChanged += OnValueChanged;
                break;
        }

    }

    private void OnValueChanged(int value)
    {
        _targetText.text = value.ToString();
    }

    private void OnValueChanged(float value)
    {
        _targetText.text = value.ToString();
    }

    private void OnValueChanged(string value)
    {
        _targetText.text = value;
    }

}
