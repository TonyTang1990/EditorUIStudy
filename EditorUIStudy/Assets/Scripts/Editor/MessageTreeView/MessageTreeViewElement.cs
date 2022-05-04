/*
 * Description:             MessageTreeViewElement.cs
 * Author:                  TONYTANG
 * Create Date:             2022/04/22
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MessageTreeViewElement.cs
/// 消息树形数据结构
/// </summary>
[Serializable]
public class MessageTreeViewElement : TreeViewElement
{
    /// <summary>
    /// 消息类型
    /// </summary>
    public enum MessageType
    {
        Request = 0,
        Push,
    }

    /// <summary>
    /// 消息名
    /// </summary>
    [Header("消息名")]
    public string MsgName;

    /// <summary>
    /// 消息类型
    /// </summary>
    [Header("消息类型")]
    public MessageTreeViewElement.MessageType MsgType;

    /// <summary>
    /// 消息内容
    /// </summary>
    [Header("消息内容")]
    public string MsgContent;

    /// <summary>
    /// 消息注释
    /// </summary>
    [Header("消息注释")]
    public string MsgAnnotation;

    /// <summary>
    /// 带参构造函数
    /// </summary>
    /// <param name="id"></param>
    /// <param name="depth"></param>
    /// <param name="name"></param>
    public MessageTreeViewElement(int id, int depth, string name) : base(id, depth, name)
    {
        MsgName = name;
    }

    /// <summary>
    /// 带参构造函数
    /// </summary>
    /// <param name="id">唯一id</param>
    /// <param name="depth">深度值</param>
    /// <param name="name">显示名字</param>
    /// <param name="msgType">消息类型</param>
    /// <param name="msgContent">消息内容</param>
    /// <param name="msgAnnotation">消息注释</param>
    public MessageTreeViewElement(int id, int depth, string name, MessageType msgType, string msgContent, string msgAnnotation) : base(id, depth, name)
    {
        MsgName = name;
        MsgType = msgType;
        MsgContent = msgContent;
        MsgAnnotation = msgAnnotation;
    }
}