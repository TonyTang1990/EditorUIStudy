/*
 * Description:             CoordinateSystemDraw.cs
 * Author:                  TONYTANG
 * Create Date:             2022/02/20
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// CoordinateSystemDraw.cs
/// 坐标系Gizmo绘制
/// </summary>
public class CoordinateSystemDraw : MonoBehaviour 
{
    /// <summary>
    /// 坐标系原点
    /// </summary>
    [Header("坐标系原点")]
    public Vector3 CoordinateCenterPoint = Vector3.zero;

    /// <summary>
    /// 坐标系长度
    /// </summary>
    [Header("坐标系长度")]
    public float CoordinateSystemLength = 1.0f;

    /// <summary>
    /// Z轴颜色
    /// </summary>
    [Header("Z轴颜色")]
    public Color ForwardAxisColor = Color.blue;

    /// <summary>
    /// X轴颜色
    /// </summary>
    [Header("X轴颜色")]
    public Color RightAxisColor = Color.red;

    /// <summary>
    /// Y轴颜色
    /// </summary>
    [Header("Y轴颜色")]
    public Color UpAxisColor = Color.green;

    /// <summary>
    /// XY平面颜色
    /// </summary>
    [Header("XY平面颜色")]
    public Color ForwardPlaneColor = new Color(0, 0, 255, 48);

    /// <summary>
    /// ZY平面颜色
    /// </summary>
    [Header("ZY平面颜色")]
    public Color RightPlaneColor = new Color(255, 0, 0, 48);

    /// <summary>
    /// XZ平面颜色
    /// </summary>
    [Header("XZ平面颜色")]
    public Color UpPlaneColor = new Color(0, 255, 0, 48);

    /// <summary>
    /// XY轴平面大小
    /// </summary>
    private Vector3 ForwardPlaneSize = new Vector3(1, 1, 0.001f);

    /// <summary>
    /// ZY轴平面大小
    /// </summary>
    private Vector3 RightPlaneSize = new Vector3(0.001f, 1, 1);

    /// <summary>
    /// XZ轴平面大小
    /// </summary>
    private Vector3 UpPlaneSize = new Vector3(1, 0.001f, 1);

    /// <summary>
    /// 原始颜色
    /// </summary>
    private Color mOriginalColor;

    void OnDrawGizmos()
    {
        DrawForwardPlane();
        DrawRightPlane();
        DrawUpPlane();
    }

    /// <summary>
    /// 绘制XY平面
    /// </summary>
    private void DrawForwardPlane()
    {
        mOriginalColor = GUI.color;
        Gizmos.color = ForwardPlaneColor;
        ForwardPlaneSize.x = CoordinateSystemLength;
        ForwardPlaneSize.y = CoordinateSystemLength;
        Gizmos.DrawCube(CoordinateCenterPoint, ForwardPlaneSize);
        Gizmos.color = mOriginalColor;
    }

    /// <summary>
    /// 绘制ZY平面
    /// </summary>
    private void DrawRightPlane()
    {
        mOriginalColor = GUI.color;
        Gizmos.color = RightPlaneColor;
        RightPlaneSize.y = CoordinateSystemLength;
        RightPlaneSize.z = CoordinateSystemLength;
        Gizmos.DrawCube(CoordinateCenterPoint, RightPlaneSize);
        Gizmos.color = mOriginalColor;
    }

    /// <summary>
    /// 绘制XZ平面
    /// </summary>
    private void DrawUpPlane()
    {
        mOriginalColor = GUI.color;
        Gizmos.color = UpPlaneColor;
        UpPlaneSize.x = CoordinateSystemLength;
        UpPlaneSize.z = CoordinateSystemLength;
        Gizmos.DrawCube(CoordinateCenterPoint, UpPlaneSize);
        Gizmos.color = mOriginalColor;
    }
}