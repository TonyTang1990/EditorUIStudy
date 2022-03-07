/*
 * Description:             FixLayoutDraw.cs
 * Author:                  TONYTANG
 * Create Date:             2022/03/06
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// FixLayoutDraw.cs
/// 固定排版绘制
/// </summary>
public class FixLayoutDraw : MonoBehaviour
{
    private void OnGUI()
    {
        GUI.Label(new Rect(0f, 0f, 300f, 100f), $"固定排版文本位置信息:(0, 0)大小:(300, 100)");

        GUI.BeginGroup(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 100, 300f, 200f));
        GUI.Box(new Rect(0, 25, 300, 50), "GUI组里的Box位置:(0, 25)大小:(300, 50)");
        if(GUI.Button(new Rect(0, 100, 300, 50), "GUI组里的按钮位置:(0, 100)大小:(300, 50)"))
        {
            Debug.Log($"点击的GUI组里的按钮");
        }
        GUI.EndGroup();
    }
}