/*
 * Description:             CustomInspectorDraw.cs
 * Author:                  TONYTANG
 * Create Date:             2022/02/21
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// CustomInspectorDraw.cs
/// 自定义Inspector面板绘制
/// </summary>
public class CustomInspectorDraw : MonoBehaviour
{
    /// <summary>
    /// 序列化类
    /// </summary>
    [Serializable]
    public class SerilizableClass
    {
        [Header("类的整形数据")]
        public int ClassIntValue;

        /// <summary>
        /// 类隐藏不显示的字符串数据
        /// </summary>
        [HideInInspector]
        [Header("类隐藏不显示的字符串数据")]
        public string ClassHideInspectorStringValue;

        /// <summary>
        /// 类不序列化的布尔数据
        /// </summary>
        [NonSerialized]
        [Header("类不序列化的布尔数据")]
        public bool ClassNonSerializedBoolValue;
    }

    /// <summary>
    /// 字符串数据
    /// </summary>
    [Header("字符串数据")]
    public string StringValue;

    /// <summary>
    /// 布尔数据
    /// </summary>
    [Header("布尔数据")]
    public bool BoolValue;

    /// <summary>
    /// GameObject对象数据
    /// </summary>
    [Header("GameObject对象数据")]
    public GameObject GoValue;

    /// <summary>
    /// 整形数组数据
    /// </summary>
    [Header("整形数组数据")]
    public int[] IntArrayValue;

    /// <summary>
    /// 序列化类成员
    /// </summary>
    [Header("序列化类成员")]
    public SerilizableClass SerilizabledClass;
}