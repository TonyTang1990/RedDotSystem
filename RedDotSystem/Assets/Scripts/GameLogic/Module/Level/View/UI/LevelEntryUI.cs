/*
 * Description:             LevelEntryUI.cs
 * Author:                  TONYTANG
 * Create Date:             2026/04/02
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// LevelEntryUI.cs
/// 关卡入口UI
/// </summary>
public class LevelEntryUI : BaseUI
{
    /// <summary>
    /// 关闭按钮
    /// </summary>
    [Header("关闭按钮")]
    public Button BtnClose;

    /// <summary>
    /// 关卡入口父GameObject
    /// </summary>
    [Header("关卡入口父GameObject")]
    public GameObject LevelEntryParentGo;

    /// <summary>
    /// 关卡入口预制件
    /// </summary>
    [Header("关卡入口预制件")]
    public GameObject LevelEntryPrefab;

    /// <summary>
    /// PVE按钮组件
    /// </summary>
    [Header("PVE按钮组件")]
    public CommonBtnWidget PVEBtnWidget;

    /// <summary>
    /// 关卡入口实例对象列表
    /// </summary>
    private List<GameObject> mLevelEntryGoList = new List<GameObject>();

    /// <summary>
    /// 响应打开
    /// </summary>
    protected override void OnOpen()
    {
        base.OnOpen();
        InitView();
        RefreshView();
    }

    /// <summary>
    /// 添加所有监听
    /// </summary>
    protected override void AddAllListeners()
    {
        base.AddAllListeners();
        BtnClose.onClick.AddListener(OnBtnCloseClick);
    }

    /// <summary>
    /// 移除所有监听
    /// </summary>
    protected override void RemoveAllListeners()
    {
        base.RemoveAllListeners();
        BtnClose.onClick.RemoveListener(OnBtnCloseClick);
    }

    /// <summary>
    /// 初始化显示
    /// </summary>
    private void InitView()
    {
        PVEBtnWidget.Init("PVE战斗", OnPVEBtnClick, RedDotNames.LEVEL_PVE_ENTRY);
    }

    /// <summary>
    /// 刷新显示
    /// </summary>
    private void RefreshView()
    {
        RefreshLevelEntryView();
    }

    /// <summary>
    /// 刷新关卡入口显示
    /// </summary>
    private void RefreshLevelEntryView()
    {
        if(mLevelEntryGoList.Count == 0)
        {
            LevelEntryPrefab.SetActive(true);
            for(int levelIndex = 0, length = LevelModel.MaxLevelId; levelIndex < length; levelIndex++)
            {
                var levelId = levelIndex + 1;
                var levelEntryGo = Instantiate(LevelEntryPrefab, LevelEntryParentGo.transform);
                var levelEntryWidget = levelEntryGo.GetComponent<LevelEntryWidget>();
                if (levelEntryWidget != null)
                {
                    levelEntryWidget.Init(levelId);
                }
                mLevelEntryGoList.Add(levelEntryGo);
            }
            LevelEntryPrefab.SetActive(false);
        }
        else
        {
            for(int index = 0, length = mLevelEntryGoList.Count; index < length; index++)
            {
                var levelId = index + 1;
                var levelEntryWidget = mLevelEntryGoList[index].GetComponent<LevelEntryWidget>();
                levelEntryWidget.Init(levelId);
            }
        }
    }

    /// <summary>
    /// 响应PVE按钮点击
    /// </summary>
    private void OnPVEBtnClick()
    {
        Debug.Log("点击了PVE按钮");
        PVEAgent.OpenPVEUI();
    }

    /// <summary>
    /// 响应关闭按钮点击
    /// </summary>
    private void OnBtnCloseClick()
    {
        Close();
    }
}