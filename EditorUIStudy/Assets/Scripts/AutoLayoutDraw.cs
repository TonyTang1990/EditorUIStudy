/*
 * Description:             AutoLayoutDraw.cs
 * Author:                  TONYTANG
 * Create Date:             2022/03/06
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// AutoLayoutDraw.cs
/// 自动排版绘制
/// </summary>
public class AutoLayoutDraw : MonoBehaviour
{
    private void OnGUI()
    {
        // 自动排版
        if(GUILayout.Button("自动排版的第一个按钮"))
        {
            Debug.Log($"点击了自动排版的第一个按钮!");
        }
        // 带自动排版的自定义绘制区域1
        GUILayout.BeginArea(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 150, 300, 300));
        if(GUILayout.Button("自动排版中心区域内的按钮1"))
        {
            Debug.Log($"点击了自动排版中心区域内的按钮1!");
        }
        if (GUILayout.Button("自动排版中心区域内的按钮2"))
        {
            Debug.Log($"点击了自动排版中心区域内的按钮2!");
        }
        GUILayout.EndArea();

        // 带自动排版的自定义绘制区域2
        GUILayout.BeginArea(new Rect(Screen.width - 300, Screen.height - 100, 300, 100));
        // 指定右下角区域采用纵向自动布局
        GUILayout.BeginVertical();
        // 指定GUI按钮的宽度固定200
        if (GUILayout.Button("自动排版右下角区域内的按钮1", GUILayout.Width(200f)))
        {
            Debug.Log($"点击了自动排版右下角区域内的按钮1!");
        }
        // 指定GUI按钮的宽度自适应
        if (GUILayout.Button("自动排版右下角区域内的按钮2", GUILayout.ExpandWidth(true)))
        {
            Debug.Log($"点击了自动排版右下角区域内的按钮2!");
        }
        GUILayout.EndVertical();
        GUILayout.EndArea();
    }
}