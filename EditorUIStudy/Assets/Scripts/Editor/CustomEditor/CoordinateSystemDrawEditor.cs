/*
 * Description:             CoordinateSystemDrawEditor.cs
 * Author:                  TONYTANG
 * Create Date:             2022/02/20
 */

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// CoordinateSystemDrawEditor.cs
/// CoordinateSystemDraw的Editor绘制
/// </summary>
[CustomEditor(typeof(CoordinateSystemDraw))]
public class CoordinateSystemDrawEditor : Editor
{
    /// <summary>
    /// 坐标系原点属性
    /// </summary>
    SerializedProperty mCoordinateCenterPointProperty;

    /// <summary>
    /// 坐标系长度属性
    /// </summary>
    SerializedProperty mCoordinateSystemLengthProperty;

    /// <summary>
    /// Z轴颜色属性
    /// </summary>
    SerializedProperty mForwardAxisColorProperty;

    /// <summary>
    /// X轴颜色属性
    /// </summary>
    SerializedProperty mRightAxisColorProperty;

    /// <summary>
    /// Y轴颜色
    /// </summary>
    SerializedProperty mUpAxisColorProperty;

    protected void OnEnable()
    {
        mCoordinateCenterPointProperty = serializedObject.FindProperty("CoordinateCenterPoint");
        mCoordinateSystemLengthProperty = serializedObject.FindProperty("CoordinateSystemLength");
        mForwardAxisColorProperty = serializedObject.FindProperty("ForwardAxisColor");
        mRightAxisColorProperty = serializedObject.FindProperty("RightAxisColor");
        mUpAxisColorProperty = serializedObject.FindProperty("UpAxisColor");
    }

    protected virtual void OnSceneGUI()
    {
        if(Event.current.type == EventType.Repaint)
        {
            Handles.color = mForwardAxisColorProperty.colorValue;
            Handles.ArrowHandleCap(1, mCoordinateCenterPointProperty.vector3Value,
                                    Quaternion.LookRotation(Vector3.forward),
                                    mCoordinateSystemLengthProperty.floatValue, EventType.Repaint);

            Handles.color = mRightAxisColorProperty.colorValue;
            Handles.ArrowHandleCap(1, mCoordinateCenterPointProperty.vector3Value,
                                    Quaternion.LookRotation(Vector3.right),
                                    mCoordinateSystemLengthProperty.floatValue, EventType.Repaint);

            Handles.color = mUpAxisColorProperty.colorValue;
            Handles.ArrowHandleCap(1, mCoordinateCenterPointProperty.vector3Value,
                                    Quaternion.LookRotation(Vector3.up),
                                    mCoordinateSystemLengthProperty.floatValue, EventType.Repaint);
        }
    }
}