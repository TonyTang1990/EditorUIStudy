/*
 * Description:             InspectorPropertyDraw.cs
 * Author:                  TONYTANG
 * Create Date:             2022/02/21
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// InspectorPropertyDraw.cs
/// 自定义属性标签绘制
/// </summary>
public class InspectorPropertyDraw : MonoBehaviour
{
    /// <summary>
    /// 整形数值
    /// </summary>
    [TRange(0, 100)]
    [Header("整形数值")]
    public int IntValue;

    /// <summary>
    /// float数值
    /// </summary>
    [TRange(0, 100)]
    [Header("float数值")]
    public float FloatValue;

    /// <summary>
    /// 预览的预制件
    /// </summary>
    [TPreview]
    [Header("预览的预制件")]
    public GameObject PreviewPrefab;
}