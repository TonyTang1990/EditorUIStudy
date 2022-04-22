/*
 * Description:             TreeViewItemGeneric.cs
 * Author:                  TONYTANG
 * Create Date:             2022/04/21
 */

using System.Collections;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

/// <summary>
/// TreeViewItemGeneric.cs
/// 泛型树形单元数据
/// </summary>
public class TreeViewItemGeneric<T> : TreeViewItem where T : TreeViewElement
{
    /// <summary>
    /// 树形单元数据
    /// </summary>
    public T Data
    {
        get;
        set;
    }

    /// <summary>
    /// 带参构造函数
    /// </summary>
    /// <param name="id"></param>
    /// <param name="depth"></param>
    /// <param name="name"></param>
    /// <param name="data"></param>
    public TreeViewItemGeneric(int id, int depth, string name, T data) : base(id, depth, name)
    {
        Data = data;
    }
}