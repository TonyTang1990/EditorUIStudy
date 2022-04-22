/*
 * Description:             TreeViewElement.cs
 * Author:                  TONYTANG
 * Create Date:             2022/04/21
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// TreeViewElement.cs
/// 树形数据结构基类抽象
/// </summary>
[Serializable]
public class TreeViewElement
{
    /// <summary>
    /// UID
    /// </summary>
    [SerializeField]
    public int ID;

    /// <summary>
    /// 名字
    /// </summary>
    [SerializeField]
    public string Name;

    /// <summary>
    /// 树形结构深度
    /// </summary>
    [SerializeField]
    public int Depth;

    /// <summary>
    /// 树形父节点
    /// </summary>
    [NonSerialized]
    public TreeViewElement Parent;

    /// <summary>
    /// 树形子节点列表
    /// </summary>
    [NonSerialized]
    public List<TreeViewElement> ChildList;

    /// <summary>
    /// 带参构造函数
    /// </summary>
    /// <param name="id"></param>
    /// <param name="depth"></param>
    /// <param name="name"></param>
    public TreeViewElement(int id, int depth, string name)
    {
        ID = id;
        Name = name;
        Depth = depth;
    }

    /// <summary>
    /// 是否是根节点
    /// </summary>
    /// <returns></returns>
    public bool IsRoot()
    {
        return Depth == -1;
    }

    /// <summary>
    /// 是否有子节点
    /// </summary>
    /// <returns></returns>
    public bool HasChild()
    {
        return ChildList != null ? ChildList.Count > 0 : false;
    }
}