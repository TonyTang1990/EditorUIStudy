/*
 * Description:             MessageTreeView.cs
 * Author:                  TONYTANG
 * Create Date:             2022/04/22
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    /// 列枚举定义
    /// </summary>
    private enum MessageTreeColumns
    {
        MessageName = 0,        // 消息名
        MessageType,            // 消息类型(请求返回或推送)
        MessageAnnotation,      // 消息注释
        MessageContent,         // 消息内容
        MessageDelete,          // 删除消息
    }

    /// <summary>
    /// 排序类型
    /// </summary>
    public enum SortOption
    {
        MsgName,           // 按消息名字排序
        MsgType,           // 按消息类型排序
    }

    /// <summary>
    /// 列排序规则映射Map<列索引, 排序规则>
    /// </summary>
    private Dictionary<int, SortOption> mSortOptionsMap = new Dictionary<int, SortOption>()
    {
        { 0, SortOption.MsgName },
        { 1, SortOption.MsgType }
    };

    /// <summary>
    /// 推送纹理
    /// </summary>
    public static Texture2D PushIconTexture = EditorGUIUtility.FindTexture("CollabPull");

    /// <summary>
    /// 请求纹理
    /// </summary>
    public static Texture2D PullIconTexture = EditorGUIUtility.FindTexture("CollabPush");

    /// <summary>
    /// 带参构造函数
    /// </summary>
    /// <param name="state"></param>
    /// <param name="multicolumnHeader"></param>
    /// <param name="model"></param>
    public MessageTreeView(TreeViewState state, MultiColumnHeader multicolumnHeader, TreeModel<MessageTreeViewElement> model) : base(state, multicolumnHeader, model)
    {
        columnIndexForTreeFoldouts = 0;
        showAlternatingRowBackgrounds = true;
        showBorder = true;
        // 监听排序设置变化
        multicolumnHeader.sortingChanged += OnSortingChanged;

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
                    headerContent = new GUIContent("消息名", "消息名"),
                    contextMenuText = "消息名",
                    headerTextAlignment = TextAlignment.Center,
                    sortedAscending = true,
                    sortingArrowAlignment = TextAlignment.Right,
                    width = 200,
                    minWidth = 200,
                    maxWidth = 250,
                    autoResize = false,
                    allowToggleVisibility = false
                },
                new MultiColumnHeaderState.Column
                {
                    headerContent = new GUIContent(EditorGUIUtility.FindTexture("FilterByLabel"), "消息类型"),
                    contextMenuText = "消息类型",
                    headerTextAlignment = TextAlignment.Center,
                    sortedAscending = true,
                    sortingArrowAlignment = TextAlignment.Right,
                    width = 40,
                    minWidth = 40,
                    maxWidth = 60,
                    autoResize = false,
                    allowToggleVisibility = false
                },
                new MultiColumnHeaderState.Column
                {
                    headerContent = new GUIContent("消息注释", "消息注释"),
                    contextMenuText = "消息介绍",
                    headerTextAlignment = TextAlignment.Center,
                    sortingArrowAlignment = TextAlignment.Right,
                    width = 200,
                    minWidth = 200,
                    maxWidth = 250,
                    autoResize = false,
                    allowToggleVisibility = false,
                },
                new MultiColumnHeaderState.Column
                {
                    headerContent = new GUIContent("消息内容", "消息内容"),
                    contextMenuText = "消息内容",
                    headerTextAlignment = TextAlignment.Center,
                    sortingArrowAlignment = TextAlignment.Right,
                    width = 120,
                    minWidth = 120,
                    maxWidth = 150,
                    autoResize = false,
                    allowToggleVisibility = false,
                },
                new MultiColumnHeaderState.Column
                {
                    headerContent = new GUIContent("删除", "删除"),
                    contextMenuText = "删除",
                    headerTextAlignment = TextAlignment.Center,
                    sortingArrowAlignment = TextAlignment.Right,
                    width = 80,
                    minWidth = 80,
                    maxWidth = 80,
                    autoResize = false,
                    allowToggleVisibility = false,
                }
            };
        var state = new MultiColumnHeaderState(columns);
        return state;
    }

    /// <summary>
    /// 自定义构建行显示
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    protected override IList<TreeViewItem> BuildRows(TreeViewItem root)
    {
        var rows = base.BuildRows(root);
        SortIfNeeded(root, rows);
        return rows;
    }

    /// <summary>
    /// 排序回调
    /// </summary>
    /// <param name="multiColumnHeader"></param>
    void OnSortingChanged(MultiColumnHeader multiColumnHeader)
    {
        SortIfNeeded(rootItem, GetRows());
    }

    /// <summary>
    /// 排序
    /// </summary>
    /// <param name="root"></param>
    /// <param name="rows"></param>
    private void SortIfNeeded(TreeViewItem root, IList<TreeViewItem> rows)
    {
        if (rows.Count <= 1)
        {
            return;
        }

        // 没有设置排序的列
        if (multiColumnHeader.sortedColumnIndex == -1)
        {
            return;
        }

        SortByMultipleColumns();
        TreeToList(root, rows);
        Repaint();
    }

    /// <summary>
    /// 根据多列设置排序
    /// </summary>
    void SortByMultipleColumns()
    {
        var sortedColumns = multiColumnHeader.state.sortedColumns;

        if (sortedColumns.Length == 0)
        {
            return;
        }
        // 暂时只根据单个设置排序
        SortByColumnIndex(0);
    }

    /// <summary>
    /// 根据指定列索引排序
    /// </summary>
    /// <param name="index"></param>
    private void SortByColumnIndex(int index)
    {
        var sortedColumns = multiColumnHeader.state.sortedColumns;
        if (sortedColumns.Length == 0 || sortedColumns.Length <= index)
        {
            return;
        }
        if (!mSortOptionsMap.ContainsKey(sortedColumns[index]))
        {
            return;
        }
        var childTreeViewItems = rootItem.children.Cast<TreeViewItemGeneric<MessageTreeViewElement>>();
        SortOption sortOption = mSortOptionsMap[sortedColumns[index]];
        bool ascending = multiColumnHeader.IsSortedAscending(sortedColumns[index]);
        switch (sortOption)
        {
            case SortOption.MsgName:
                if (ascending)
                {
                    childTreeViewItems = childTreeViewItems.OrderBy(treeViewItemGeneric => treeViewItemGeneric.Data.MsgName);
                }
                else
                {
                    childTreeViewItems = childTreeViewItems.OrderByDescending(treeViewItemGeneric => treeViewItemGeneric.Data.MsgName);
                }
                break;
            case SortOption.MsgType:
                if (ascending)
                {
                    childTreeViewItems = childTreeViewItems.OrderBy(treeViewItemGeneric => treeViewItemGeneric.Data.MsgType);
                }
                else
                {
                    childTreeViewItems = childTreeViewItems.OrderByDescending(treeViewItemGeneric => treeViewItemGeneric.Data.MsgType);
                }
                break;
        }
        rootItem.children = childTreeViewItems.Cast<TreeViewItem>().ToList();
    }

    /// <summary>
    /// 构建
    /// </summary>
    /// <param name="root"></param>
    /// <param name="result"></param>
    private void TreeToList(TreeViewItem root, IList<TreeViewItem> result)
    {
        if (root == null)
        {
            throw new NullReferenceException("不能传空根节点!");
        }
        if (result == null)
        {
            throw new NullReferenceException("不能传空结果列表!");
        }

        result.Clear();

        if (root.children == null)
        {
            return;
        }

        Stack<TreeViewItem> stack = new Stack<TreeViewItem>();
        for (int i = root.children.Count - 1; i >= 0; i--)
        {
            stack.Push(root.children[i]);
        }

        while (stack.Count > 0)
        {
            TreeViewItem current = stack.Pop();
            result.Add(current);

            if (current.hasChildren && current.children[0] != null)
            {
                for (int i = current.children.Count - 1; i >= 0; i--)
                {
                    stack.Push(current.children[i]);
                }
            }
        }
    }

    /// <summary>
    /// 自定义TreeView每行显示
    /// </summary>
    /// <param name="args"></param>
    protected override void RowGUI(RowGUIArgs args)
    {
        var item = (TreeViewItemGeneric<MessageTreeViewElement>)args.item;

        // 只构建可见的TreeView Row
        for (int i = 0; i < args.GetNumVisibleColumns(); ++i)
        {
            CellGUI(args.GetCellRect(i), item, (MessageTreeColumns)args.GetColumn(i), ref args);
        }
    }

    /// <summary>
    /// 单行显示
    /// </summary>
    /// <param name="cellRect"></param>
    /// <param name="item"></param>
    /// <param name="column"></param>
    /// <param name="args"></param>
    void CellGUI(Rect cellRect, TreeViewItemGeneric<MessageTreeViewElement> item, MessageTreeColumns column, ref RowGUIArgs args)
    {
        // Center cell rect vertically (makes it easier to place controls, icons etc in the cells)
        CenterRectUsingSingleLineHeight(ref cellRect);

        switch (column)
        {
            case MessageTreeColumns.MessageName:
                // 显示初始折叠和描述信息
                base.RowGUI(args);
                break;
            case MessageTreeColumns.MessageType:
                var iconTexture = item.Data.MsgType == MessageTreeViewElement.MessageType.Push ? PushIconTexture : PullIconTexture;
                GUI.DrawTexture(cellRect, iconTexture, ScaleMode.ScaleToFit);
                break;
            case MessageTreeColumns.MessageAnnotation:
                item.Data.MsgAnnotation = EditorGUI.TextField(cellRect, item.Data.MsgAnnotation);
                break;
            case MessageTreeColumns.MessageContent:
                if (GUI.Button(cellRect, "编辑消息内容"))
                {
                    var messageWindow = MessageWindow.OpenMessageWindow();
                    messageWindow.SetSelectedMessageSimulation(item.Data);
                    messageWindow.SelectMessageTag(MessageWindow.MessageTag.MessageSimulation);
                }
                break;
            case MessageTreeColumns.MessageDelete:
                if (GUI.Button(cellRect, "-"))
                {
                    TreeModel.RemoveElementByID(item.Data.ID);
                }
                break;
        }
    }
}