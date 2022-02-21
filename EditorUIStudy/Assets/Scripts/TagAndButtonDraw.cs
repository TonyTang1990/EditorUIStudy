/*
 * Description:             TagAndButtonDraw.cs
 * Author:                  TONYTANG
 * Create Date:             2022/02/21
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// TagAndButtonDraw.cs
/// 物体标签和交互按钮显示
/// </summary>
public class TagAndButtonDraw : MonoBehaviour 
{
    /// <summary>
    /// 物体名
    /// </summary>
    [Header("物体名")]
    public string Name = "GUI测试物体";

    /// <summary>
    /// 物体标签名
    /// </summary>
    [Header("物体标签名")]
    public string TagName = "sv_icon_name3";

    void OnDrawGizmos()
    {
        DrawTag();
    }

    /// <summary>
    /// 绘制物体标签
    /// </summary>
    private void DrawTag()
    {
        Gizmos.DrawIcon(transform.position, TagName, false);
    }
}