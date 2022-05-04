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
    /// 查找指定id的数据成员
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public T Find(int id)
    {
        return DataList.FirstOrDefault(element => element.ID == id);
    }

    /// <summary>
    /// 设置数据
    /// </summary>
    /// <param name="dataList"></param>
    public void SetData(IList<T> dataList)
    {
        Debug.Assert(dataList != null, "不允许传空列表数据!");
        DataList = dataList;
        if (DataList.Count > 0)
        {
            Root = TreeElementUtility.ListToTree(DataList);
        }
        mMaxID = DataList.Count;
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
    /// 获取指定id的所有上层父节点id列表
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public IList<int> GetAncestors(int id)
    {
        var parents = new List<int>();
        TreeViewElement T = Find(id);
        if (T != null)
        {
            while (T.Parent != null)
            {
                parents.Add(T.Parent.ID);
                T = T.Parent;
            }
        }
        return parents;
    }

    /// <summary>
    /// 插入指定数据
    /// </summary>
    /// <param name="element">插入数据</param>
    /// <param name="parent">插入数据父节点</param>
    /// <param name="insertPosition">插入数据位置</param>
    public void AddElement(T element, TreeViewElement parent = null, int insertPosition = 0)
    {
        Debug.Assert(element != null, "不允许插入空数据!");
        // 默认不传父节点为根节点
        if(parent == null)
        {
            parent = Root;
        }
        if (parent.ChildList == null)
        {
            parent.ChildList = new List<TreeViewElement>();
        }
        parent.ChildList.Insert(insertPosition, element);
        element.Parent = parent;

        // 更新子节点深度信息
        TreeElementUtility.UpdateDepthValues(parent);
        // 更新成员列表数据
        TreeElementUtility.TreeToList(Root, DataList);

        ModelChange();
    }

    /// <summary>
    /// 移除指定数据
    /// </summary>
    /// <param name="element"></param>
    /// <param name="triggerModelChange">是否触发数据改变回调</param>
    public bool RemoveElement(T element, bool triggerModelChange = true)
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
            if(triggerModelChange)
            {
                // 更新成员列表数据
                TreeElementUtility.TreeToList(Root, DataList);
                ModelChange();
            }
        }
        return result;
    }

    /// <summary>
    /// 移除指定成员数据列表
    /// </summary>
    /// <param name="elements"></param>
    public void RemoveElements(IList<T> elements)
    {
        for(int i = elements.Count - 1; i >= 0; i--)
        {
            if (elements[i] == Root)
            {
                throw new ArgumentException("It is not allowed to remove the root element");
            }
            else
            {
                RemoveElement(elements[i], false);
            }
        }

        // 更新成员列表数据
        TreeElementUtility.TreeToList(Root, DataList);
        ModelChange();
    }


    /// <summary>
    /// 移除指定id的数据成员
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool RemoveElementByID(int id)
    {
        var findElement = DataList.First(element => element.ID == id);
        return RemoveElement(findElement);
    }

    /// <summary>
    /// 移除指定id列表的数据成员
    /// </summary>
    /// <param name="elementIDs"></param>
    public void RemoveElementsByID(IList<int> elementIDs)
    {
        IList<T> elements = DataList.Where(element => elementIDs.Contains(element.ID)).ToArray();
        RemoveElements(elements);
    }

    /// <summary>
    /// 数据变化
    /// </summary>
    private void ModelChange()
    {
        ModelChangeDelegate?.Invoke();
    }
}