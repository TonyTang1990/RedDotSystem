/*
 * Description:             LevelEntryWidget.cs
 * Author:                  TONYTANG
 * Create Date:             2026/04/02
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// LevelEntryWidget.cs
/// 关卡入口Widget
/// </summary>
public class LevelEntryWidget : MonoBehaviour
{
    /// <summary>
    /// 关卡文本
    /// </summary>
    [Header("关卡文本")]
    public Text LevelTxt;

    /// <summary>
    /// 关卡按钮
    /// </summary>
    [Header("关卡按钮")]
    public Button LevelBtn;

    /// <summary>
    /// 关卡红点组件
    /// </summary>
    [Header("关卡红点组件")]
    public RedDotWidget LevelRedDotWidget;

    /// <summary>
    /// 关卡Id
    /// </summary>
    private int mLevelId;

    /// <summary>
    /// 关卡入口红点名
    /// </summary>
    private string mLevelEntryRedDotName;

    private void Awake()
    {
        AddAllListeners();
    }

    /// <summary>
    /// 添加所有监听
    /// </summary>
    private void AddAllListeners()
    {
        LevelBtn.onClick.AddListener(OnLevelBtnClick);
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="levelId"></param>
    public void Init(int levelId)
    {
        mLevelId = levelId;
        mLevelEntryRedDotName = RedDotUtilities.GetTemplateRDName(RedDotNames.LEVEL_ENTRY, levelId);
        UnbindAllRedDotNames();
        BindAllRedDotNames();
        RefreshView();
    }

    /// <summary>
    /// 取消所有红点绑定
    /// </summary>
    private void UnbindAllRedDotNames()
    {
        LevelRedDotWidget.UnbindRedDotName();
    }

    /// <summary>
    /// 添加所有红点绑定
    /// </summary>
    private void BindAllRedDotNames()
    {
        LevelRedDotWidget.Init(mLevelEntryRedDotName);
    }

    /// <summary>
    /// 刷新显示
    /// </summary>
    private void RefreshView()
    {
        LevelTxt.text = $"关卡{mLevelId}";
    }

    /// <summary>
    /// 相应关卡入口点击
    /// </summary>
    private void OnLevelBtnClick()
    {
        Debug.Log($"点击了关卡{mLevelId}入口!");
        LevelAgent.OpenLevelUI();
    }
}