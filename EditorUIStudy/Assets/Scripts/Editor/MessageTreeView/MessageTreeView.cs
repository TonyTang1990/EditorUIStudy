/*
 * Description:             MessageTreeView.cs
 * Author:                  TONYTANG
 * Create Date:             2022/04/22
 */

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

/// <summary>
/// MessageTreeView.cs
/// 消息TreeView
/// </summary>
public class MessageTreeView : TreeViewWithTreeModel<MessageTreeViewElement>
{
    /// <summary>
    /// 带参构造函数
    /// </summary>
    /// <param name="state"></param>
    /// <param name="multicolumnHeader"></param>
    /// <param name="model"></param>
    public MessageTreeView(TreeViewState state, MultiColumnHeader multicolumnHeader, TreeModel<MessageTreeViewElement> model) : base(state, multicolumnHeader, model)
    {
        columnIndexForTreeFoldouts = 2;
        showAlternatingRowBackgrounds = true;
        showBorder = true;

        Reload();
    }

    /// <summary>
    /// 创建默认的多列显示状态数据
    /// </summary>
    /// <returns></returns>
    public static MultiColumnHeaderState CreateDefaultMultiColumnHeaderState()
    {
        var columns = new[]
            {
                new MultiColumnHeaderState.Column
                {
                    headerContent = new GUIContent(EditorGUIUtility.FindTexture("FilterByLabel"), "消息数据"),
                    contextMenuText = "消息数据",
                    headerTextAlignment = TextAlignment.Center,
                    sortedAscending = true,
                    sortingArrowAlignment = TextAlignment.Right,
                    width = 30,
                    minWidth = 30,
                    maxWidth = 60,
                    autoResize = false,
                    allowToggleVisibility = true
                },
                new MultiColumnHeaderState.Column
                {
                    headerContent = new GUIContent(EditorGUIUtility.FindTexture("FilterByType"), "消息名"),
                    contextMenuText = "消息名",
                    headerTextAlignment = TextAlignment.Center,
                    sortedAscending = true,
                    sortingArrowAlignment = TextAlignment.Right,
                    width = 120,
                    minWidth = 30,
                    maxWidth = 150,
                    autoResize = false,
                    allowToggleVisibility = true
                },
                new MultiColumnHeaderState.Column
                {
                    headerContent = new GUIContent(EditorGUIUtility.FindTexture("Favorite"), "消息内容"),
                    contextMenuText = "消息内容",
                    headerTextAlignment = TextAlignment.Center,
                    sortedAscending = true,
                    sortingArrowAlignment = TextAlignment.Right,
                    width = 120,
                    minWidth = 30,
                    maxWidth = 150,
                    autoResize = false,
                    allowToggleVisibility = true
                },
            };
        var state = new MultiColumnHeaderState(columns);
        return state;
    }
}