/*
 * Description:             EditorUtilities.cs
 * Author:                  TANGHUAN
 * Create Date:             2020/05/26
 */

using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 编辑器工具类(编写部分公用工具代码)
/// </summary>
public static class EditorUtilities
{
    private static MethodInfo _clearConsoleMethod;
    private static MethodInfo ClearConsoleMethod
    {
        get
        {
            if (_clearConsoleMethod == null)
            {
                Assembly assembly = Assembly.GetAssembly(typeof(SceneView));
                System.Type logEntries = assembly.GetType("UnityEditor.LogEntries");
                _clearConsoleMethod = logEntries.GetMethod("Clear");
            }
            return _clearConsoleMethod;
        }
    }

    #region 路径相关
    /// <summary>
    /// 获取项目工程路径
    /// </summary>
    public static string GetProjectPath()
    {
        string projectPath = Path.GetDirectoryName(Application.dataPath);
        return PathUtilities.GetRegularPath(projectPath);
    }

    /// <summary>
    /// 清空文件夹
    /// </summary>
    /// <param name="folderPath">要清理的文件夹路径</param>
    public static void ClearFolder(string directoryPath)
    {
        if (Directory.Exists(directoryPath) == false)
            return;

        // 删除文件
        string[] allFiles = Directory.GetFiles(directoryPath);
        for (int i = 0; i < allFiles.Length; i++)
        {
            File.Delete(allFiles[i]);
        }

        // 删除文件夹
        string[] allFolders = Directory.GetDirectories(directoryPath);
        for (int i = 0; i < allFolders.Length; i++)
        {
            Directory.Delete(allFolders[i], true);
        }
    }
    #endregion

    /// <summary>
    /// 清空控制台
    /// </summary>
    public static void ClearUnityConsole()
    {
        ClearConsoleMethod.Invoke(new object(), null);
    }

    /// <summary>
    /// 是否是数字
    /// </summary>
    public static bool IsNumber(string content)
    {
        if (string.IsNullOrEmpty(content))
            return false;
        string pattern = @"^\d*$";
        return Regex.IsMatch(content, pattern);
    }

    /// <summary>
    /// 显示指定Color,Space间隔和宽度的的GUILable
    /// </summary>
    /// <param name="content"></param>
    /// <param name="color"></param>
    /// <param name="space"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    public static void DisplayDIYGUILable(string content, Color color, float space = 0, float width = 150.0f, float height = 20.0f)
    {
        var originalcolor = GUI.color;
        GUI.color = color;
        GUILayout.Space(space);
        GUILayout.Label(content, "Box", GUILayout.Width(width), GUILayout.Height(height));
        GUI.color = originalcolor;
    }

    /// <summary>
    /// 绘制UI分割线
    /// </summary>
    /// <param name="color"></param>
    /// <param name="thickness"></param>
    /// <param name="padding"></param>
    public static void DrawUILine(int thickness = 2, int padding = 10)
    {
        Rect r = EditorGUILayout.GetControlRect(GUILayout.Height(padding + thickness));
        r.height = thickness;
        r.y += padding / 2;
        r.x -= 2;
        r.width += 6;
        EditorGUI.DrawRect(r, GUI.color);
    }
}