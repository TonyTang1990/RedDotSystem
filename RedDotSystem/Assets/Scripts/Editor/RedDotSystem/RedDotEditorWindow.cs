/*
 * Description:             RedDotEditorWindow.cs
 * Author:                  TONYTANG
 * Create Date:             2022/08/14
 */

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

/// <summary>
/// RedDotEditorWindow.cs
/// 红点系统可视化窗口
/// </summary>
public class RedDotEditorWindow : EditorWindow
{
    /// <summary>
    /// 红点系统页签类型
    /// </summary>
    public enum RedDotSystemTag
    {
        RED_DOT_TREE = 0,                   // 红点数页签类型
        RED_DOT_UNITS,                      // 红点运算单元页签类型
        RED_DOT_DETAIL,                     // 红点详情页签类型
    }

    /// <summary>
    /// 红点系统页签名字
    /// </summary>
    private string[] mRedDotTagNames = new string[3] { "红点树", "红点运算单元", "红点名详情" };

    /// <summary>
    /// 当前页签选择索引
    /// </summary>
    private int mSelectedTagIndex = 0;

    /// <summary>
    /// 当前选择节点
    /// </summary>
    private TrieNode mSelectedTrieNode;

    /// <summary>
    /// 当前滚动位置
    /// </summary>
    private Vector2 mCurrentScrollPos;

    /// <summary>
    /// 红点名展开信息Map<红点名, 是否展开>
    /// </summary>
    private Dictionary<string, bool> mRedDotNameUnfoldMap = new Dictionary<string, bool>();

    /// <summary>
    /// 红点名列表(缓存红点名列表避免内存开销)
    /// </summary>
    private List<string> mRedDotNameList;

    /// <summary>
    /// 红点运算单元搜索栏
    /// </summary>
    private SearchField mRedDotUnitSearchField;

    /// <summary>
    /// 红点运算单元搜索文本
    /// </summary>
    private string mRedDotUnitSearchText;

    /// <summary>
    /// 打开红点系统可视化窗口
    /// </summary>
    [MenuItem("ToolsWindow/红点系统可视化窗口")]
    public static void Open()
    {
        RedDotEditorWindow redDotEditorWindow = (RedDotEditorWindow)EditorWindow.GetWindow<RedDotEditorWindow>(false, "红点系统可视化窗口");
        redDotEditorWindow.Show();
    }

    /// <summary>
    /// 绘制
    /// </summary>
    private void OnGUI()
    {
        if(EditorApplication.isPlaying)
        {
            if(RedDotModel.Singleton.IsInitCompelte)
            {
                if(mRedDotUnitSearchField == null)
                {
                    mRedDotUnitSearchField = new SearchField();
                }
                DrawRedDotTagArea();
                mCurrentScrollPos = EditorGUILayout.BeginScrollView(mCurrentScrollPos);
                DrawRedDotContentArea();
                EditorGUILayout.EndScrollView();
            }
            else
            {
                EditorGUILayout.LabelField($"等待红点系统数据初始化完成!");
            }
        }
        else
        {
            EditorGUILayout.LabelField($"只支持运行时模式查看!");
        }
    }

    /// <summary>
    /// 绘制红点系统页签区域
    /// </summary>
    private void DrawRedDotTagArea()
    {
        mSelectedTagIndex = GUILayout.SelectionGrid(mSelectedTagIndex, mRedDotTagNames, mRedDotTagNames.Length);
    }

    /// <summary>
    /// 绘制红点区域内容区域
    /// </summary>
    private void DrawRedDotContentArea()
    {
        if (mSelectedTagIndex == (int)RedDotSystemTag.RED_DOT_TREE)
        {
            DrawRedDotTreeArea();
        }
        else if(mSelectedTagIndex == (int)RedDotSystemTag.RED_DOT_UNITS)
        {
            DrawRedDotUnitsArea();
        }
        else if(mSelectedTagIndex == (int)RedDotSystemTag.RED_DOT_DETAIL)
        {
            DrawRedDotDetailArea();
        }
    }

    /// <summary>
    /// 绘制红点树区域
    /// </summary>
    private void DrawRedDotTreeArea()
    {
        var preColor = GUI.color;
        GUI.color = Color.green;
        if (GUILayout.Button("全部折叠", GUILayout.ExpandWidth(true)))
        {
            FoldAllTrieNode(false);
        }
        if (GUILayout.Button("全部展开", GUILayout.ExpandWidth(true)))
        {
            FoldAllTrieNode(true);
        }
        GUI.color = preColor;
        var redDotTrie = RedDotModel.Singleton.RedDotTrie;
        DrawTrieNode(redDotTrie.RootNode);
    }

