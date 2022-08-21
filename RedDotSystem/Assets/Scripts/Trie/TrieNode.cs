/*
 * Description:             TrieNode.cs
 * Author:                  TONYTANG
 * Create Date:             2022/08/12
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// TrieNode.cs
/// 前缀树节点类
/// </summary>
public class TrieNode : IRecycle
{
    /// <summary>
    /// 节点字符串
    /// </summary>
    public string NodeValue
    {
        get;
        private set;
    }

    /// <summary>
    /// 父节点
    /// </summary>
    public TrieNode Parent
    {
        get;
        private set;
    }

    /// <summary>
    /// 所属前缀树
    /// </summary>
    public Trie OwnerTree
    {
        get;
        private set;
    }

    /// <summary>
    /// 节点深度(根节点为0)
    /// </summary>
    public int Depth
    {
        get;
        private set;
    }

    /// <summary>
    /// 是否是单词节点
    /// </summary>
    public bool IsTail
    {
        get;
        set;
    }

    /// <summary>
    /// 是否是根节点
    /// </summary>
    public bool IsRoot
    {
        get
        {
            return Parent == null;
        }
    }

    /// <summary>
    /// 子节点映射Map<节点字符串, 节点对象>
    /// </summary>
    public Dictionary<string, TrieNode> ChildNodesMap
    {
        get;
        private set;
    }

    /// <summary>
    /// 子节点数量
    /// </summary>
    public int ChildCount
    {
        get
        {
            return ChildNodesMap.Count;
        }
    }

    public TrieNode()
    {
        ChildNodesMap = new Dictionary<string, TrieNode>();
    }

    public void OnCreate()
    {
        NodeValue = null;
        Parent = null;
        OwnerTree = null;
        Depth = 0;
        IsTail = false;
        ChildNodesMap.Clear();
    }

    /// <summary>
    /// 初始化数据
    /// </summary>
    /// <param name="value">字符串</param>
    /// <param name="parent">父节点</param>
    /// <param name="ownerTree">所属前缀树</param>
    /// <param name="depth">节点深度</param>
    /// <param name="isTail">是否是单词节点</param>
    public void Init(string value, TrieNode parent, Trie ownerTree, int depth, bool isTail = false)
    {
        NodeValue = value;
        Parent = parent;
        OwnerTree = ownerTree;
        Depth = depth;
        IsTail = isTail;
    }

    public void OnDispose()
    {
        NodeValue = null;
        Parent = null;
        OwnerTree = null;
        Depth = 0;
        IsTail = false;
        ChildNodesMap.Clear();
    }

    /// <summary>
    /// 添加子节点
    /// </summary>
    /// <param name="nodeWord"></param>
    /// <param name="isTail"></param>
    /// <returns></returns>
    public TrieNode AddChildNode(string nodeWord, bool isTail)
    {
        TrieNode node;
        if (ChildNodesMap.TryGetValue(nodeWord, out node))
        {
            Debug.Log($"节点字符串:{NodeValue}已存在字符串:{nodeWord}的子节点,不重复添加子节点!");
            return node;
        }
        node = ObjectPool.Singleton.pop<TrieNode>();
        node.Init(nodeWord, this, OwnerTree, Depth + 1, isTail);
        ChildNodesMap.Add(nodeWord, node);
        return node;
    }

    /// <summary>
    /// 移除指定子节点
    /// </summary>
    /// <param name="nodeWord"></param>
    /// <returns></returns>
    public bool RemoveChildNodeByWord(string nodeWord)
    {
        var childNode = GetChildNode(nodeWord);
        return RemoveChildNode(childNode);
    }

    /// <summary>
    /// 移除指定子节点
    /// </summary>
    /// <param name="childNode"></param>
    /// <returns></returns>
    public bool RemoveChildNode(TrieNode childNode)
    { 
        if(childNode == null)
        {
            Debug.LogError($"无法移除空节点!");
            return false;
        }
        var realChildNode = GetChildNode(childNode.NodeValue);
        if(realChildNode != childNode)
        {
            Debug.LogError($"移除的子节点单词:{childNode.NodeValue}对象不是同一个,移除子节点失败!");
            return false;
        }
        ChildNodesMap.Remove(childNode.NodeValue);
        ObjectPool.Singleton.push<TrieNode>(childNode);
        return true;
    }

    /// <summary>
    /// 当前节点从父节点移除
    /// </summary>
    /// <returns></returns>
    public bool RemoveFromParent()
    {
        if(IsRoot)
        {
            Debug.LogError($"当前节点是根节点，不允许从父节点移除，从父节点移除当前节点失败!");
            return false;
        }
        return Parent.RemoveChildNode(this);
    }

    /// <summary>
    /// 获取指定字符串的子节点
    /// </summary>
    /// <param name=""></param>
    /// <param name=""></param>
    /// <returns></returns>
    public TrieNode GetChildNode(string nodeWord)
    {
        TrieNode trieNode;
        if (!ChildNodesMap.TryGetValue(nodeWord, out trieNode))
        {
            Debug.Log($"节点字符串:{NodeValue}找不到子节点字符串:{nodeWord},获取子节点失败!");
            return null;
        }
        return trieNode;
    }

    /// <summary>
    /// 是否包含指定字符串的子节点
    /// </summary>
    /// <param name=""></param>
    /// <param name=""></param>
    /// <returns></returns>
    public bool ContainWord(string nodeWord)
    {
        return ChildNodesMap.ContainsKey(nodeWord);
    }

    /// <summary>
    /// 获取当前节点构成的单词
    /// Note:
    /// 不管当前节点是否是单词节点,都返回从当前节点回溯到根节点拼接的单词
    /// 若当前节点为根节点，则返回根节点的字符串(默认为"Root")
    /// </summary>
    /// <returns></returns>
    public string GetFullWord()
    {
        var trieNodeWord = NodeValue;
        var node = Parent;
        while(node != null && !node.IsRoot)
        {
            trieNodeWord = $"{node.NodeValue}{OwnerTree.Separator}{trieNodeWord}";
            node = node.Parent;
        }
        return trieNodeWord;
    }
}
