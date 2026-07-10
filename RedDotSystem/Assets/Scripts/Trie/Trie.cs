/*
 * Description:             Trie.cs
 * Author:                  TONYTANG
 * Create Date:             2022/08/12
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// 前缀树
/// </summary>
public class Trie
{
    /// <summary>
    /// 单词分隔符
    /// </summary>
    public string Separator
    {
        get;
        private set;
    }

    /// <summary>
    /// 单词数量
    /// </summary>
    public int WorldCount
    {
        get;
        private set;
    }

    /// <summary>
    /// 树深度
    /// </summary>
    public int TrieDeepth
    {
        get;
        private set;
    }

    /// <summary>
    /// 根节点
    /// </summary>
    public TrieNode RootNode
    {
        get;
        private set;
    }

    /// <summary>
    /// 所有节点Map(Key为节点名，Value为节点信息)
    /// Note:
    /// 方便使用父子关系时快速查询节点名
    /// </summary>
    private Dictionary<string, TrieNode> mAllNodeMap;

    /// <summary>
    /// 单词列表(用于缓存分割结果，优化单个单词判定时重复分割问题)
    /// </summary>
    private List<string> mWordList;

    /// <summary>
    /// 临时字符串构建器
    /// </summary>
    private StringBuilder mTempStringBuilder;

    public Trie()
    {
        Separator = "|";
        WorldCount = 0;
        TrieDeepth = 0;
        RootNode = ObjectPool.Singleton.pop<TrieNode>();
        RootNode.Init("Root", null, this, 0, false);
        mAllNodeMap = new Dictionary<string, TrieNode>();
        mWordList = new List<string>();
        mTempStringBuilder = new StringBuilder();
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="separator"></param>
    public Trie(string separator = "|")
    {
        Separator = separator;
        WorldCount = 0;
        TrieDeepth = 0;
        RootNode = ObjectPool.Singleton.pop<TrieNode>();
        RootNode.Init("Root", null, this, 0, false);
        mAllNodeMap = new Dictionary<string, TrieNode>();
        mWordList = new List<string>();
        mTempStringBuilder = new StringBuilder();
    }

    /// <summary>
    /// 释放
    /// </summary>
    public void Dispose()
    {
        RemoveAllTrieNode();
        mWordList.Clear();
        mTempStringBuilder.Clear();
    }

    /// <summary>
    /// 添加单词
    /// </summary>
    /// <param name="word"></param>
    public void AddWord(string word)
    {
        mWordList.Clear();
        var words = word.Split(Separator);
        mWordList.AddRange(words);
        var length = mWordList.Count;
        var node = RootNode;
        for (int i = 0; i < length; i++)
        {
            var spliteWord = mWordList[i];
            var isLast = i == (length - 1);
            if (!node.ContainWord(spliteWord))
            {
                node = node.AddChildNode(spliteWord, isLast);
                AddTrieNode(node);
            }
            else
            {
                node = node.GetChildNode(spliteWord);
                if(!node.IsTail && isLast)
                {
                    node.IsTail = isLast;
                }
                //if(isLast)
                //{
                //    Debug.Log($"添加重复单词:{word}");
                //}
            }
        }
    }

    /// <summary>
    /// 移除指定单词
    /// Note:
    /// 仅当指定单词存在时才能移除成功
    /// </summary>
    /// <param name="word"></param>
    /// <returns></returns>
    public bool RemoveWord(string word)
    {
        if(string.IsNullOrEmpty(word))
        {
            Debug.LogError($"不允许移除空单词!");
            return false;
        }
        var wordNode = GetWordNode(word);
        if(wordNode == null)
        {
            Debug.LogError($"找不到单词:{word}的节点信息，移除单词失败!");
            return false;
        }
        if(wordNode.IsRoot)
        {
            Debug.LogError($"不允许删除根节点!");
            return false;
        }
        // 从最里层节点开始反向判定更新和删除
        if(!wordNode.IsTail)
        {
            Debug.LogError($"单词:{word}的节点不是单词节点，移除单词失败!");
            return false;
        }
        // 删除的节点是叶子节点时要删除节点并往上递归更新节点数据
        // 反之只更新标记为非单词节点即可结束
        if(wordNode.ChildCount > 0)
        {
            wordNode.IsTail = false;
            return true;
        }
        // 提前存储父节点信息，避免节点移除后拿不到数据
        // 往上遍历更新节点信息
        var node = wordNode.Parent;
        RemoveTrieNode(wordNode);
        while(node != null && !node.IsRoot)
        {
            // 没有子节点且不是单词节点则直接删除
            if(node.ChildCount == 0 && !node.IsTail)
            {
                RemoveTrieNode(node);
            }
            node = node.Parent;
            // 有子节点则停止往上更新
            if(node != null && node.ChildCount > 0)
            {
                break;
            }
        }
        return true;
    }

    /// <summary>
    /// 添加指定节点
    /// </summary>
    /// <param name="word"></param>
    /// <param name="node"></param>
    /// <returns></returns>
    private bool AddTrieNode(TrieNode node)
    {
        if(node == null)
        {
            Debug.LogError($"不允许添加空节点!");
            return false;
        }
        var word = node.NodeValue;
        if(mAllNodeMap.ContainsKey(word))
        {
            Debug.LogError($"节点:{word}已存在，不允许重复添加，请检查代码!");
            return false;
        }
        mAllNodeMap.Add(word, node);
        return true;
    }

    /// <summary>
    /// 移除指定节点
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    private bool RemoveTrieNode(TrieNode node)
    {
        if (node == null)
        {
            Debug.LogError($"不允许移除空节点!");
            return false;
        }
        var word = node.NodeValue;
        if(!mAllNodeMap.TryGetValue(word, out var trieNode))
        {
            Debug.LogError($"节点:{word}不存在，无法移除，请检查代码!");
            return false;
        }
        if(trieNode != node)
        {
            Debug.LogError($"节点:{word}对象不一致，无法移除，请检查代码!");
            return false;
        }
        node.RemoveFromParent();
        mAllNodeMap.Remove(word);
        ObjectPool.Singleton.push<TrieNode>(node);
        return true;
    }

    /// <summary>
    /// 移除所有节点数据
    /// </summary>
    public void RemoveAllTrieNode()
    {
        // 必须从里层往顶层清理
        var nodes = mAllNodeMap.Values.ToList();
        nodes.Sort(SortTrieNodes);
        foreach(var node in nodes)
        {
            RemoveTrieNode(node);
        }
    }

    /// <summary>
    /// 排序节点(深度大的在前面，方便从里层往顶层清理节点数据)
    /// </summary>
    /// <param name="node1"></param>
    /// <param name="node2"></param>
    /// <returns></returns>
    private int SortTrieNodes(TrieNode node1, TrieNode node2)
    {
        if(node1.Depth != node2.Depth)
        {
            return node2.Depth.CompareTo(node1.Depth);
        }
        return node2.NodeValue.CompareTo(node1.NodeValue);
    }

    /// <summary>
    /// 获取指定字符串的单词节点
    /// Note:
    /// 只有满足每一层且最后一层是单词的节点才算有效单词节点
    /// 时间复杂度O(1)
    /// </summary>
    /// <param name="word"></param>
    /// <returns></returns>
    public TrieNode GetWordNode(string word)
    {
        if(string.IsNullOrEmpty(word))
        {
            Debug.LogError($"不允许获取空单词的单词节点!");
            return null;
        }
        var words = word.Split(Separator);
        if(words == null || words.Length == 0)
        {
            Debug.LogError($"单词:{word}分割后没有有效单词，获取单词节点失败!");
            return null;
        }
        mWordList.Clear();
        mWordList.AddRange(words);
        var length = mWordList.Count;
        var node = RootNode;
        for (int i = 0; i < length; i++)
        {
            var spliteWord = mWordList[i];
            node = node.GetChildNode(spliteWord);
            if(node == null)
            {
                Debug.LogError($"找不到单词:{word}的节点信息，获取单词节点失败!");
                return null;
            }
        }
        if(node == null || !node.IsTail)
        {
            Debug.Log($"找不到单词:{word}的单词节点!");
            return null;
        }
        return node;
    }

    /// <summary>
    /// 有按指定单词开头的词语
    /// </summary>
    /// <param name="word"></param>
    /// <returns></returns>
    public bool StartWith(string word)
    {
        if (string.IsNullOrEmpty(word))
        {
            return false;
        }
        mWordList.Clear();
        var wordArray = word.Split(Separator);
        mWordList.AddRange(wordArray);
        return FindWord(RootNode, mWordList);
    }

    /// <summary>
    /// 查找单词
    /// </summary>
    /// <param name="trieNode"></param>
    /// <param name="wordList"></param>
    /// <returns></returns>
    private bool FindWord(TrieNode trieNode, List<string> wordList)
    {
        if (wordList.Count == 0)
        {
            return true;
        }
        var firstWord = wordList[0];
        if (!trieNode.ContainWord(firstWord))
        {
            return false;
        }
        var childNode = trieNode.GetChildNode(firstWord);
        wordList.RemoveAt(0);
        return FindWord(childNode, wordList);
    }

    /// <summary>
    /// 单词是否存在
    /// </summary>
    /// <param name="word"></param>
    /// <returns></returns>
    public bool ContainWord(string word)
    {
        if(string.IsNullOrEmpty(word))
        {
            return false;
        }
        mWordList.Clear();
        var wordArray = word.Split(Separator);
        mWordList.AddRange(wordArray);
        return MatchWord(RootNode, mWordList);
    }

    /// <summary>
    /// 匹配单词(单词必须完美匹配)
    /// </summary>
    /// <param name="trieNode"></param>
    /// <param name="wordList"></param>
    /// <returns></returns>
    private bool MatchWord(TrieNode trieNode, List<string> wordList)
    {
        if (wordList.Count == 0)
        {
            return trieNode.IsTail;
        }
        var firstWord = wordList[0];
        if (!trieNode.ContainWord(firstWord))
        {
            return false;
        }
        var childNode = trieNode.GetChildNode(firstWord);
        wordList.RemoveAt(0);
        return MatchWord(childNode, wordList);
    }

    /// <summary>
    /// 获取指定单词的所有父单词列表(不含自身)
    /// </summary>
    /// <param name="word"></param>
    /// <param name="wordList"></param>
    public void GetParentWordList(string word, ref List<string> wordList)
    {
        var trieNode = GetWordNode(word);
        if(trieNode == null || !trieNode.IsTail)
        {
            return;
        }
        wordList.Clear();
        var parentNode = trieNode.Parent;
        while(parentNode != null && !parentNode.IsRoot)
        {
            if(parentNode.IsTail)
            {
                var parentNodeWord = GetNodeWord(parentNode);
                wordList.Add(parentNodeWord);
            }
            parentNode = parentNode.Parent;
        }
    }

    /// <summary>
    /// 获取指定节点的单词
    /// Note:
    /// 如果节点不是单词则返回空字符串
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    private string GetNodeWord(TrieNode node)
    {
        if (node == null || !node.IsTail)
        {
            return string.Empty;
        }
        mTempStringBuilder.Clear();
        mTempStringBuilder.Append(node.NodeValue);
        var parentNode = node.Parent;
        while(parentNode != null && !parentNode.IsRoot)
        {
            mTempStringBuilder.Insert(0, $"{parentNode.NodeValue}{Separator}");
            parentNode = parentNode.Parent;
        }
        return mTempStringBuilder.ToString();
    }

    /// <summary>
    /// 获取所有单词列表
    /// </summary>
    /// <returns></returns>
    public List<string> GetWordList()
    {
        return GetNodeWorldList(RootNode, string.Empty);
    }

    /// <summary>
    /// 获取节点单词列表
    /// </summary>
    /// <param name="trieNode"></param>
    /// <param name="preFix"></param>
    /// <returns></returns>
    private List<string> GetNodeWorldList(TrieNode trieNode, string preFix)
    {
        var wordList = new List<string>();
        foreach (var childNodeKey in trieNode.ChildNodesMap.Keys)
        {
            var childNode = trieNode.ChildNodesMap[childNodeKey];
            string word;
            if (trieNode.IsRoot)
            {
                word = $"{preFix}{childNodeKey}";
            }
            else
            {
                word = $"{preFix}{Separator}{childNodeKey}";
            }
            if (childNode.IsTail)
            {
                wordList.Add(word);
            }
            if (childNode.ChildNodesMap.Count > 0)
            {
                var childNodeWorldList = GetNodeWorldList(childNode, word);
                wordList.AddRange(childNodeWorldList);
            }
        }
        return wordList;
    }

    /// <summary>
    /// 打印树形节点
    /// </summary>
    public void PrintTreeNodes()
    {
        PrintNodes(RootNode, 1);
    }

    /// <summary>
    /// 打印节点
    /// </summary>
    /// <param name="node"></param>
    /// <param name="depth"></param>
    private void PrintNodes(TrieNode node, int depth = 1)
    {
        var count = 1;
        foreach (var childeNode in node.ChildNodesMap)
        {
            Console.Write($"{childeNode.Key}({depth}-{count})");
            count++;
        }
        Console.WriteLine();
        foreach (var childeNode in node.ChildNodesMap)
        {
            PrintNodes(childeNode.Value, depth + 1); 
        }
    }
}