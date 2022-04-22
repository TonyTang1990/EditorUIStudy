/*
 * Description:             MessageTreeAsset.cs
 * Author:                  TONYTANG
 * Create Date:             2022/04/22
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MessageTreeAsset.cs
/// 消息树形数据Asset
/// </summary>
[CreateAssetMenu(fileName = "MessageTreeAsset", menuName = "ScriptableObject/Message Tree Asset", order = 1)]
public class MessageTreeAsset : ScriptableObject
{
    /// <summary>
    /// 消息树形数据列表
    /// </summary>
    [SerializeField]
    public List<MessageTreeViewElement> MessageTreeElementList = new List<MessageTreeViewElement>();

    public MessageTreeAsset()
    {
        MessageTreeElementList = new List<MessageTreeViewElement>();
        var messageTreeElement = new MessageTreeViewElement(0, -1, "消息根节点");
        MessageTreeElementList.Add(messageTreeElement);
    }
}