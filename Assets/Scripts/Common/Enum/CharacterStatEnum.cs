using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
public class StatFieldNameAttribute : Attribute
{
    public string FieldName { get; }

    public StatFieldNameAttribute(string fieldName)
    {
        FieldName = fieldName;
    }
}
