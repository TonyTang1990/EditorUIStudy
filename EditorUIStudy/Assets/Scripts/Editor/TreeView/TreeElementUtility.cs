/*
 * Description:             TreeElementUtility.cs
 * Author:                  TONYTANG
 * Create Date:             2022/04/21
 */

using System;
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
    /// 将树形根节点转换到数据列表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="root"></param>
    /// <param name="result"></param>
    public static void TreeToList<T>(T root, IList<T> result) where T : TreeViewElement
    {
        if (result == null)
        {
            throw new NullReferenceException("The input 'IList<T> result' list is null");
        }
        result.Clear();

        Stack<T> stack = new Stack<T>();
        stack.Push(root);

        while (stack.Count > 0)
        {
            T current = stack.Pop();
            result.Add(current);

            if (current.ChildList != null && current.ChildList.Count > 0)
            {
                for (int i = current.ChildList.Count - 1; i >= 0; i--)
                {
                    stack.Push((T)current.ChildList[i]);
                }
            }
        }
    }

    /// <summary>
    /// 从数据列表里生成TreeView
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <returns>返回根节点</returns>
    public static T ListToTree<T>(IList<T> list) where T : TreeViewElement
    {
        // Validate input
        ValidateDepthValues(list);

        // Clear old states
        foreach (var element in list)
        {
            element.Parent = null;
            element.ChildList = null;
        }

        // Set child and parent references using depth info
        for (int parentIndex = 0; parentIndex < list.Count; parentIndex++)
        {
            var parent = list[parentIndex];
            bool alreadyHasValidChildren = parent.ChildList != null;
            if (alreadyHasValidChildren)
            {
                continue;
            }

            int parentDepth = parent.Depth;
            int childCount = 0;

            // Count children based depth value, we are looking at children until it's the same depth as this object
            for (int i = parentIndex + 1; i < list.Count; i++)
            {
                if (list[i].Depth == parentDepth + 1)
                {
                    childCount++;
                }
                if (list[i].Depth <= parentDepth)
                {
                    break;
                }
            }

            // Fill child array
            List<TreeViewElement> childList = null;
            if (childCount != 0)
            {
                childList = new List<TreeViewElement>(childCount); // Allocate once
                childCount = 0;
                for (int i = parentIndex + 1; i < list.Count; i++)
                {
                    if (list[i].Depth == parentDepth + 1)
                    {
                        list[i].Parent = parent;
                        childList.Add(list[i]);
                        childCount++;
                    }

                    if (list[i].Depth <= parentDepth)
                    {
                        break;
                    }
                }
            }

            parent.ChildList = childList;
        }

        return list[0];
    }


    /// <summary>
    /// 检查数据列表信息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    public static void ValidateDepthValues<T>(IList<T> list) where T : TreeViewElement
    {
        if (list.Count == 0)
        {
            throw new ArgumentException("数据列表长度不能为0");
        }

        if (list[0].Depth != -1)
        {
            throw new ArgumentException($"数据列表第一个数据深度:{list[0].Depth}必须为深度-1!");
        }

        for (int i = 0; i < list.Count - 1; i++)
        {
            int depth = list[i].Depth;
            int nextDepth = list[i + 1].Depth;
            if (nextDepth > depth && nextDepth - depth > 1)
            {
                throw new ArgumentException($"数据列表深度的插值必须在0或者1. 索引:{i}深度值:{depth} 索引:{i + 1}深度值:{nextDepth}");
            }
        }

        for (int i = 1; i < list.Count; ++i)
        {
            if (list[i].Depth < 0)
            {
                throw new ArgumentException($"索引:{i}深度值:{list[i].Depth}除了第一位数据深度都不应该小于0! ");
            }
        }

        if (list.Count > 1 && list[1].Depth != 0)
        {
            throw new ArgumentException("第二个数据节点深度必须为0");
        }
    }

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