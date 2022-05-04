/*
 * Description:             MessageAsset.cs
 * Author:                  TONYTANG
 * Create Date:             2022/05/04
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MessageAsset.cs
/// 消息数据Asset
/// </summary>
[CreateAssetMenu(fileName = "MessageAsset", menuName = "ScriptableObject/Message Asset", order = 2)]
public class MessageAsset : ScriptableObject
{
    /// <summary>
    /// 消息说明
    /// </summary>
    [Serializable]
    public class MessageIntroduction
    {
        /// <summary>
        /// 消息名
        /// </summary>
        [Header("消息名")]
        public string Name;

        /// <summary>
        /// 消息介绍
        /// </summary>
        [Header("消息介绍")]
        public string Introduction;

        public MessageIntroduction(string name, string introduction)
        {
            Name = name;
            Introduction = introduction;
        }
    }

    /// <summary>
    /// 消息名黑名单列表
    /// </summary>
    [Header("消息名黑名单列表")]
    public List<string> MessageBlackList;

    /// <summary>
    /// 消息介绍列表
    /// </summary>
    [Header("消息介绍列表")]
    public List<MessageIntroduction> MessageIntroductionList;

    public MessageAsset()
    {
        MessageBlackList = new List<string>();
        MessageIntroductionList = new List<MessageIntroduction>();
    }

    /// <summary>
    /// 添加黑名单
    /// </summary>
    /// <param name="messageName"></param>
    /// <returns></returns>
    public void AddBlackList()
    {
        MessageBlackList.Add("黑名单待修改");
    }

    /// <summary>
    /// 添加消息介绍
    /// </summary>
    /// <param name="messageName"></param>
    /// <returns></returns>
    public void AddIntroduction()
    {
        MessageIntroductionList.Add(new MessageIntroduction("消息名待修改", "消息介绍待修改"));
    }

    /// <summary>
    /// 删除指定索引的黑名单
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public bool RemoveBlackListByIndex(int index)
    {
        if (MessageBlackList.Count > index && index >= 0)
        {
            MessageBlackList.RemoveAt(index);
            return true;
        }
        else
        {
            Debug.LogError($"找不到索引:{index}的黑名单,移除失败!");
            return false;
        }
    }

    /// <summary>
    /// 删除指定索引的消息介绍
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public bool RemoveIntroductionByIndex(int index)
    {
        if (MessageIntroductionList.Count > index && index >= 0)
        {
            MessageIntroductionList.RemoveAt(index);
            return true;
        }
        else
        {
            Debug.LogError($"找不到索引:{index}的消息介绍,移除失败!");
            return false;
        }
    }

    /// <summary>
    /// 获取消息介绍Map<消息名, 消息介绍>
    /// </summary>
    /// <returns></returns>
    public Dictionary<string, string> GetMessageIntroductionMap()
    {
        Dictionary<string, string> messageIntroductionMap = new Dictionary<string, string>();
        foreach(var messageIntroduction in MessageIntroductionList)
        {
            messageIntroductionMap.Add(messageIntroduction.Name, messageIntroduction.Introduction);
        }
        return messageIntroductionMap;
    }

    /// <summary>
    /// 检查有效性
    /// </summary>
    public void CheckValidation()
    {
        CheckMessageDuplicated();
    }

    /// <summary>
    /// 检查消息重名
    /// </summary>
    public void CheckMessageDuplicated()
    {
        CheckBlackListDuplicated();
        CheckIntroductionDumplicated();
    }

    /// <summary>
    /// 检查黑名单重名
    /// </summary>
    private void CheckBlackListDuplicated()
    {
        var messageNameMap = new Dictionary<string, bool>();
        for (int i = MessageBlackList.Count - 1; i >= 0; i--)
        {
            if (!messageNameMap.ContainsKey(MessageBlackList[i]))
            {
                messageNameMap.Add(MessageBlackList[i], true);
            }
            else
            {
                Debug.LogError($"重复添加同名:{MessageBlackList[i]}消息黑名单,自动删除!");
                MessageBlackList.RemoveAt(i);
            }
        }
    }

    /// <summary>
    /// 检查消息介绍重名
    /// </summary>
    private void CheckIntroductionDumplicated()
    {
        var messageNameMap = new Dictionary<string, bool>();
        for (int i = MessageIntroductionList.Count - 1; i >= 0; i--)
        {
            if (!messageNameMap.ContainsKey(MessageIntroductionList[i].Name))
            {
                messageNameMap.Add(MessageIntroductionList[i].Name, true);
            }
            else
            {
                Debug.LogError($"重复添加同名:{MessageIntroductionList[i].Name}消息介绍,自动删除!");
                MessageIntroductionList.RemoveAt(i);
            }
        }
    }

    /// <summary>
    /// 获取指定消息的介绍
    /// </summary>
    /// <param name="messageName"></param>
    /// <returns></returns>
    public string GetMessageIntroduction(string messageName)
    {
        var mesasgeIntroduction = MessageIntroductionList.Find((messageIntroduction)=>
        {
            return messageIntroduction.Name.Equals(messageName);
        });
        return mesasgeIntroduction != null ? mesasgeIntroduction.Introduction : string.Empty;
    }
}