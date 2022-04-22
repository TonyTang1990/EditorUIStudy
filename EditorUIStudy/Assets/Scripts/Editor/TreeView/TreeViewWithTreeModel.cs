/*
 * Description:             TreeViewWithTreeModel.cs
 * Author:                  TONYTANG
 * Create Date:             2022/04/21
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

/// <summary>
/// TreeViewWithTreeModel.cs
/// 带TreeModel的TreeView单元格数据
/// </summary>
public class TreeViewWithTreeModel<T> : TreeView where T : TreeViewElement
{
    /// <summary>
    /// TreeView数据
    /// </summary>
    public TreeModel<T> TreeModel;

    /// <summary>
    /// TreeView数据单元列表
    /// </summary>
    public List<TreeViewItem> RowList = new List<TreeViewItem>();

    /// <summary>
    /// 带参构造函数
    /// </summary>
    /// <param name="state">TreeView状态数据</param>
    /// <param name="model">TreeView数据Model</param>
    public TreeViewWithTreeModel(TreeViewState state, TreeModel<T> model) : base(state)
    {
        Init(model);
    }

    /// <summary>
    /// 带参构造函数
    /// </summary>
    /// <param name="state">TreeView状态数据</param>
    /// <param name="multiColumnHeader">TreeView多列相关数据</param>
    /// <param name="model">TreeView数据Model</param>
    public TreeViewWithTreeModel(TreeViewState state, MultiColumnHeader multiColumnHeader, TreeModel<T> model) : base(state, multiColumnHeader)
    {
        Init(model);
    }

    /// <summary>
    /// 初始化TreeView数据Model
    /// </summary>
    /// <param name="model"></param>
    private void Init(TreeModel<T> model)
    {
        TreeModel = model;
        TreeModel.ModelChangeDelegate += ModelChanged;
    }

    /// <summary>
    /// 构建TreeView根节点
    /// </summary>
    /// <returns></returns>
    protected override TreeViewItem BuildRoot()
    {
        return new TreeViewItemGeneric<T>(TreeModel.Root.ID, -1, TreeModel.Root.Name, TreeModel.Root);
    }

    /// <summary>
    /// 构建TreeView单行数据节点
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    protected override IList<TreeViewItem> BuildRows(TreeViewItem root)
    {
        if (TreeModel.Root == null)
        {
            Debug.LogError("TreeModel Root is null. Did you call SetData()?");
        }

        RowList.Clear();
        if (!string.IsNullOrEmpty(searchString))
        {
            Search(TreeModel.Root, searchString, RowList);
        }
        else
        {
            if (TreeModel.Root.HasChild())
            {
                AddChildrenRecursive(TreeModel.Root, 0, RowList);
            }
        }

        // 自动构建节点深度信息
        SetupParentsAndChildrenFromDepths(root, RowList);

        return RowList;
    }

    /// <summary>
    /// 搜索节点信息
    /// </summary>
    /// <param name="searchFromThis"></param>
    /// <param name="search"></param>
    /// <param name="result"></param>
    void Search(T searchFromThis, string search, List<TreeViewItem> result)
    {
        if (string.IsNullOrEmpty(search))
        {
            throw new ArgumentException("Invalid search: cannot be null or empty", "search");
        }

        const int deepth = 0;

        Stack<T> stack = new Stack<T>();
        foreach (var element in searchFromThis.ChildList)
        {
            stack.Push((T)element);
        }
        while (stack.Count > 0)
        {
            T current = stack.Pop();
            if (current.Name.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                result.Add(new TreeViewItemGeneric<T>(current.ID, deepth, current.Name, current));
            }
            if (current.HasChild())
            {
                foreach (var element in current.ChildList)
                {
                    stack.Push((T)element);
                }
            }
        }
        SortSearchResult(result);
    }

    /// <summary>
    /// 行数据列表排序
    /// </summary>
    /// <param name="rows"></param>
    protected virtual void SortSearchResult(List<TreeViewItem> rows)
    {
        rows.Sort((x, y) => EditorUtility.NaturalCompare(x.displayName, y.displayName)); // sort by displayName by default, can be overriden for multicolumn solutions
    }

    /// <summary>
    /// 递归添加子节点
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="depth"></param>
    /// <param name="newRows"></param>
    void AddChildrenRecursive(T parent, int depth, IList<TreeViewItem> newRows)
    {
        foreach (T child in parent.ChildList)
        {
            var item = new TreeViewItemGeneric<T>(child.ID, depth, child.Name, child);
            newRows.Add(item);

            if (child.HasChild())
            {
                if (IsExpanded(child.ID))
                {
                    AddChildrenRecursive(child, depth + 1, newRows);
                }
                else
                {
                    item.children = CreateChildListForCollapsedParent();
                }
            }
        }
    }

    /// <summary>
    /// 响应数据Model变化
    /// </summary>
    private void ModelChanged()
    {
        Reload();
    }
}