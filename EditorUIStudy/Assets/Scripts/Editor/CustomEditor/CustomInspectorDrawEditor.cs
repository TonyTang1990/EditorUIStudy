/*
 * Description:             CustomInspectorDrawEditor.cs
 * Author:                  TONYTANG
 * Create Date:             2022/02/21
 */

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// CustomInspectorDrawEditor.cs
/// CustomInspectorDraw自定义Inspector
/// </summary>
[CustomEditor(typeof(CustomInspectorDraw))]
public class CustomInspectorDrawEditor : Editor
{
    /// <summary>
    /// 字符串数据属性
    /// </summary>
    SerializedProperty StringValueProperty;

    /// <summary>
    /// 布尔数据属性
    /// </summary>
    SerializedProperty BoolValueProperty;

    /// <summary>
    /// GameObject对象数据属性
    /// </summary>
    SerializedProperty GoValueProperty;

    /// <summary>
    /// 整形数组数据属性
    /// </summary>
    SerializedProperty IntArrayValueProperty;

    /// <summary>
    /// 序列化类成员属性
    /// </summary>
    SerializedProperty SerilizabledClassProperty;

    /// <summary>
    /// 序列化类成员属性
    /// </summary>
    SerializedProperty ClassIntValueProperty;

    /// <summary>
    /// 序列化类成员属性
    /// </summary>
    SerializedProperty ClassHideInspectorStringValueProperty;

    private void OnEnable()
    {
        StringValueProperty = serializedObject.FindProperty("StringValue");
        BoolValueProperty = serializedObject.FindProperty("BoolValue");
        GoValueProperty = serializedObject.FindProperty("GoValue");
        IntArrayValueProperty = serializedObject.FindProperty("IntArrayValue");
        SerilizabledClassProperty = serializedObject.FindProperty("SerilizabledClass");
        ClassIntValueProperty = SerilizabledClassProperty.FindPropertyRelative("ClassIntValue");
        ClassHideInspectorStringValueProperty = SerilizabledClassProperty.FindPropertyRelative("ClassHideInspectorStringValue");
    }

    public override void OnInspectorGUI()
    {
        // 确保对SerializedObject和SerializedProperty的数据修改每帧同步
        serializedObject.Update();

        EditorGUILayout.BeginVertical();
        if(GUILayout.Button("重置所有值", GUILayout.ExpandWidth(true), GUILayout.Height(20f)))
        {
            StringValueProperty.stringValue = string.Empty;
            BoolValueProperty.boolValue = false;
            GoValueProperty.objectReferenceValue = null;
            IntArrayValueProperty.arraySize = 0;
            ClassIntValueProperty.intValue = 0;
            ClassHideInspectorStringValueProperty.stringValue = string.Empty;
        }
        EditorGUILayout.PropertyField(StringValueProperty);
        EditorGUILayout.PropertyField(BoolValueProperty);
        EditorGUILayout.PropertyField(GoValueProperty);
        EditorGUILayout.PropertyField(IntArrayValueProperty);
        EditorGUILayout.PropertyField(SerilizabledClassProperty, true);
        EditorGUILayout.EndVertical();

        // 确保对SerializedObject和SerializedProperty的数据修改写入生效
        serializedObject.ApplyModifiedProperties();
    }
}