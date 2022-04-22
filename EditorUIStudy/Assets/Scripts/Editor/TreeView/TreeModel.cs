/*
 * Description:             TreeModel.cs
 * Author:                  TONYTANG
 * Create Date:             2022/04/21
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// TreeModel.cs
/// 泛型TreeView数据Model
/// </summary>
public class TreeModel<T> where T : TreeViewElement
{
    /// <summary>
    /// 数据列表
    /// </summary>
    public IList<T> DataList
    {
        get;
        set;
    }

    /// <summary>
    /// 根数据
    /// </summary>
    public T Root
    {
        get;
        set;
    }

    /// <summary>
    /// 数据变化委托
    /// </summary>
    public Action ModelChangeDelegate
    {
        get;
        set;
    }

    /// <summary>
    /// 最大数据ID
    /// </summary>
    private int mMaxID;

    /// <summary>
    /// 数据数量
    /// </summary>
    public int NumberOfData
    {
        get
        {
            return DataList.Count;
        }
    }

    /// <summary>
    /// 带参构造函数
    /// </summary>
    /// <param name="dataList"></param>
    public TreeModel(IList<T> dataList)
    {
        SetData(dataList);
    }

    /// <summary>
    /// 设置数据
    /// </summary>
    /// <param name="dataList"></param>
    public void SetData(IList<T> dataList)
    {
        Debug.Assert(dataList != null, "不允许传空列表数据!");
        DataList = dataList;
        mMaxID = DataList.Count;
        Root = TreeElementUtility.FindRootTreeViewElement<T>(DataList);
        Debug.Assert(Root != null, "找不到根节点数据!");
    }

    /// <summary>
    /// 生成下一个唯一ID
    /// </summary>
    /// <returns></returns>
    public int GenerateUniqueID()
    {
        return ++mMaxID;
    }

    /// <summary>
    /// 插入指定数据
    /// </summary>
    /// <param name="element">插入数据</param>
    /// <param name="parent">插入数据父节点</param>
    /// <param name="insertPosition">插入数据位置</param>
    public void AddElement(T element, TreeViewElement parent, int insertPosition)
    {
        Debug.Assert(element != null, "不允许插入空数据!");
        Debug.Assert(parent != null, "不允许插入空父节点的数据!");
        if (parent.ChildList == null)
        {
            parent.ChildList = new List<TreeViewElement>();
        }
        parent.ChildList.Insert(insertPosition, element);
        element.Parent = parent;

        ModelChange();
    }

    /// <summary>
    /// 移除指定数据
    /// </summary>
    /// <param name="element"></param>
    public bool RemoveElement(T element)
    {
        if(element == null)
        {
            Debug.LogError($"不允许移除空节点!");
            return false;
        }
        if (element == Root)
        {
            Debug.LogError($"不能允许移除根节点!");
            return false;
        }
        var result = DataList.Remove(element);
        if(result)
        {
            element.Parent.ChildList.Remove(element);
            ModelChange();
        }
        return result;
    }

    /// <summary>
    /// 移除指定id的数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool RemoveElementByID(int id)
    {
        var findElement = DataList.First(element => element.ID == id);
        return RemoveElement(findElement);
    }

    /// <summary>
    /// 数据变化
    /// </summary>
    private void ModelChange()
    {
        ModelChangeDelegate?.Invoke();
    }
}