/*
 * Description:             MessageWindow.cs
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
/// MessageWindow.cs
/// 消息辅助窗口
/// </summary>
public class MessageWindow : BaseEditorWindow
{
    /// <summary>
    /// 常规高度
    /// </summary>
    private const float NormalHeight = 20f;

    /// <summary>
    /// 常规宽度
    /// </summary>
    private const float NormalWidth = 20f;

    /// <summary>
    /// 消息辅助窗口Styles定义
    /// </summary>
    public static class MessageStyles
    {
        public static GUIStyle TabMiddleStyle = new GUIStyle("Tab middle");

        public static GUIStyle AppToolbarButtonMidStyle = new GUIStyle("AppToolbarButtonMid");
    }

    [MenuItem("Tools/消息辅助窗口")]
    public static MessageWindow OpenMessageWindow()
    {
        var messageTreeViewWindow = EditorWindow.GetWindow(typeof(MessageWindow)) as MessageWindow;
        messageTreeViewWindow.titleContent = new GUIContent("消息辅助窗口");
        messageTreeViewWindow.Focus();
        messageTreeViewWindow.Repaint();
        return messageTreeViewWindow;
    }

    /// <summary>
    /// 初始化窗口数据
    /// </summary>
    protected override void InitData()
    {
        base.InitData();
        InitMessageStatisticData();
        InitMessageTreeViewData();
    }

    /// <summary>
    /// 保存数据
    /// </summary>
    protected override void SaveData()
    {
        base.SaveData();
        SaveMessageStatisticData();
        SaveMessageTreeViewData();
    }

    /// <summary>
    /// 绘制
    /// </summary>
    private void OnGUI()
    {
        DrawTagArea();
        DrawTagSelectionArea();
    }

    #region 消息页签部分
    /// <summary>
    /// 消息树状结构页签
    /// </summary>
    public enum MessageTag
    {
        MessageStatistic = 0,               // 消息统计
        MessagePreview,                     // 消息浏览
        MessageSimulation,                  // 消息模拟
    }

    /// <summary>
    /// 页签选中索引
    /// </summary>
    private int mTagSelectedIndex;

    /// <summary>
    /// 页签名
    /// </summary>
    private string[] mTagNames = new string[] { "消息统计", "消息预览", "消息模拟" };

    /// <summary>
    /// 设置消息页签
    /// </summary>
    /// <param name="messageTag"></param>
    public void SelectMessageTag(MessageTag messageTag)
    {
        mTagSelectedIndex = (int)messageTag;
    }

    /// <summary>
    /// 绘制页签部分
    /// </summary>
    private void DrawTagArea()
    {
        EditorGUI.BeginChangeCheck();
        mTagSelectedIndex = GUILayout.SelectionGrid(mTagSelectedIndex, mTagNames, mTagNames.Length);
        if(EditorGUI.EndChangeCheck())
        {
            SelectMessageTag((MessageTag)mTagSelectedIndex);
        }
    }
    #endregion

    /// <summary>
    /// 当前滚动位置
    /// </summary>
    private Vector2 mCurrentScrollPos;

    /// <summary>
    /// 绘制消息页签选择区域
    /// </summary>
    private void DrawTagSelectionArea()
    {
        mCurrentScrollPos = EditorGUILayout.BeginScrollView(mCurrentScrollPos);
        if (mTagSelectedIndex == (int)MessageTag.MessageStatistic)
        {
            DrawMessageStatisticArea();
        }
        else if (mTagSelectedIndex == (int)MessageTag.MessagePreview)
        {
            DrawMessagePreviewArea();
        }
        else if (mTagSelectedIndex == (int)MessageTag.MessageSimulation)
        {
            DrawMessageSimulationArea();
        }
        EditorGUILayout.EndScrollView();
    }

    #region 消息统计部分
    /// <summary>
    /// 消息Asset相对路径
    /// </summary>
    private const string MessageAssetPath = "Assets/Scripts/Editor/MessageTreeView/MessageAsset.asset";

    /// <summary>
    /// 消息数据Asset
    /// </summary>
    private MessageAsset mMessageAsset;

    /// <summary>
    /// 是否开始统计消息
    /// </summary>
    private bool mIsStartMessageStatistic;

    /// <summary>
    /// 添加的消息名
    /// </summary>
    private string mAddedMessageName;

    /// <summary>
    /// 添加的消息类型
    /// </summary>
    private MessageTreeViewElement.MessageType mAddedMessageType = MessageTreeViewElement.MessageType.Request;

    /// <summary>
    /// 添加的消息注释
    /// </summary>
    private string mAddedMessageAnnotation;

    /// <summary>
    /// 黑名单区域折叠
    /// </summary>
    private bool mBlackListAreaFold;

    /// <summary>
    /// 消息介绍折叠
    /// </summary>
    private bool mIntroductionAreaFold;

    /// <summary>
    /// 初始化消息统计数据
    /// </summary>
    private void InitMessageStatisticData()
    {
        if(!LoadMessageAsset())
        {
            CreateMessageAsset();
            LoadMessageAsset();
        }
        mMessageAsset.CheckValidation();
    }

    /// <summary>
    /// 加载消息Asset
    /// </summary>
    /// <returns></returns>
    private bool LoadMessageAsset()
    {
        mMessageAsset = AssetDatabase.LoadAssetAtPath<MessageAsset>(MessageAssetPath);
        return mMessageAsset != null;
    }

    /// <summary>
    /// 创建消息Asset
    /// </summary>
    private void CreateMessageAsset()
    {
        AssetDatabase.CreateAsset(new MessageAsset(), MessageAssetPath);
    }

    /// <summary>
    /// 保存消息统计数据
    /// </summary>
    private void SaveMessageStatisticData()
    {

    }

    /// <summary>
    /// 绘制消息统计部分
    /// </summary>
    private void DrawMessageStatisticArea()
    {
        EditorGUILayout.BeginVertical();
        DrawMessageOperationArea();
        DrawMessageManuallyArea();
        DrawMessageBlackListArea();
        DrawMessageIntroductionArea();
        EditorGUILayout.EndVertical();
    }

    /// <summary>
    /// 绘制消息操作区域
    /// </summary>
    private void DrawMessageOperationArea()
    {
        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.LabelField("消息操作区域", GUILayout.ExpandWidth(true), GUILayout.Height(NormalHeight));
        EditorGUILayout.BeginHorizontal();
        var btnColor = mIsStartMessageStatistic ? Color.yellow : Color.white;
        var preColor = GUI.color;
        GUI.color = btnColor;
        if (GUILayout.Button("开始消息统计", GUILayout.ExpandWidth(true), GUILayout.Height(NormalHeight)))
        {
            mIsStartMessageStatistic = true;
            ShowNotification(new GUIContent($"开始消息统计!"));
        }
        GUI.color = preColor;
        btnColor = !mIsStartMessageStatistic ? Color.yellow : Color.white;
        preColor = GUI.color;
        GUI.color = btnColor;
        if (GUILayout.Button("停止消息统计", GUILayout.ExpandWidth(true), GUILayout.Height(NormalHeight)))
        {
            mIsStartMessageStatistic = false;
            ShowNotification(new GUIContent($"停止消息统计!"));
        }
        GUI.color = preColor;
        if (GUILayout.Button("导出Proto", GUILayout.ExpandWidth(true), GUILayout.Height(NormalHeight)))
        {
            ShowNotification(new GUIContent($"导出Proto!"));
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
    }

    /// <summary>
    /// 绘制消息手动区域
    /// </summary>
    private void DrawMessageManuallyArea()
    {
        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.LabelField("消息手动添加区域", GUILayout.ExpandWidth(true), GUILayout.Height(NormalHeight));
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("消息名", MessageStyles.TabMiddleStyle, GUILayout.ExpandWidth(true), GUILayout.Height(NormalHeight));
        EditorGUILayout.LabelField("消息类型", MessageStyles.TabMiddleStyle, GUILayout.ExpandWidth(true), GUILayout.Height(NormalHeight));
        EditorGUILayout.LabelField("消息注释", MessageStyles.TabMiddleStyle, GUILayout.ExpandWidth(true), GUILayout.Height(NormalHeight));
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        mAddedMessageName = EditorGUILayout.TextField(mAddedMessageName, MessageStyles.TabMiddleStyle, GUILayout.ExpandWidth(true), GUILayout.Height(NormalHeight));
        mAddedMessageType = (MessageTreeViewElement.MessageType)EditorGUILayout.EnumPopup(mAddedMessageType, MessageStyles.TabMiddleStyle, GUILayout.ExpandWidth(true), GUILayout.Height(NormalHeight));
        mAddedMessageAnnotation = EditorGUILayout.TextField(mAddedMessageAnnotation, MessageStyles.TabMiddleStyle, GUILayout.ExpandWidth(true), GUILayout.Height(NormalHeight));
        EditorGUILayout.EndHorizontal();
        if (GUILayout.Button("+", GUILayout.ExpandWidth(true), GUILayout.Height(NormalHeight)))
        {
            if(string.IsNullOrEmpty(mAddedMessageName))
            {
                ShowNotification(new GUIContent($"消息名不允许填空,手动添加消息失败!"));
            }
            else
            {
                if(mMessageTreeView != null)
                {
                    var id = mMessageTreeView.TreeModel.GenerateUniqueID();
                    // 往根节点添加数据
                    var messageElement = new MessageTreeViewElement(id, 0, mAddedMessageName, mAddedMessageType, string.Empty, mAddedMessageAnnotation);
                    mMessageTreeView.TreeModel.AddElement(messageElement);
                    ShowNotification(new GUIContent($"添加消息:{mAddedMessageName}成功!"));
                }
            }
        }
        EditorGUILayout.EndVertical();
    }

    /// <summary>
    /// 绘制消息
    /// </summary>
    private void DrawMessageBlackListArea()
    {
        EditorGUILayout.BeginVertical("box");
        mBlackListAreaFold = EditorGUILayout.Foldout(mBlackListAreaFold, "消息黑名单区域");
        if (mBlackListAreaFold)
        {
            if (mMessageAsset.MessageBlackList.Count > 0)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("消息名", MessageStyles.TabMiddleStyle, GUILayout.ExpandWidth(true), GUILayout.Height(NormalHeight));
                EditorGUILayout.EndHorizontal();
                for (int i = mMessageAsset.MessageBlackList.Count - 1; i >= 0; i--)
                {
                    DrawOneMessageBlackListArea(i);
                }
            }
            if (GUILayout.Button("+", GUILayout.ExpandWidth(true), GUILayout.Height(NormalHeight)))
            {
                mMessageAsset.AddBlackList();
            }
        }
        EditorGUILayout.EndVertical();
    }

    /// <summary>
    /// 绘制指定索引的消息黑名单
    /// </summary>
    /// <param name="index"></param>
    private void DrawOneMessageBlackListArea(int index)
    {
        EditorGUILayout.BeginHorizontal();
        mMessageAsset.MessageBlackList[index] = EditorGUILayout.TextField(mMessageAsset.MessageBlackList[index], MessageStyles.TabMiddleStyle, GUILayout.ExpandWidth(true), GUILayout.Height(NormalHeight));
        if(GUILayout.Button("-", GUILayout.Width(NormalWidth), GUILayout.Height(NormalHeight)))
        {
            mMessageAsset.RemoveBlackListByIndex(index);
        }
        EditorGUILayout.EndHorizontal();
    }

    /// <summary>
    /// 绘制消息介绍区域
    /// </summary>
    private void DrawMessageIntroductionArea()
    {
        EditorGUILayout.BeginVertical("box");
        mIntroductionAreaFold = EditorGUILayout.Foldout(mIntroductionAreaFold, "消息介绍区域");
        if (mIntroductionAreaFold)
        {
            if (mMessageAsset.MessageIntroductionList.Count > 0)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("消息名", MessageStyles.TabMiddleStyle, GUILayout.ExpandWidth(true), GUILayout.Height(NormalHeight));
                EditorGUILayout.LabelField("消息介绍", MessageStyles.TabMiddleStyle, GUILayout.ExpandWidth(true), GUILayout.Height(NormalHeight));
                EditorGUILayout.EndHorizontal();
                for (int i = mMessageAsset.MessageIntroductionList.Count - 1; i >= 0; i--)
                {
                    DrawOneMessageIntroductionArea(i);
                }
            }
            if (GUILayout.Button("+", GUILayout.ExpandWidth(true), GUILayout.Height(NormalHeight)))
            {
                mMessageAsset.AddIntroduction();
            }
        }
        EditorGUILayout.EndVertical();
    }

    /// <summary>
    /// 绘制指定索引的消息介绍
    /// </summary>
    /// <param name="index"></param>
    private void DrawOneMessageIntroductionArea(int index)
    {
        EditorGUILayout.BeginHorizontal();
        mMessageAsset.MessageIntroductionList[index].Name = EditorGUILayout.TextField(mMessageAsset.MessageIntroductionList[index].Name, GUILayout.ExpandWidth(true), GUILayout.Height(NormalHeight));
        mMessageAsset.MessageIntroductionList[index].Introduction = EditorGUILayout.TextField(mMessageAsset.MessageIntroductionList[index].Introduction, GUILayout.ExpandWidth(true), GUILayout.Height(NormalHeight));
        if (GUILayout.Button("-", GUILayout.Width(NormalWidth), GUILayout.Height(NormalHeight)))
        {
            mMessageAsset.RemoveIntroductionByIndex(index);
        }
        EditorGUILayout.EndHorizontal();
    }
    #endregion

    #region 消息预览部分
    /// <summary>
    /// 默认消息TreeViewAsset相对路径
    /// </summary>
    private const string MessageTreeAssetPath = "Assets/Scripts/Editor/MessageTreeView/MessageTreeAsset.asset";

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
    /// 搜索绘制区域
    /// </summary>
    private Rect SearchRect
    {
        get { return new Rect(20, 10, position.width - 40f, 20f); }
    }

    /// <summary>
    /// TreeView绘制区域
    /// </summary>
    private Rect MultiColumnTreeViewRect
    {
        get { return new Rect(20, 30, position.width - 40, position.height - 60); }
    }
    
    /// <summary>
    /// 搜索框
    /// </summary>
    private SearchField mSearchField;

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
            var window = OpenMessageWindow();
            window.SetTreeAsset(treeAsset);
            window.SelectMessageTag(MessageTag.MessagePreview);
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
    /// 初始化消息TreeView数据
    /// </summary>
    private void InitMessageTreeViewData()
    {
        if (!LoadMessageTreeAsset())
        {
            CreateMessageTreeAsset();
            LoadMessageTreeAsset();
        }
    }

    /// <summary>
    /// 保存消息TreeView数据
    /// </summary>
    private void SaveMessageTreeViewData()
    {

    }

    /// <summary>
    /// 加载消息TreeView数据
    /// </summary>
    /// <returns></returns>
    private bool LoadMessageTreeAsset()
    {
        var messageTreeAsset = AssetDatabase.LoadAssetAtPath<MessageTreeAsset>(MessageTreeAssetPath);
        if(messageTreeAsset != null)
        {
            SetTreeAsset(messageTreeAsset);
        }
        return messageTreeAsset != null;
    }

    /// <summary>
    /// 创建消息TreeView Asset
    /// </summary>
    private void CreateMessageTreeAsset()
    {
        AssetDatabase.CreateAsset(new MessageTreeAsset(), MessageTreeAssetPath);
    }

    /// <summary>
    /// 绘制消息预览区域
    /// </summary>
    private void DrawMessagePreviewArea()
    {
        CheckInit();
        DrawSearchBarArea(SearchRect);
        DrawTreeViewArea(MultiColumnTreeViewRect);
    }

    /// <summary>
    /// 检查是否需要根据数据初始化
    /// </summary>
    private void CheckInit()
    {
        if (!mIsInitalized && mMessageTreeAsset != null)
        {
            // 根据消息数据Asset构建相关数据
            // 构建TreeView操作数据
            if (mTreeViewState == null)
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

            mSearchField = new SearchField();
            // 将搜索栏的输入回调和TreeView关联方便搜索检测
            mSearchField.downOrUpArrowKeyPressed += mMessageTreeView.SetFocusAndEnsureSelectedItem;

            mIsInitalized = true;
        }
    }

    /// <summary>
    /// 绘制搜索区域
    /// </summary>
    ///<param name="rect">绘制区域信息</param>
    private void DrawSearchBarArea(Rect rect)
    {
        if(mMessageTreeView != null)
        {
            // 显示关联TreeView的搜索栏
            mMessageTreeView.searchString = mSearchField.OnGUI(rect, mMessageTreeView.searchString);
        }

    }

    ///显示自定义TreeView信息区域
    ///<param name="rect">绘制区域信息</param>
    private void DrawTreeViewArea(Rect rect)
    {
        mMessageTreeView?.OnGUI(rect);
    }
    #endregion

    #region 消息模拟部分
    /// <summary>
    /// 选中模拟消息
    /// </summary>
    private MessageTreeViewElement mSelectedMessageElement;

    /// <summary>
    /// 消息请求内容替换开关
    /// </summary>
    private bool mIsRequestContentReplace;

    /// <summary>
    /// 设置选中模拟消息
    /// </summary>
    /// <param name="messageElement"></param>
    public void SetSelectedMessageSimulation(MessageTreeViewElement messageElement)
    {
        mSelectedMessageElement = messageElement;
    }

    /// <summary>
    /// 绘制消息编辑区域
    /// </summary>
    private void DrawMessageSimulationArea()
    {
        EditorGUILayout.BeginVertical();
        if(mSelectedMessageElement != null)
        {
            DrawMessageContentArea();
            DrawMessageSimulationOperationArea();
        }
        else
        {
            EditorGUILayout.LabelField("未选中有效消息，请先从消息预览里选中需要编辑的消息!", MessageStyles.TabMiddleStyle, GUILayout.ExpandWidth(true), GUILayout.Height(NormalHeight));
        }
        EditorGUILayout.EndVertical();
    }

    /// <summary>
    /// 绘制消息内容区域
    /// </summary>
    private void DrawMessageContentArea()
    {
        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("消息名:", MessageStyles.TabMiddleStyle, GUILayout.Width(150f), GUILayout.Height(NormalHeight));
        EditorGUILayout.LabelField(mSelectedMessageElement.MsgName, MessageStyles.TabMiddleStyle, GUILayout.ExpandWidth(true), GUILayout.Height(NormalHeight));
        EditorGUILayout.LabelField("消息类型:", MessageStyles.TabMiddleStyle, GUILayout.Width(150f), GUILayout.Height(NormalHeight));
        var iconTexture = mSelectedMessageElement.MsgType == MessageTreeViewElement.MessageType.Push ? MessageTreeView.PushIconTexture : MessageTreeView.PullIconTexture;
        GUILayout.Label(iconTexture, GUILayout.Width(40f), GUILayout.Height(NormalHeight));
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
    }

    /// <summary>
    /// 绘制消息模拟操作区域
    /// </summary>
    private void DrawMessageSimulationOperationArea()
    {
        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.LabelField("消息内容", MessageStyles.TabMiddleStyle, GUILayout.ExpandWidth(true), GUILayout.Height(NormalHeight));
        mSelectedMessageElement.MsgContent = EditorGUILayout.TextArea(mSelectedMessageElement.MsgContent, GUILayout.ExpandWidth(true), GUILayout.MinHeight(500f));
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("填充默认消息内容", GUILayout.ExpandWidth(true), GUILayout.Height(20f)))
        {

        }
        if (GUILayout.Button("检查消息内容有效性", GUILayout.ExpandWidth(true), GUILayout.Height(20f)))
        {

        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        if (mSelectedMessageElement.MsgType == MessageTreeViewElement.MessageType.Push)
        {
            if (GUILayout.Button("模拟消息推送", GUILayout.ExpandWidth(true), GUILayout.Height(NormalHeight)))
            {
                ShowNotification(new GUIContent($"模拟消息:{mSelectedMessageElement.MsgName}的推送!"));
            }
        }
        else if (mSelectedMessageElement.MsgType == MessageTreeViewElement.MessageType.Request)
        {
            var preColor = GUI.color;
            var color = mIsRequestContentReplace ? Color.green : Color.white;
            GUI.color = color;
            if (GUILayout.Button("消息请求内容替换", GUILayout.ExpandWidth(true), GUILayout.Height(NormalHeight)))
            {
                mIsRequestContentReplace = !mIsRequestContentReplace;
                var content = mIsRequestContentReplace ? "开启消息请求内容替换" : "关闭消息请求内容替换";
                ShowNotification(new GUIContent(content));
            }
            GUI.color = preColor;
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
    }
    #endregion
}