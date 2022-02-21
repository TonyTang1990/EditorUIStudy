/*
 * Description:             SerializedObjectExtension.cs
 * Author:                  TONYTANG
 * Create Date:             2022/02/21
 */

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// SerializedObjectExtension.cs
/// SerializedObject扩展方法
/// </summary>
public static class SerializedObjectExtension 
{
    /// <summary>
    /// 重置所有属性
    /// </summary>
    /// <param name="serializedObject"></param>
    public static bool ResetAllValue(this SerializedObject serializedObject)
    {
        if(serializedObject == null)
        {
            return false;
        }
        var propertyIterator = serializedObject.GetIterator();
        while(propertyIterator.NextVisible(true))
        {
            Debug.Log($"propertyPath = {propertyIterator.propertyPath}");
            Debug.Log($"propertyType = {propertyIterator.propertyType}");
        }
        return true;
    }
}