/*
 * Description:             TreeElementUtility.cs
 * Author:                  TONYTANG
 * Create Date:             2022/04/21
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// TreeElementUtility.cs
/// TreeView工具类
/// </summary>
public static class TreeElementUtility
{
    /// <summary>
    /// 查找TreeView列表的根数据
    /// </summary>
    /// <param name="treeViewElementList"></param>
    /// <returns></returns>
    public static T FindRootTreeViewElement<T>(IList<T> treeViewElementList) where T : TreeViewElement
    {
        foreach(var treeViewElement in treeViewElementList)
        {
            if(treeViewElement.Depth == -1)
            {
                return treeViewElement;
            }
        }
        Debug.LogError($"找不到TreeView列表的根节点(depth == -1)!");
        return null;
    }

    /// <summary>
    /// 更新节点深度信息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="root"></param>
    public static void UpdateDepthValues<T>(T root) where T : TreeViewElement
    {
        Debug.Assert(root != null, "不允许更新空根节点的深度信息!");

        if(!root.IsRoot())
        {
            Debug.LogError($"不允许更新非根节点的深度信息!");
            return;
        }

        if (!root.HasChild())
        {
            return;
        }

        Stack<TreeViewElement> stack = new Stack<TreeViewElement>();
        stack.Push(root);
        while (stack.Count > 0)
        {
            TreeViewElement current = stack.Pop();
            if (current.ChildList != null)
            {
                foreach (var child in current.ChildList)
                {
                    child.Depth = current.Depth + 1;
                    stack.Push(child);
                }
            }
        }
    }
}