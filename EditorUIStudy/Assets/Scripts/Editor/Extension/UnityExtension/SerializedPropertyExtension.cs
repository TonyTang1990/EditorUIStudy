/*
 * Description:             SerializedPropertyExtension.cs
 * Author:                  TONYTANG
 * Create Date:             2022/02/21
 */

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// SerializedPropertyExtension.cs
/// SerializedProperty扩展方法
/// </summary>
public static class SerializedPropertyExtension 
{
    /// <summary>
    /// 重置属性
    /// </summary>
    /// <param name="serializedProperty"></param>
    public static bool ResetValue(this SerializedProperty serializedProperty)
    {
        if (serializedProperty == null)
        {
            return false;
        }
        if(serializedProperty.propertyType == SerializedPropertyType.Integer)
        {
            serializedProperty.intValue = 0;
        }
        else if (serializedProperty.propertyType == SerializedPropertyType.String)
        {
            serializedProperty.stringValue = string.Empty;
        }
        else if (serializedProperty.propertyType == SerializedPropertyType.Boolean)
        {
            serializedProperty.boolValue = false;
        }
        else if (serializedProperty.propertyType == SerializedPropertyType.Float)
        {
            serializedProperty.floatValue = 0f;
        }
        else if (serializedProperty.propertyType == SerializedPropertyType.ArraySize)
        {
            serializedProperty.intValue = 0;
        }
        return true;
    }
}