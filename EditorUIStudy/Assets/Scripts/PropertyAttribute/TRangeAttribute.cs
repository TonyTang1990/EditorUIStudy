/*
 * Description:             TRangeAttribute.cs
 * Author:                  TONYTANG
 * Create Date:             2022/02/21
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// TRangeAttribute.cs
/// 数值范围属性标签
/// </summary>
public class TRangeAttribute : PropertyAttribute
{
    /// <summary>
    /// 最小值
    /// </summary>
    public readonly float MinValue;

    /// <summary>
    /// 最大值
    /// </summary>
    public readonly float MaxValue;

    public TRangeAttribute(float minValue, float maxValue)
    {
        MinValue = minValue;
        MaxValue = maxValue;
    }
}