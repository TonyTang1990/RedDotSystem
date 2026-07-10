/*
 * Description:             PVEUI.cs
 * Author:                  TONYTANG
 * Create Date:             2026/04/02
 */

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// PVEUI.cs
/// PVE窗口
/// </summary>
public class PVEUI : BaseUI
{
    /// <summary>
    /// PVE文本
    /// </summary>
    [Header("PVE文本")]
    public Text PVETxt;

    /// <summary>
    /// PVE可领奖数红点组件
    /// </summary>
    [Header("PVE可领奖数红点组件")]
    public RedDotWidget PVERewardRedDotWidget;

    /// <summary>
    /// 关闭按钮
    /// </summary>
    [Header("关闭按钮")]
    public Button BtnClose;

    /// <summary>
    /// PVE领奖数加1按钮
    /// </summary>
    [Header("PVE领奖数加1按钮")]
    public Button BtnAddPVERewardNum;

    /// <summary>
    /// PVE领奖数减1按钮
    /// </summary>
    [Header("PVE领奖数减1按钮")]
    public Button BtnMinusPVERewardNum;

    /// <summary>
    /// 响应打开
    /// </summary>
    protected override void OnOpen()
    {
        base.OnOpen();
        RefreshView();
    }

    /// <summary>
    /// 响应关闭
    /// </summary>
    protected override void OnClose()
    {
        base.OnClose();
    }

    /// <summary>
    /// 添加所有Listerner
    /// </summary>
    protected override void AddAllListeners()
    {
        base.AddAllListeners();
        BtnClose.onClick.AddListener(OnBtnCloseClick);
        BtnAddPVERewardNum.onClick.AddListener(OnBtnAddPVERewardNumClick);
        BtnMinusPVERewardNum.onClick.AddListener(OnBtnMinusPVERewardNumClick);
    }

    /// <summary>
    /// 移除所有Listerner
    /// </summary>
    protected override void RemoveAllListeners()
    {
        base.RemoveAllListeners();
        BtnClose.onClick.RemoveListener(OnBtnCloseClick);
        BtnAddPVERewardNum.onClick.RemoveListener(OnBtnAddPVERewardNumClick);
        BtnMinusPVERewardNum.onClick.RemoveListener(OnBtnMinusPVERewardNumClick);
    }

    /// <summary>
    /// 解绑所有红点
    /// </summary>
    protected override void UnbindAllRedDotNames()
    {
        base.UnbindAllRedDotNames();
        PVERewardRedDotWidget.UnbindRedDotName();
    }

    /// <summary>
    /// 绑定所有红点
    /// </summary>
    protected override void BindAllRedDotNames()
    {
        base.BindAllRedDotNames();
        PVERewardRedDotWidget.Init(RedDotNames.PVE_REWARD_ENTRY);
    }

    /// <summary>
    /// 刷新显示
    /// </summary>
    private void RefreshView()
    {
        RefreshLevelInfoView();
    }

    /// <summary>
    /// 刷新PVE信息显示
    /// </summary>
    private void RefreshLevelInfoView()
    {
        PVETxt.text = $"PVE";
    }

    /// <summary>
    /// 响应关闭按钮点击
    /// </summary>
    private void OnBtnCloseClick()
    {
        Close();
    }

    /// <summary>
    /// 响应增加关卡可升级1按钮点击
    /// </summary>
    private void OnBtnAddPVERewardNumClick()
    {
        var newPVERewardNum = PVEModel.Singleton.PVERewardNum + 1;
        PVEModel.Singleton.SetPVERewardNum(newPVERewardNum);
    }

    /// <summary>
    /// 响应减少可穿戴装备1按钮点击
    /// </summary>
    private void OnBtnMinusPVERewardNumClick()
    {
        var newPVERewardNum = PVEModel.Singleton.PVERewardNum - 1;
        PVEModel.Singleton.SetPVERewardNum(newPVERewardNum);
    }
}