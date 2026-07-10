/*
 * Description:             MailUI.cs
 * Author:                  TONYTANG
 * Create Date:             2022/08/14
 */

using UnityEngine;

/// <summary>
/// MailUI.cs
/// 邮件UI
/// </summary>
public class MailUI : BaseUI
{
    /// <summary>
    /// 关闭按钮组件
    /// </summary>
    [Header("关闭按钮组件")]
    public CommonBtnWidget CloseBtnWidget;

    /// <summary>
    /// 增加公共邮件已读1按钮组件
    /// </summary>
    [Header("增加公共邮件已读1按钮组件")]
    public CommonBtnWidget AddReadPublicMailBtnWidget;

    /// <summary>
    /// 减少公共邮件已读1按钮组件
    /// </summary>
    [Header("减少公共邮件已读1按钮组件")]
    public CommonBtnWidget MinusReadPublicMailBtnWidget;

    /// <summary>
    /// 增加公共邮件可领奖1按钮组件
    /// </summary>
    [Header("增加公共邮件可领奖1按钮组件")]
    public CommonBtnWidget AddClaimPublicMailBtnWidget;

    /// <summary>
    /// 减少公共邮件可领奖1按钮组件
    /// </summary>
    [Header("减少公共邮件可领奖1按钮组件")]
    public CommonBtnWidget MinusClaimPublicMailBtnWidget;

    /// <summary>
    /// 公共邮件红点组件
    /// </summary>
    [Header("公共邮件红点组件")]
    public RedDotWidget PublicMailRedDot;

    /// <summary>
    /// 增加战斗邮件已读1按钮组件
    /// </summary>
    [Header("增加战斗邮件已读1按钮组件")]
    public CommonBtnWidget AddReadBattleMailBtnWidget;

    /// <summary>
    /// 减少战斗邮件已读1按钮组件
    /// </summary>
    [Header("减少战斗邮件已读1按钮组件")]
    public CommonBtnWidget MinusReadBattleMailBtnWidget;

    /// <summary>
    /// 增加战斗邮件可领奖1按钮组件
    /// </summary>
    [Header("增加战斗邮件可领奖1按钮组件")]
    public CommonBtnWidget AddClaimBattleMailBtnWidget;

    /// <summary>
    /// 减少战斗邮件可领奖1按钮组件
    /// </summary>
    [Header("减少战斗邮件可领奖1按钮组件")]
    public CommonBtnWidget MinusClaimBattleMailBtnWidget;

    /// <summary>
    /// 战斗邮件红点组件
    /// </summary>
    [Header("战斗邮件红点组件")]
    public RedDotWidget BattleMailRedDot;

    /// <summary>
    /// 增加其他邮件已读1按钮组件
    /// </summary>
    [Header("增加其他邮件已读1按钮组件")]
    public CommonBtnWidget AddReadOtherMailBtnWidget;

    /// <summary>
    /// 减少其他邮件已读1按钮组件
    /// </summary>
    [Header("减少其他邮件已读1按钮组件")]
    public CommonBtnWidget MinusReadOtherMailBtnWidget;

    /// <summary>
    /// 其他邮件红点组件
    /// </summary>
    [Header("其他邮件红点组件")]
    public RedDotWidget OtherMailRedDot;

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
    /// 响应关闭
    /// </summary>
    protected override void OnClose()
    {
        base.OnClose();
    }

    /// <summary>
    /// 添加所有Listener
    /// </summary>
    protected override void AddAllListeners()
    {
        base.AddAllListeners();
        CloseBtnWidget.Init("关闭", OnBtnCloseClick);
        AddReadPublicMailBtnWidget.Init("增加公共已读1", OnBtnAddReadPublicMailClick);
        MinusReadPublicMailBtnWidget.Init("减少公共已读1", OnBtnMinusReadPublicMailClick);
        AddClaimPublicMailBtnWidget.Init("增加公共可领奖1", OnBtnAddClaimPublicMailClick);
        MinusClaimPublicMailBtnWidget.Init("减少公共可领奖1", OnBtnMinusClaimPublicMailClick);
        AddReadBattleMailBtnWidget.Init("增加战斗已读1", OnBtnAddReadBattleMailClick);
        MinusReadBattleMailBtnWidget.Init("减少战斗已读1", OnBtnMinusReadBattleMailClick);
        AddClaimBattleMailBtnWidget.Init("增加战斗可领奖1", OnBtnAddClaimBattleMailClick);
        MinusClaimBattleMailBtnWidget.Init("减少战斗可领奖1", OnBtnMinusClaimBattleMailClick);
        AddReadOtherMailBtnWidget.Init("增加其他已读1", OnBtnAddReadOtherMailClick);
        MinusReadOtherMailBtnWidget.Init("减少其他已读1", OnBtnMinusReadOtherMailClick);
    }

