/*
 * Description:             TrieEditorWindow.cs
 * Author:                  TONYTANG
 * Create Date:             2022/08/12
 */

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

/// <summary>
/// TrieEditorWindow.cs
/// 前缀树窗口
/// </summary>
public class TrieEditorWindow : EditorWindow
{
    /// <summary>
    /// 居中Button GUI Style
    /// </summary>
    private GUIStyle mButtonMidStyle;

    /// <summary>
    /// 前缀树
    /// </summary>
    private Trie mTrie;

    /// <summary>
    /// 当前滚动位置
    /// </summary>
    private Vector2 mCurrentScrollPos;

    /// <summary>
    /// 输入单词
    /// </summary>
    private string mInputWord;

    /// <summary>
    /// 节点展开Map<节点单词全名, 是否展开> 
    /// </summary>
    private Dictionary<string, bool> mTrieNodeUnfoldMap = new Dictionary<string, bool>();

    /// <summary>
    /// 前缀树单词列表
    /// </summary>
    private List<string> mTrieWordList;

    [MenuItem("Tools/前缀树测试窗口")]
    static void Init()
    {
        TrieEditorWindow window = (TrieEditorWindow)EditorWindow.GetWindow(typeof(TrieEditorWindow), false, "前缀树测试窗口");
        window.Show();
    }

    void OnGUI()
    {
        InitGUIStyle();
        InitData();
        mCurrentScrollPos = EditorGUILayout.BeginScrollView(mCurrentScrollPos);
        EditorGUILayout.BeginVertical();
        DisplayTrieOperationArea();
        DisplayTrieContentArea();
        DisplayTrieWordsArea();
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndScrollView();
    }

    /// <summary>
    /// 初始化GUIStyle
    /// </summary>
    private void InitGUIStyle()
    {
        if(mButtonMidStyle == null)
        {
            mButtonMidStyle = new GUIStyle("ButtonMid");
        }
    }

    /// <summary>
    /// 初始化数据
    /// </summary>
    private void InitData()
    {
        if (mTrie == null)
        {
            mTrie = new Trie();
            mTrieWordList = null;
        }
    }

    /// <summary>
    /// 更新前缀树单词列表
    /// </summary>
    private void UpdateTrieWordList()
    {
        mTrieWordList = mTrie.GetWordList();
    }

    /// <summary>
    /// 显示前缀树操作区域
    /// </summary>
    private void DisplayTrieOperationArea()
    {
        EditorGUILayout.BeginHorizontal("box");
        EditorGUILayout.LabelField("单词:", GUILayout.Width(40f), GUILayout.Height(20f));
        mInputWord = EditorGUILayout.TextField(mInputWord, GUILayout.ExpandWidth(true), GUILayout.Height(20f));
        if(GUILayout.Button("添加", GUILayout.Width(120f), GUILayout.Height(20f)))
        {
            if (string.IsNullOrEmpty(mInputWord))
            {
                Debug.LogError($"不能允许添加空单词!");
            }
            else
            {
                mTrie.AddWord(mInputWord);
                UpdateTrieWordList();
            }
        }
        if (GUILayout.Button("删除", GUILayout.Width(120f), GUILayout.Height(20f)))
        {
            if(string.IsNullOrEmpty(mInputWord))
            {
                Debug.LogError($"不能允许删除空单词!");
            }
            else
            {
                mTrie.RemoveWord(mInputWord);
            }
        }
        EditorGUILayout.EndHorizontal();
    }

    /// <summary>
    /// 绘制前缀树内容
    /// </summary>
    private void DisplayTrieContentArea()
    {
        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.LabelField("前缀树节点信息", mButtonMidStyle, GUILayout.ExpandWidth(true), GUILayout.Height(20f));
        DisplayTrieNode(mTrie.RootNode);
        EditorGUILayout.EndVertical();
    }

    /// <summary>
    /// 显示一个节点
    /// </summary>
    /// <param name="trieNode"></param>
    private void DisplayTrieNode(TrieNode trieNode)
    {
        var nodeFullWord = trieNode.GetFullWord();
        if(!mTrieNodeUnfoldMap.ContainsKey(nodeFullWord))
        {
            mTrieNodeUnfoldMap.Add(nodeFullWord, true);
        }
        EditorGUILayout.BeginHorizontal("box");
        GUILayout.Space(trieNode.Depth * 20);
        var displayName = $"{trieNode.NodeValue}({trieNode.Depth})";
        if (trieNode.ChildCount > 0)
        {
            mTrieNodeUnfoldMap[nodeFullWord] = EditorGUILayout.Foldout(mTrieNodeUnfoldMap[nodeFullWord], displayName);
        }
        else
        {
            EditorGUILayout.LabelField(displayName);
        }
        EditorGUILayout.EndHorizontal();
        if(mTrieNodeUnfoldMap[nodeFullWord] && trieNode.ChildCount > 0)
        {
            var childNodeValueList = trieNode.ChildNodesMap.Keys.ToList();
            foreach(var childNodeValue in childNodeValueList)
            {
                var childNode = trieNode.GetChildNode(childNodeValue);
                DisplayTrieNode(childNode);
            }
        }
    }

    /// <summary>
    /// 显示前缀树单词区域
    /// </summary>
    private void DisplayTrieWordsArea()
    {
        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.LabelField("前缀树单词信息", mButtonMidStyle, GUILayout.ExpandWidth(true), GUILayout.Height(20f));
        if(mTrieWordList != null)
        {
            foreach (var word in mTrieWordList)
            {
                EditorGUILayout.LabelField(word, GUILayout.ExpandWidth(true), GUILayout.Height(20f));
            }
        }
        EditorGUILayout.EndVertical();
    }
}