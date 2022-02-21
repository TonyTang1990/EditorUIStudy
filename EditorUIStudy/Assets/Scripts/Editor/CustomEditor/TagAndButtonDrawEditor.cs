/*
 * Description:             TagAndButtonDrawEditor.cs
 * Author:                  TONYTANG
 * Create Date:             2022/02/21
 */

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// TagAndButtonDrawEditor.cs
/// 物体也签和交互按钮显示的Editor绘制
/// </summary>
[CustomEditor(typeof(TagAndButtonDraw))]
public class TagAndButtonDrawEditor : Editor 
{
    /// <summary>
    /// 物体名属性
    /// </summary>
    SerializedProperty mNameProperty;

    protected void OnEnable()
    {
        mNameProperty = serializedObject.FindProperty("Name");
    }

    protected virtual void OnSceneGUI()
    {
        var tagAndButtonDraw = (TagAndButtonDraw)target;
        if(tagAndButtonDraw == null)
        {
            return;
        }
        Handles.Label(tagAndButtonDraw.transform.position, mNameProperty.stringValue);
        // Handles里绘制2D GUI需要在Handles.BeginGUI()和Handles.EndGUI()内
        Handles.BeginGUI();
        if(GUILayout.Button("测试按钮", GUILayout.Width(200f), GUILayout.Height(40f)))
        {
            Debug.Log($"物体按钮被点击!");
            Selection.activeGameObject = tagAndButtonDraw.gameObject;
        }
        Handles.EndGUI();
    }
}