    /// <summary>
    /// 移除所有Listener
    /// </summary>
    protected override void RemoveAllListeners()
    {
        base.RemoveAllListeners();
        CloseBtnWidget.Init("关闭", null);
        AddReadPublicMailBtnWidget.Init("增加公共已读1", null);
        MinusReadPublicMailBtnWidget.Init("减少公共已读1", null);
        AddClaimPublicMailBtnWidget.Init("增加公共可领奖1", null);
        MinusClaimPublicMailBtnWidget.Init("减少公共可领奖1", null);
        AddReadBattleMailBtnWidget.Init("增加战斗已读1", null);
        MinusReadBattleMailBtnWidget.Init("减少战斗已读1", null);
        AddClaimBattleMailBtnWidget.Init("增加战斗可领奖1", null);
        MinusClaimBattleMailBtnWidget.Init("减少战斗可领奖1", null);
        AddReadOtherMailBtnWidget.Init("增加其他已读1", null);
        MinusReadOtherMailBtnWidget.Init("减少其他已读1", null);
    }

    /// <summary>
    /// 绑定所有红点名
    /// </summary>
    protected override void BindAllRedDotNames()
    {
        base.BindAllRedDotNames();
        PublicMailRedDot.Init(RedDotNames.MAIL_UI_PUBLIC_MAIL);
        BattleMailRedDot.Init(RedDotNames.MAIL_UI_BATTLE_MAIL);
        OtherMailRedDot.Init(RedDotNames.MAIL_UI_OTHER_MAIL);
    }

    /// <summary>
    /// 解绑所有红点名
    /// </summary>
    protected override void UnbindAllRedDotNames()
    {
        base.UnbindAllRedDotNames();
        PublicMailRedDot.UnbindRedDotName();
        BattleMailRedDot.UnbindRedDotName();
        OtherMailRedDot.UnbindRedDotName();
    }

    /// <summary>
    /// 初始化显示
    /// </summary>
    private void InitView()
    {
    }

    /// <summary>
    /// 刷新显示
    /// </summary>
    private void RefreshView()
    {
        
    }

    /// <summary>
    /// 响应关闭按钮点击
    /// </summary>
    private void OnBtnCloseClick()
    {
        Close();
    }

    /// <summary>
    /// 响应增加公共邮件已读1按钮点击
    /// </summary>
    private void OnBtnAddReadPublicMailClick()
    {
        var newPublicMailNum = MailModel.Singleton.NewPublicMailNum + 1;
        MailModel.Singleton.SetNewPublicMailNum(newPublicMailNum);
    }

    /// <summary>
    /// 响应减少公共邮件已读1按钮点击
    /// </summary>
    private void OnBtnMinusReadPublicMailClick()
    {
        var newPublicMailNum = MailModel.Singleton.NewPublicMailNum - 1;
        MailModel.Singleton.SetNewPublicMailNum(newPublicMailNum);
    }

    /// <summary>
    /// 响应增加公共邮件可领奖1按钮点击
    /// </summary>
    private void OnBtnAddClaimPublicMailClick()
    {
        var newPublicMailRewardNum = MailModel.Singleton.NewPublicMailRewardNum + 1;
        MailModel.Singleton.SetPublicMailRewardNum(newPublicMailRewardNum);
    }

    /// <summary>
    /// 响应减少公共邮件可领奖1按钮点击
    /// </summary>
    private void OnBtnMinusClaimPublicMailClick()
    {
        var newPublicMailRewardNum = MailModel.Singleton.NewPublicMailRewardNum - 1;
        MailModel.Singleton.SetPublicMailRewardNum(newPublicMailRewardNum);
    }

    /// <summary>
    /// 响应增加战斗邮件已读1按钮点击
    /// </summary>
    private void OnBtnAddReadBattleMailClick()
    {
        var newBattleMailNum = MailModel.Singleton.NewBattleMailNum + 1;
        MailModel.Singleton.SetNewBattleMailNum(newBattleMailNum);
    }

    /// <summary>
    /// 响应减少战斗邮件已读1按钮点击
    /// </summary>
    private void OnBtnMinusReadBattleMailClick()
    {
        var newBattleMailNum = MailModel.Singleton.NewBattleMailNum - 1;
        MailModel.Singleton.SetNewBattleMailNum(newBattleMailNum);
    }

    /// <summary>
    /// 响应增加战斗邮件可领奖1按钮点击
    /// </summary>
    private void OnBtnAddClaimBattleMailClick()
    {
        var newBattleMailRewardNum = MailModel.Singleton.NewBattleMailRewardNum + 1;
        MailModel.Singleton.SetBattleMailRewardNum(newBattleMailRewardNum);
    }

    /// <summary>
    /// 响应减少战斗邮件可领奖1按钮点击
    /// </summary>
    private void OnBtnMinusClaimBattleMailClick()
    {
        var newBattleMailRewardNum = MailModel.Singleton.NewBattleMailRewardNum - 1;
        MailModel.Singleton.SetBattleMailRewardNum(newBattleMailRewardNum);
    }

    /// <summary>
    /// 响应增加其他邮件已读1按钮点击
    /// </summary>
    private void OnBtnAddReadOtherMailClick()
    {
        var newOtherMailNum = MailModel.Singleton.NewOtherMailNum + 1;
        MailModel.Singleton.SetmNewOtherMailNum(newOtherMailNum);
    }

    /// <summary>
    /// 响应减少其他邮件已读1按钮点击
    /// </summary>
    private void OnBtnMinusReadOtherMailClick()
    {
        var newOtherMailNum = MailModel.Singleton.NewOtherMailNum - 1;
        MailModel.Singleton.SetmNewOtherMailNum(newOtherMailNum);
    }
}