/*
 * Description:             MessageTreeAssetEditor.cs
 * Author:                  TONYTANG
 * Create Date:             2022/05/03
 */

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

/// <summary>
/// MessageTreeAssetEditor.cs
/// 消息数据Asset自定义Editor
/// </summary>
[CustomEditor(typeof(MessageTreeAsset))]
public class MessageTreeAssetEditor : Editor
{
    /// <summary>
    /// 消息自定义Inspector的TreeView
    /// </summary>
    class MessageInspectorTreeView : TreeViewWithTreeModel<MessageTreeViewElement>
    {
        public MessageInspectorTreeView(TreeViewState state, TreeModel<MessageTreeViewElement> model)
            : base(state, model)
        {
            showBorder = true;
            showAlternatingRowBackgrounds = true;
        }
    }

    /// <summary>
    /// 消息TreeView
    /// </summary>
    private MessageInspectorTreeView mTreeView;

    /// <summary>
    /// 数据Asset
    /// </summary>
    public MessageTreeAsset Asset
    {
        get
        {
            return (MessageTreeAsset) target;
        }
    }

    /// <summary>
    /// TreeViewState保存Key
    /// </summary>
    private const string TreeViewStateSaveKey = "MessageTreeViewStateSaveKey";

    /// <summary>
    /// 消息树状GUI的所有Style
    /// </summary>
    public static class MessageTreeGUIStyles
    {
        /// <summary>
        /// MiniButton的Style
        /// </summary>
        public static GUIStyle MiniButtonStyle = new GUIStyle("miniButton");
    }

    /// <summary>
    /// 激活
    /// </summary>
    void OnEnable()
    {
        var treeViewState = new TreeViewState();
        var jsonState = SessionState.GetString(TreeViewStateSaveKey + Asset.GetInstanceID(), "");
        if(!string.IsNullOrEmpty(jsonState))
        {
            JsonUtility.FromJsonOverwrite(jsonState, treeViewState);
        }
        var treeModel = new TreeModel<MessageTreeViewElement>(Asset.MessageTreeElementList);
        mTreeView = new MessageInspectorTreeView(treeViewState, treeModel);
        mTreeView.Reload();
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    void OnDisable()
    {
        if(mTreeView != null)
        {
            SessionState.SetString(TreeViewStateSaveKey + Asset.GetInstanceID(), JsonUtility.ToJson(mTreeView));
        }
    }

    /// <summary>
    /// 重写自定义面板显示
    /// </summary>
    public override void OnInspectorGUI()
    {
        EditorGUILayout.Space(5f);
        DrawOperationBarArea();
        EditorGUILayout.Space(3f);

        DrawTreeViewArea();
    }

    /// <summary>
    /// 绘制操作区域
    /// </summary>
    private void DrawOperationBarArea()
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("取消选中", MessageTreeGUIStyles.MiniButtonStyle, GUILayout.MinWidth(80f)))
        {
            if (mTreeView != null)
            {
                mTreeView.SetSelection(new int[] { });
            }
        }

        if (GUILayout.Button("添加", MessageTreeGUIStyles.MiniButtonStyle, GUILayout.MinWidth(80f)))
        {
            if(mTreeView != null)
            {
                var selection = mTreeView.GetSelection();
                var parent = (selection.Count == 1 ? mTreeView.TreeModel.Find(selection[0]) : null) ?? mTreeView.TreeModel.Root;
                var depth = parent != null ? parent.Depth + 1 : 0;
                var id = mTreeView.TreeModel.GenerateUniqueID();
                var element = new MessageTreeViewElement(id, depth, $"Message{id}");
                mTreeView.TreeModel.AddElement(element, parent, 0);
                mTreeView.SetSelection(new[] { id }, TreeViewSelectionOptions.RevealAndFrame);
            }
        }

        if(GUILayout.Button("删除", MessageTreeGUIStyles.MiniButtonStyle, GUILayout.MinWidth(80f)))
        {
            if (mTreeView != null)
            {
                var selection = mTreeView.GetSelection();
                mTreeView.TreeModel.RemoveElementsByID(selection);
            }
        }
        EditorGUILayout.EndHorizontal();
    }

    /// <summary>
    /// 绘制树形数据区域
    /// </summary>
    private void DrawTreeViewArea()
    {
        if(mTreeView != null)
        {
            var totalHeight = mTreeView.totalHeight;
            // 树形数据展示Rect占位
            Rect rect = GUILayoutUtility.GetRect(0f, 100000f, 0f, totalHeight);
            Rect multiColumnTreeViewRect = new Rect(rect.x, rect.y, rect.width, rect.height);
            mTreeView?.OnGUI(multiColumnTreeViewRect);
        }
    }
}