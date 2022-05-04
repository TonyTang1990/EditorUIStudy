/*
 * Description:             GameLauncher.cs
 * Author:                  TONYTANG
 * Create Date:             2022/02/20
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// GameLauncher.cs
/// GUI学习入口
/// </summary>
public class GameLauncher : MonoBehaviour
{
    /// <summary>
    /// 入口场景名
    /// </summary>
    public const string ENTRY_SCENE_NAME = "EditorUIStudyScene";

    /// <summary>
    /// 场景名
    /// </summary>
    private List<string> mSceneNameList = new List<string>
    {
        "FixLayoutScene",
        "AutoLayoutScene",
        "EventScene",
        "CustomInspectorScene",
        "InspectorPropertyScene",
        "TagAndButtonScene",
        "CoordinateSystemScene",
    };

    /// <summary>
    /// 每行几个按钮
    /// </summary>
    private const int NUMBER_PER_ROW = 3;

    private void OnGUI()
    {
        if(CheckSameScen(ENTRY_SCENE_NAME))
        {
            // 标题部分
            GUILayout.BeginArea(new Rect(Screen.width / 2 - 40, Screen.height / 2 - 190, 80, 20));
            GUILayout.Label("GUI学习入口", GUILayout.Width(100f), GUILayout.Height(20f));
            GUILayout.EndArea();

            // 学习入口部分
            GUILayout.BeginArea(new Rect(Screen.width / 2 - 300, Screen.height / 2 - 150, 600, 150));
            GUILayout.BeginVertical();
            var Index = 0;
            for (int i = 0, length = mSceneNameList.Count; i < length; i++)
            {
                if (Index % NUMBER_PER_ROW == 0)
                {
                    GUILayout.BeginHorizontal();
                }
                if(GUILayout.Button($"{mSceneNameList[i]}", GUILayout.Width(180), GUILayout.Height(20f)))
                {
                    Debug.Log($"点击按钮切换场景:{mSceneNameList[i]}");
                    GameLauncher.LoadScene(mSceneNameList[i]);
                }
                if (Index == (NUMBER_PER_ROW - 1) || i == (length - 1))
                {
                    GUILayout.EndHorizontal();
                    GUILayout.Space(25f);
                }
                else
                {
                    GUILayout.Space(25f);
                }
                Index = (Index + 1) % NUMBER_PER_ROW;
            }
            GUILayout.EndVertical();
            GUILayout.EndArea();
        }
        else
        {
            GUILayout.BeginArea(new Rect(Screen.width - 100, 0, 100, 20));
            if (GUILayout.Button("返回入口场景", GUILayout.Width(100f), GUILayout.Height(20f)))
            {
                Debug.Log($"点击返回入口场景按钮!");
                GameLauncher.LoadScene(ENTRY_SCENE_NAME);
            }
            GUILayout.EndArea();
        }
    }

    /// <summary>
    /// 加载场景
    /// </summary>
    /// <param name="sceneName"></param>
    public static void LoadScene(string sceneName)
    {
        if(CheckSameScen(sceneName))
        {
            return;
        }
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// 加载入口场景
    /// </summary>
    public static void LoadEntryScene()
    {
        if(CheckSameScen(ENTRY_SCENE_NAME))
        {
            return;
        }
        SceneManager.LoadScene(ENTRY_SCENE_NAME);
    }

    /// <summary>
    /// 检查当前场景是否是指定场景
    /// </summary>
    /// <param name="sceneName"></param>
    /// <returns></returns>
    public static bool CheckSameScen(string sceneName)
    {
        return string.Equals(SceneManager.GetActiveScene().name, sceneName);
    }
}