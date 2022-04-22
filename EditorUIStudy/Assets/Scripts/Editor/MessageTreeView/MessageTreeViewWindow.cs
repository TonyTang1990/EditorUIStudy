/*
 * Description:             MessageTreeViewWindow.cs
 * Author:                  TONYTANG
 * Create Date:             2022/04/21
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

/// <summary>
/// MessageTreeViewWindow.cs
/// 消息树状结构窗口
/// </summary>
public class MessageTreeViewWindow : EditorWindow
{
    /// <summary>
    /// TreeView操作信息
    /// </summary>
    [SerializeField]
    private TreeViewState mTreeViewState;

    /// <summary>
    /// TreeView多列显示信息
    /// </summary>
    [SerializeField]
    private MultiColumnHeaderState mMultiColumnHeaderState;

    /// <summary>
    /// 消息TreeView
    /// </summary>
    private MessageTreeView mMessageTreeView;

    /// <summary>
    /// 消息数据
    /// </summary>
    private MessageTreeAsset mMessageTreeAsset;

    /// <summary>
    /// 是否初始化过
    /// </summary>
    [NonSerialized]
    private bool mIsInitalized;
    
    /// <summary>
    /// TreeView绘制区域
    /// </summary>
    private Rect MultiColumnTreeViewRect
    {
        get { return new Rect(20, 30, position.width - 40, position.height - 60); }
    }

    [MenuItem("Tools/消息树状结构窗口")]
    public static MessageTreeViewWindow OpenMessageTreeViewWindow()
    {
        var messageTreeViewWindow = EditorWindow.GetWindow(typeof(MessageTreeViewWindow)) as MessageTreeViewWindow;
        messageTreeViewWindow.titleContent = new GUIContent("消息树状结构窗口");
        messageTreeViewWindow.Show();
        return messageTreeViewWindow;
    }

    /// <summary>
    /// 监听Asset双击打开
    /// </summary>
    /// <param name="instanceID"></param>
    /// <param name="line"></param>
    /// <returns></returns>
    [OnOpenAsset]
    public static bool OnOpenAsset(int instanceID, int line)
    {
        var treeAsset = EditorUtility.InstanceIDToObject(instanceID) as MessageTreeAsset;
        if (treeAsset != null)
        {
            var window = OpenMessageTreeViewWindow();
            window.SetTreeAsset(treeAsset);
            return true;
        }
        return false;
    }

    /// <summary>
    /// 设置消息数据
    /// </summary>
    /// <param name="treeAsset"></param>
    private void SetTreeAsset(MessageTreeAsset treeAsset)
    {
        mMessageTreeAsset = treeAsset;
        mIsInitalized = false;
    }

    /// <summary>
    /// 绘制
    /// </summary>
    private void OnGUI()
    {
        CheckInit();

        DisplayTreeViewArea(MultiColumnTreeViewRect);
    }

    /// <summary>
    /// 检查是否需要根据数据初始化
    /// </summary>
    private void CheckInit()
    {
        if(!mIsInitalized && mMessageTreeAsset != null)
        {
            // 根据消息数据Asset构建相关数据
            // 构建TreeView操作数据
            if(mTreeViewState == null)
            {
                mTreeViewState = new TreeViewState();
            }
            // 构建MultiColumnHeader数据
            bool firstInit = mMultiColumnHeaderState == null;
            var headerState = MessageTreeView.CreateDefaultMultiColumnHeaderState();
            if (MultiColumnHeaderState.CanOverwriteSerializedFields(mMultiColumnHeaderState, headerState))
            {
                MultiColumnHeaderState.OverwriteSerializedFields(mMultiColumnHeaderState, headerState);
            }
            mMultiColumnHeaderState = headerState;
            var multiColumnHeader = new MultiColumnHeader(headerState);
            if (firstInit)
            {
                multiColumnHeader.ResizeToFit();
            }
            // 通过自定义MessageTreeAsset构建自定义的MessageTreeModel
            var treeModel = new TreeModel<MessageTreeViewElement>(mMessageTreeAsset.MessageTreeElementList);
            // 通过构建的TreeViewState和MultiColumnHeader数据以及自定义MessageTreeAsset构建最终自定义的TreeView
            mMessageTreeView = new MessageTreeView(mTreeViewState, multiColumnHeader, treeModel);
            
            mIsInitalized = true;
        }
    }

    ///显示自定义TreeView信息区域
    ///<param name="rect">绘制区域信息</param>
    private void DisplayTreeViewArea(Rect rect)
    {
        mMessageTreeView?.OnGUI(rect);
    }
}