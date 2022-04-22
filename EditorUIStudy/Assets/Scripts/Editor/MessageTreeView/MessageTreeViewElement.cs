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
    /// 消息名
    /// </summary>
    [Header("消息名")]
    public string MessageName;

    /// <summary>
    /// 消息内容
    /// </summary>
    [Header("消息内容")]
    public string MessageContent;

    /// <summary>
    /// 带参构造函数
    /// </summary>
    /// <param name="id"></param>
    /// <param name="depth"></param>
    /// <param name="name"></param>
    public MessageTreeViewElement(int id, int depth, string name) : base(id, depth, name)
    {

    }

    /// <summary>
    /// 带参构造函数
    /// </summary>
    /// <param name="id"></param>
    /// <param name="depth"></param>
    /// <param name="name"></param>
    /// <param name="messageName">消息名</param>
    /// <param name="messageContent">消息内容</param>
    public MessageTreeViewElement(int id, int depth, string name, string messageName, string messageContent) : base(id, depth, name)
    {
        MessageName = messageName;
        MessageContent = messageContent;
    }
}