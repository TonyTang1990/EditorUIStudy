/*
 * Description:             EventDraw.cs
 * Author:                  TONYTANG
 * Create Date:             2022/03/06
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// EventDraw.cs
/// 事件响应绘制
/// </summary>
public class EventDraw : MonoBehaviour
{
    /// <summary>
    /// 事件信息
    /// </summary>
    private string mEventInfo;

    /// <summary>
    /// 键盘按下事件
    /// </summary>
    private string mKeyDownEvent;

    /// <summary>
    /// 鼠标点击事件
    /// </summary>
    private string mMouseDownEvent;

    private void OnGUI()
    {
        mEventInfo = string.Empty;
        mEventInfo = $"当前事件类型:{Event.current.type}";
        if(Event.current.type == EventType.KeyDown && Event.current.keyCode != KeyCode.None)
        {
            mKeyDownEvent = $"按下过按键码:{Event.current.keyCode}";
        }
        if (Event.current.type == EventType.MouseDown)
        {
            if(Event.current.button == 0)
            {
                mMouseDownEvent = $"按下过鼠标:左键";
            }
            else if (Event.current.button == 1)
            {
                mMouseDownEvent = $"按下过鼠标:右键";
            }
        }
        GUILayout.BeginArea(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 200, 400, 400));
        GUILayout.Label($"{mEventInfo}", GUILayout.Width(400), GUILayout.Height(20));
        GUILayout.Label($"{mKeyDownEvent}", GUILayout.Width(400), GUILayout.Height(20));
        GUILayout.Label($"{mMouseDownEvent}", GUILayout.Width(400), GUILayout.Height(20));
        GUILayout.EndArea();
    }
}