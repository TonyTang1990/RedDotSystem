/*
 * Description:             ActivityEntryWidget.cs
 * Author:                  TONYTANG
 * Create Date:             2026/05/27
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ActivityEntryWidget.cs
/// 活动入口组件
/// </summary>
public class ActivityEntryWidget : MonoBehaviour
{
    /// <summary>
    /// 按钮
    /// </summary>
    [Header("按钮")]
    public Button Btn;

    /// <summary>
    /// 文本
    /// </summary>
    [Header("文本")]
    public Text Txt;

    /// <summary>
    /// 红点组件
    /// </summary>
    [Header("红点组件")]
    public RedDotWidget RDWidget;

    /// <summary>
    /// 活动配置Id
    /// </summary>
    [SerializeField, Header("活动配置Id")]
    private int mActivityID;

    private void Awake()
    {
        AddAllListeners();
    }

    /// <summary>
    /// 添加所有监听
    /// </summary>
    private void AddAllListeners()
    {
        Btn.onClick.AddListener(OnBtnClick);
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="activityID">活动ID</param>
    public void Init(int activityID)
    {
        mActivityID = activityID;
        BindRedDotName();
        RefreshView();
    }

    /// <summary>
    /// 添加所有红点绑定
    /// </summary>
    private void BindRedDotName()
    {
        var actEntryRedDotName = ActivityAgent.GetActEntryRedDotName(mActivityID);
        RDWidget.Init(actEntryRedDotName);
    }

    /// <summary>
    /// 刷新显示
    /// </summary>
    private void RefreshView()
    {
        Txt.text = $"活动{mActivityID}入口";
    }

    /// <summary>
    /// 响应按钮点击
    /// </summary>
    private void OnBtnClick()
    {
        GameLauncher.Singleton.ActivityUI.Open(mActivityID);
    }
}