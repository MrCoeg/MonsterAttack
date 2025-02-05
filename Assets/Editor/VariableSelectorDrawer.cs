using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Common.Utility;

[CustomPropertyDrawer(typeof(VariableSelectorAttribute))]
public class VariableSelectorDrawer : PropertyDrawer
{
    private List<string> _variables = new List<string>();
    private bool _initialized = false;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedObject serializedObject = property.serializedObject;
        SerializedProperty targetScriptProperty = serializedObject.FindProperty("targetScript");

        EditorGUI.BeginProperty(position, label, property);

        if (targetScriptProperty.objectReferenceValue == null)
        {
            EditorGUI.LabelField(position, "Select a script first!");
            EditorGUI.EndProperty();
            return;
        }

        MonoBehaviour targetScript = (MonoBehaviour)targetScriptProperty.objectReferenceValue;

        if (!_initialized)
        {
            _variables = GetFields(targetScript);
            _initialized = true;
        }

        if (_variables.Count == 0)
        {
            EditorGUI.LabelField(position, "No variables found.");
        }
        else
        {
            int index = Mathf.Max(0, _variables.IndexOf(property.stringValue));
            index = EditorGUI.Popup(position, property.displayName, index, _variables.ToArray());
            property.stringValue = _variables[index];
        }

        EditorGUI.EndProperty();
    }

    private List<string> GetFields(MonoBehaviour script)
    {
        List<string> fieldsList = new List<string>();
        Type type = script.GetType();

        foreach (FieldInfo field in type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
        {
            if (!field.FieldType.IsClass || field.FieldType == typeof(string))
            {
                fieldsList.Add(field.Name);
            }
            else
            {
                foreach (FieldInfo subField in field.FieldType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
                {
                    fieldsList.Add($"{field.Name}.{subField.Name}");
                }
            }
        }
        return fieldsList;
    }
}
