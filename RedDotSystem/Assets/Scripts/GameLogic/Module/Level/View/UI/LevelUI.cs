/*
 * Description:             LevelUI.cs
 * Author:                  TONYTANG
 * Create Date:             2026/04/02
 */

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// LevelUI.cs
/// 关卡UI
/// </summary>
public class LevelUI : BaseUI
{
    /// <summary>
    /// 关卡文本
    /// </summary>
    [Header("关卡文本")]
    public Text LevelTxt;

    /// <summary>
    /// 关卡可升级红点组件
    /// </summary>
    [Header("关卡可升级红点组件")]
    public RedDotWidget LevelUpgradeRedDotWidget;

    /// <summary>
    /// 关卡可领奖红点组件
    /// </summary>
    [Header("关卡可领奖红点组件")]
    public RedDotWidget LevelRewardRedDotWidget;

    /// <summary>
    /// 关闭按钮
    /// </summary>
    [Header("关闭按钮")]
    public Button BtnClose;

    /// <summary>
    /// 增加关卡可升级1按钮
    /// </summary>
    [Header("增加关卡可升级1按钮")]
    public Button BtnAddLevelUpgrade;

    /// <summary>
    /// 减少关卡可升级1按钮
    /// </summary>
    [Header("减少关卡可升级1按钮")]
    public Button BtnMinusLevelUpgrade;

    /// <summary>
    /// 增加关卡可领奖1按钮
    /// </summary>
    [Header("增加关卡可领奖1按钮")]
    public Button BtnAddLevelReward;

    /// <summary>
    /// 减少关卡可领奖1按钮
    /// </summary>
    [Header("减少关卡可领奖1按钮")]
    public Button BtnMinusLevelReward;

    /// <summary>
    /// 关卡Id
    /// </summary>
    [Header("关卡Id")]
    private int mLevelId;

    /// <summary>
    /// 关卡可升级红点名
    /// </summary>
    private string mLevelUpgradeRedDotName;

    /// <summary>
    /// 关卡可领奖红点名
    /// </summary>
    private string mLevelRewardRedDotName;

    /// <summary>
    /// 初始化数据
    /// </summary>
    /// <param name="args"></param>
    protected override void InitDatas(params object[] args)
    {
        base.InitDatas(args);
        mLevelId = (int)args[0];
        mLevelUpgradeRedDotName = RedDotUtilities.GetTemplateRDName(RedDotNames.LEVEL_UPGRADE, mLevelId);
        mLevelRewardRedDotName = RedDotUtilities.GetTemplateRDName(RedDotNames.LEVEL_REWARD, mLevelId);
    }

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
        mLevelId = 0;
        mLevelUpgradeRedDotName = null;
        mLevelRewardRedDotName = null;
    }

    /// <summary>
    /// 添加所有监听
    /// </summary>
    protected override void AddAllListeners()
    {
        base.AddAllListeners();
        BtnClose.onClick.AddListener(OnBtnCloseClick);
        BtnAddLevelUpgrade.onClick.AddListener(OnBtnAddLevelUpgradeClick);
        BtnMinusLevelUpgrade.onClick.AddListener(OnBtnMinusLevelUpgradeClick);
        BtnAddLevelReward.onClick.AddListener(OnBtnAddLevelRewardClick);
        BtnMinusLevelReward.onClick.AddListener(OnBtnMinusLevelRewardClick);
    }

    protected override void RemoveAllListeners()
    {
        base.RemoveAllListeners();
        BtnClose.onClick.RemoveListener(OnBtnCloseClick);
        BtnAddLevelUpgrade.onClick.RemoveListener(OnBtnAddLevelUpgradeClick);
        BtnMinusLevelUpgrade.onClick.RemoveListener(OnBtnMinusLevelUpgradeClick);
        BtnAddLevelReward.onClick.RemoveListener(OnBtnAddLevelRewardClick);
        BtnMinusLevelReward.onClick.RemoveListener(OnBtnMinusLevelRewardClick);
    }

    /// <summary>
    /// 绑定所有红点
    /// </summary>
    protected override void BindAllRedDotNames()
    {
        base.BindAllRedDotNames();
        LevelUpgradeRedDotWidget.Init(mLevelUpgradeRedDotName);
        LevelRewardRedDotWidget.Init(mLevelRewardRedDotName);
    }

    /// <summary>
    /// 解绑所有红点
    /// </summary>
    protected override void UnbindAllRedDotNames()
    {
        base.UnbindAllRedDotNames();
        LevelUpgradeRedDotWidget.UnbindRedDotName();
        LevelRewardRedDotWidget.UnbindRedDotName();
    }

    /// <summary>
    /// 刷新显示
    /// </summary>
    private void RefreshView()
    {
        RefreshLevelInfoView();
    }

    /// <summary>
    /// 刷新关卡信息显示
    /// </summary>
    private void RefreshLevelInfoView()
    {
        LevelTxt.text = $"关卡Id:{mLevelId}";
    }

    /// <summary>
    /// 响应关闭按钮点击
    /// </summary>
    private void OnBtnCloseClick()
    {
        OnClose();
    }

    /// <summary>
    /// 响应增加关卡可升级1按钮点击
    /// </summary>
    private void OnBtnAddLevelUpgradeClick()
    {
        var newLevelUpgradeNum = LevelModel.Singleton.GetLevelUpgradeNum(mLevelId) + 1;
        LevelModel.Singleton.SetLevelUpgradeNum(mLevelId, newLevelUpgradeNum);
    }

    /// <summary>
    /// 响应减少可穿戴装备1按钮点击
    /// </summary>
    private void OnBtnMinusLevelUpgradeClick()
    {
        var newLevelUpgradeNum = LevelModel.Singleton.GetLevelUpgradeNum(mLevelId) - 1;
        LevelModel.Singleton.SetLevelUpgradeNum(mLevelId, newLevelUpgradeNum);
    }

    /// <summary>
    /// 响应增加关卡可领奖1按钮点击
    /// </summary>
    private void OnBtnAddLevelRewardClick()
    {
        var newLevelRewardNum = LevelModel.Singleton.GetLevelRewardNum(mLevelId) + 1;
        LevelModel.Singleton.SetLevelRewardNum(mLevelId, newLevelRewardNum);
    }

    /// <summary>
    /// 响应减少关卡可领奖1按钮点击
    /// </summary>
    private void OnBtnMinusLevelRewardClick()
    {
        var newLevelRewardNum = LevelModel.Singleton.GetLevelRewardNum(mLevelId) - 1;
        LevelModel.Singleton.SetLevelRewardNum(mLevelId, newLevelRewardNum);
    }
}