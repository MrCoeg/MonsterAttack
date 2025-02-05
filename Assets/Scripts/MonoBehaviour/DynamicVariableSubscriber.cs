using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


namespace Common.Utility 
{
    public static class DynamicVariableSubscriber
    {
        public static void SubscribeToObservableVariable<T>(ObservableVariable<T> observableVariable, System.Action<T> callback)
        {
            observableVariable.OnValueChanged += callback;
        }

        public static void ScanObservableVariables<T>(Object targetScript, List<ObservableVariable<T>> observableVariables)
        {
            foreach (var field in targetScript.GetType().GetFields(
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Instance))
            {
                if (field.FieldType == typeof(ObservableVariable<T>))
                {
                    Debug.Log(field.Name);
                    observableVariables.Add((ObservableVariable<T>)field.GetValue(targetScript));
                }
            }

            foreach (var variable in observableVariables)
            {
                Debug.Log(variable);
            }
        }
    }

    public class VariableSelectorAttribute : PropertyAttribute { }

}

