/*
 * Description:             TRangeDrawer.cs
 * Author:                  TONYTANG
 * Create Date:             2022/02/21
 */

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// IntRangeDrawer.cs
/// 数值范围绘制
/// </summary>
[CustomPropertyDrawer(typeof(TRangeAttribute))]
public class TRangeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var tRangeAttribute = (TRangeAttribute)attribute;
        if(property.propertyType == SerializedPropertyType.Integer)
        {
            EditorGUI.IntSlider(position, property, (int)tRangeAttribute.MinValue, (int)tRangeAttribute.MaxValue, label);
        }
        else if(property.propertyType == SerializedPropertyType.Float)
        {
            EditorGUI.Slider(position, property, tRangeAttribute.MinValue, tRangeAttribute.MaxValue, label);
        }
        else
        {
            EditorGUILayout.LabelField(label, "请使用TRange到float或int上!");
        }
    }
}