    /// <summary>
    /// 折叠或展开所有前缀树节点
    /// </summary>
    /// <param name="isUnfold">是否展开</param>
    private void FoldAllTrieNode(bool isUnfold)
    {
        mRedDotNameList = mRedDotNameUnfoldMap.Keys.ToList();
        foreach(var redDotName in mRedDotNameList)
        {
            mRedDotNameUnfoldMap[redDotName] = isUnfold;
        }
    }

    /// <summary>
    /// 绘制一个前缀树节点
    /// </summary>
    /// <param name="trieNode"></param>
    private void DrawTrieNode(TrieNode trieNode)
    {
        var redDotName = trieNode.GetFullWord();
        if(!mRedDotNameUnfoldMap.ContainsKey(redDotName))
        {
            mRedDotNameUnfoldMap.Add(redDotName, true);
        }
        EditorGUILayout.BeginHorizontal("box");
        GUILayout.Space(trieNode.Depth * 20);
        var redDotDisplayName = $"{trieNode.NodeValue}";
        var isRoot = trieNode.IsRoot;
        var isTail = trieNode.IsTail;
        var redDotNodeResult = 0;
        if(isTail)
        {
            var redDotInfo = RedDotModel.Singleton.GetRedDotInfoByName(redDotName);
            redDotNodeResult = RedDotManager.Singleton.GetRedDotResult(redDotName);
            redDotDisplayName = $"{redDotDisplayName}({redDotInfo.RedDotDes})({redDotNodeResult})";
        }
        var preColor = GUI.color;
        if (redDotNodeResult > 0)
        {
            GUI.color = Color.yellow;
        }
        if (trieNode.ChildCount > 0)
        {
            mRedDotNameUnfoldMap[redDotName] = EditorGUILayout.Foldout(mRedDotNameUnfoldMap[redDotName], redDotDisplayName);
            GUI.color = preColor;
        }
        else
        {
            EditorGUILayout.LabelField(redDotDisplayName);
            GUI.color = preColor;
        }
        if (!isRoot && isTail)
        {
            DrawTrieNodeJumpUI(trieNode);
        }
        EditorGUILayout.EndHorizontal();
        if (mRedDotNameUnfoldMap[redDotName])
        {
            foreach (var childNode in trieNode.ChildNodesMap)
            {
                DrawTrieNode(childNode.Value);
            }
        }
    }

    /// <summary>
    /// 绘制前缀树节点跳转UI
    /// </summary>
    /// <param name="trieNode"></param>
    private void DrawTrieNodeJumpUI(TrieNode trieNode)
    {
        var preColor = GUI.color;
        GUI.color = Color.green;
        if(GUILayout.Button("红点名详情", GUILayout.Width(120f)))
        {
            JumpToTrieNodeDetail(trieNode);
        }
        GUI.color = preColor;
    }

    /// <summary>
    /// 绘制红点名运算单元区域
    /// </summary>
    private void DrawRedDotUnitsArea()
    {
        DrawRedDotUnitSearchArea();
        DrawRedDotUnitTile();
        var redDotUnitInfoMap = RedDotModel.Singleton.GetRedDotUnitInfoMap();
        foreach(var redDotUnitInfo in redDotUnitInfoMap)
        {
            if(string.IsNullOrEmpty(mRedDotUnitSearchText) || redDotUnitInfo.Key.ToString().StartsWith(mRedDotUnitSearchText, System.StringComparison.OrdinalIgnoreCase))
            {
                DrawRedDotUnitInfo(redDotUnitInfo.Value);
            }
        }
    }

    /// <summary>
    /// 绘制红点运算单元搜索区域
    /// </summary>
    private void DrawRedDotUnitSearchArea()
    {
        mRedDotUnitSearchText = mRedDotUnitSearchField.OnGUI(mRedDotUnitSearchText, GUILayout.ExpandWidth(true));
    }

    /// <summary>
    /// 绘制红点运算单元标题
    /// </summary>
    private void DrawRedDotUnitTile()
    {
        EditorGUILayout.BeginHorizontal("box");
        EditorGUILayout.LabelField("红点运算单元类型", RedDotStyles.ButtonMidStyle, GUILayout.Width(200f), GUILayout.Height(20f));
        EditorGUILayout.LabelField("红点运算单元描述", RedDotStyles.ButtonMidStyle, GUILayout.Width(200f), GUILayout.Height(20f));
        EditorGUILayout.LabelField("红点类型", RedDotStyles.ButtonMidStyle, GUILayout.Width(120f), GUILayout.Height(20f));
        EditorGUILayout.LabelField("红点运算单元值", RedDotStyles.ButtonMidStyle, GUILayout.Width(120f), GUILayout.Height(20f));
        EditorGUILayout.LabelField("影响红点名", RedDotStyles.ButtonMidStyle, GUILayout.ExpandWidth(true), GUILayout.Height(20f));
        EditorGUILayout.EndHorizontal();
    }

    /// <summary>
    /// 绘制红点运算单元信息
    /// </summary>
    /// <param name="redDotUnitInfo"></param>
    private void DrawRedDotUnitInfo(RedDotUnitInfo redDotUnitInfo)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(redDotUnitInfo.RedDotUnit.ToString(), RedDotStyles.ButtonMidStyle, GUILayout.Width(200f), GUILayout.Height(20f));
        EditorGUILayout.LabelField(redDotUnitInfo.RedDotUnitDes, RedDotStyles.ButtonMidStyle, GUILayout.Width(200f), GUILayout.Height(20f));
        EditorGUILayout.LabelField(redDotUnitInfo.RedDotType.ToString(), RedDotStyles.ButtonMidStyle, GUILayout.Width(120f), GUILayout.Height(20f));
        var redDotUnitResult = RedDotModel.Singleton.GetRedDotUnitResult(redDotUnitInfo.RedDotUnit);
        var preColor = GUI.color;
        if(redDotUnitResult > 0)
        {
            GUI.color = Color.yellow;
        }
        EditorGUILayout.LabelField(redDotUnitResult.ToString(), RedDotStyles.ButtonMidStyle, GUILayout.Width(120f), GUILayout.Height(20f));
        GUI.color = preColor;
        var redDotNames = RedDotModel.Singleton.GetRedDotNamesByUnit(redDotUnitInfo.RedDotUnit);
        foreach(var redDotName in redDotNames)
        {
            preColor = GUI.color;
            GUI.color = Color.green;
            if(GUILayout.Button(redDotName, GUILayout.ExpandWidth(true), GUILayout.Height(20f)))
            {
                var trieNode = RedDotModel.Singleton.RedDotTrie.GetWordNode(redDotName);
                JumpToTrieNodeDetail(trieNode);
            }
            GUI.color = preColor;
        }
        EditorGUILayout.EndHorizontal();
    }

    /// <summary>
    /// 绘制红点名详情区域
    /// </summary>
    private void DrawRedDotDetailArea()
    {
        if(mSelectedTrieNode != null)
        {
            var redDotName = mSelectedTrieNode.GetFullWord();
            if (mSelectedTrieNode.IsTail)
            {
                var redDotInfo = RedDotModel.Singleton.GetRedDotInfoByName(redDotName);
                DrawRedDotInfo(redDotInfo);
            }
            else
            {
                EditorGUILayout.LabelField($"选中红点名:{redDotName}不是单词节点!", RedDotStyles.ButtonMidStyle);
            }
        }
        else
        {
            EditorGUILayout.LabelField($"未选中有效红点名!", RedDotStyles.ButtonMidStyle);
        }
    }

    /// <summary>
    /// 绘制红点信息
    /// </summary>
    /// <param name="redDotInfo"></param>
    private void DrawRedDotInfo(RedDotInfo redDotInfo)
    {
        EditorGUILayout.LabelField($"当前选择红点", RedDotStyles.ButtonMidStyle, GUILayout.ExpandWidth(true), GUILayout.Height(20f));
        var redDotNameResult = RedDotManager.Singleton.GetRedDotResult(redDotInfo.RedDotName);
        EditorGUILayout.LabelField($"{redDotInfo.RedDotName}({redDotInfo.RedDotDes})({redDotNameResult})", RedDotStyles.ButtonMidStyle, GUILayout.ExpandWidth(true), GUILayout.Height(20f));
        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.LabelField($"红点运算单元组合", RedDotStyles.ButtonMidStyle, GUILayout.ExpandWidth(true), GUILayout.Height(20f));
        DrawRedDotUnitTile();
        var redDotUnitList = RedDotModel.Singleton.GetRedDotUnitsByName(redDotInfo.RedDotName);
        foreach(var redDotUnit in redDotUnitList)
        {
            var redDotUnitInfo = RedDotModel.Singleton.GetRedDotUnitInfo(redDotUnit);
            DrawRedDotUnitInfo(redDotUnitInfo);
        }
        EditorGUILayout.EndVertical();
    }

    /// <summary>
    /// 跳转指定树节点红点名详情
    /// </summary>
    /// <param name="trieNode"></param>
    private void JumpToTrieNodeDetail(TrieNode trieNode)
    {
        mSelectedTagIndex = (int)RedDotSystemTag.RED_DOT_DETAIL;
        mSelectedTrieNode = trieNode;
    }
}