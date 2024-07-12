/*
 * Description:             MailUI.cs
 * Author:                  TONYTANG
 * Create Date:             2022/08/14
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// MailUI.cs
/// 邮件UI
/// </summary>
public class MailUI : MonoBehaviour
{
    /// <summary>
    /// 关闭按钮
    /// </summary>
    [Header("关闭按钮")]
    public Button BtnClose;

    /// <summary>
    /// 增加公共邮件已读1按钮
    /// </summary>
    [Header("增加公共邮件已读1按钮")]
    public Button BtnAddReadPublicMail;

    /// <summary>
    /// 减少公共邮件已读1按钮
    /// </summary>
    [Header("减少公共邮件已读1按钮")]
    public Button BtnMinusReadPublicMail;

    /// <summary>
    /// 增加公共邮件可领奖1按钮
    /// </summary>
    [Header("增加公共邮件可领奖1按钮")]
    public Button BtnAddClaimPublicMail;

    /// <summary>
    /// 减少公共邮件可领奖1按钮
    /// </summary>
    [Header("减少公共邮件可领奖1按钮")]
    public Button BtnMinusClaimPublicMail;

    /// <summary>
    /// 公共邮件红点
    /// </summary>
    [Header("公共邮件红点")]
    public RedDotWidget PublicMailRedDot;

    /// <summary>
    /// 增加战斗邮件已读1按钮
    /// </summary>
    [Header("增加战斗邮件已读1按钮")]
    public Button BtnAddReadBattleMail;

    /// <summary>
    /// 减少战斗邮件已读1按钮
    /// </summary>
    [Header("减少战斗邮件已读1按钮")]
    public Button BtnMinusReadBattleMail;

    /// <summary>
    /// 增加战斗邮件可领奖1按钮
    /// </summary>
    [Header("增加战斗邮件可领奖1按钮")]
    public Button BtnAddClaimBattleMail;

    /// <summary>
    /// 减少战斗邮件可领奖1按钮
    /// </summary>
    [Header("减少战斗邮件可领奖1按钮")]
    public Button BtnMinusClaimBattleMail;

    /// <summary>
    /// 战斗邮件红点
    /// </summary>
    [Header("战斗邮件红点")]
    public RedDotWidget BattleMailRedDot;

    /// <summary>
    /// 增加其他邮件已读1按钮
    /// </summary>
    [Header("增加其他邮件已读1按钮")]
    public Button BtnAddReadOtherMail;

    /// <summary>
    /// 减少其他邮件已读1按钮
    /// </summary>
    [Header("减少其他邮件已读1按钮")]
    public Button BtnMinusReadOtherMail;

    /// <summary>
    /// 其他邮件红点
    /// </summary>
    [Header("其他邮件红点")]
    public RedDotWidget OtherMailRedDot;
    
    /// <summary>
    /// 响应打开
    /// </summary>
    public void OnOpen()
    {
        gameObject.SetActive(true);
        AddAllListeners();
        BindAllRedDotNames();
        RefreshView();
    }

    /// <summary>
    /// 添加所有监听
    /// </summary>
    private void AddAllListeners()
    {
        BtnClose.onClick.AddListener(OnBtnCloseClick);
        BtnAddReadPublicMail.onClick.AddListener(OnBtnAddReadPublicMailClick);
        BtnMinusReadPublicMail.onClick.AddListener(OnBtnMinusReadPublicMailClick);
        BtnAddClaimPublicMail.onClick.AddListener(OnBtnAddClaimPublicMailClick);
        BtnMinusClaimPublicMail.onClick.AddListener(OnBtnMinusClaimPublicMailClick);
        BtnAddReadBattleMail.onClick.AddListener(OnBtnAddReadBattleMailClick);
        BtnMinusReadBattleMail.onClick.AddListener(OnBtnMinusReadBattleMailClick);
        BtnAddClaimBattleMail.onClick.AddListener(OnBtnAddClaimBattleMailClick);
        BtnMinusClaimBattleMail.onClick.AddListener(OnBtnMinusClaimBattleMailClick);
        BtnAddReadOtherMail.onClick.AddListener(OnBtnAddReadOtherMailClick);
        BtnMinusReadOtherMail.onClick.AddListener(OnBtnMinusReadOtherMailClick);
    }

    /// <summary>
    /// 绑定所有红点名
    /// </summary>
    private void BindAllRedDotNames()
    {
        RedDotManager.Singleton.BindRedDotName(RedDotNames.MAIL_UI_PUBLIC_MAIL, OnRedDotRefresh);
        RedDotManager.Singleton.BindRedDotName(RedDotNames.MAIL_UI_BATTLE_MAIL, OnRedDotRefresh);
        RedDotManager.Singleton.BindRedDotName(RedDotNames.MAIL_UI_OTHER_MAIL, OnRedDotRefresh);
    }

    /// <summary>
    /// 刷新显示
    /// </summary>
    private void RefreshView()
    {
        RefreshRedDotView();
    }

    /// <summary>
    /// 刷新红点显示
    /// </summary>
    private void RefreshRedDotView()
    {
        (int result, RedDotType redDotType) redDotNameResult;
        redDotNameResult = RedDotManager.Singleton.GetRedDotNameResult(RedDotNames.MAIL_UI_PUBLIC_MAIL);
        OnRedDotRefresh(RedDotNames.MAIL_UI_PUBLIC_MAIL, redDotNameResult.result, redDotNameResult.redDotType);

        redDotNameResult = RedDotManager.Singleton.GetRedDotNameResult(RedDotNames.MAIL_UI_BATTLE_MAIL);
        OnRedDotRefresh(RedDotNames.MAIL_UI_BATTLE_MAIL, redDotNameResult.result, redDotNameResult.redDotType);

        redDotNameResult = RedDotManager.Singleton.GetRedDotNameResult(RedDotNames.MAIL_UI_OTHER_MAIL);
        OnRedDotRefresh(RedDotNames.MAIL_UI_OTHER_MAIL, redDotNameResult.result, redDotNameResult.redDotType);
    }

    /// <summary>
    /// 响应红点刷新
    /// </summary>
    /// <param name="redDotName"></param>
    /// <param name="result"></param>
    /// <param name="redDotType"></param>
    private void OnRedDotRefresh(string redDotName, int result, RedDotType redDotType)
    {
        var resultText = RedDotUtilities.GetRedDotResultText(result, redDotType);
        if (string.Equals(redDotName, RedDotNames.MAIL_UI_PUBLIC_MAIL))
        {
            PublicMailRedDot.SetActive(result > 0);
            PublicMailRedDot.SetRedDotTxt(resultText);
        }
        else if (string.Equals(redDotName, RedDotNames.MAIL_UI_BATTLE_MAIL))
        {
            BattleMailRedDot.SetActive(result > 0);
            BattleMailRedDot.SetRedDotTxt(resultText);
        }
        else if (string.Equals(redDotName, RedDotNames.MAIL_UI_OTHER_MAIL))
        {
            OtherMailRedDot.SetActive(result > 0);
            OtherMailRedDot.SetRedDotTxt(resultText);
        }
    }

    /// <summary>
    /// 解绑所有红点名
    /// </summary>
    private void UnbindAllRedDotNames()
    {
        RedDotManager.Singleton.UnbindRedDotName(RedDotNames.MAIL_UI_PUBLIC_MAIL, OnRedDotRefresh);
        RedDotManager.Singleton.UnbindRedDotName(RedDotNames.MAIL_UI_BATTLE_MAIL, OnRedDotRefresh);
        RedDotManager.Singleton.UnbindRedDotName(RedDotNames.MAIL_UI_OTHER_MAIL, OnRedDotRefresh);
    }

    /// <summary>
    /// 响应关闭按钮点击
    /// </summary>
    private void OnBtnCloseClick()
    {
        OnClose();
    }

    /// <summary>
    /// 响应增加公共邮件已读1按钮点击
    /// </summary>
    private void OnBtnAddReadPublicMailClick()
    {
        var newPublicMailNum = GameModel.Singleton.NewPublicMailNum + 1;
        GameModel.Singleton.SetNewPublicMailNum(newPublicMailNum);
    }

    /// <summary>
    /// 响应减少公共邮件已读1按钮点击
    /// </summary>
    private void OnBtnMinusReadPublicMailClick()
    {
        var newPublicMailNum = GameModel.Singleton.NewPublicMailNum - 1;
        GameModel.Singleton.SetNewPublicMailNum(newPublicMailNum);
    }

    /// <summary>
    /// 响应增加公共邮件可领奖1按钮点击
    /// </summary>
    private void OnBtnAddClaimPublicMailClick()
    {
        var newPublicMailRewardNum = GameModel.Singleton.NewPublicMailRewardNum + 1;
        GameModel.Singleton.SetPublicMailRewardNum(newPublicMailRewardNum);
    }

    /// <summary>
    /// 响应减少公共邮件可领奖1按钮点击
    /// </summary>
    private void OnBtnMinusClaimPublicMailClick()
    {
        var newPublicMailRewardNum = GameModel.Singleton.NewPublicMailRewardNum - 1;
        GameModel.Singleton.SetPublicMailRewardNum(newPublicMailRewardNum);
    }

    /// <summary>
    /// 响应增加战斗邮件已读1按钮点击
    /// </summary>
    private void OnBtnAddReadBattleMailClick()
    {
        var newBattleMailNum = GameModel.Singleton.NewBattleMailNum + 1;
        GameModel.Singleton.SetNewBattleMailNum(newBattleMailNum);
    }

    /// <summary>
    /// 响应减少战斗邮件已读1按钮点击
    /// </summary>
    private void OnBtnMinusReadBattleMailClick()
    {
        var newBattleMailNum = GameModel.Singleton.NewBattleMailNum - 1;
        GameModel.Singleton.SetNewBattleMailNum(newBattleMailNum);
    }

    /// <summary>
    /// 响应增加战斗邮件可领奖1按钮点击
    /// </summary>
    private void OnBtnAddClaimBattleMailClick()
    {
        var newBattleMailRewardNum = GameModel.Singleton.NewBattleMailRewardNum + 1;
        GameModel.Singleton.SetBattleMailRewardNum(newBattleMailRewardNum);
    }

    /// <summary>
    /// 响应减少战斗邮件可领奖1按钮点击
    /// </summary>
    private void OnBtnMinusClaimBattleMailClick()
    {
        var newBattleMailRewardNum = GameModel.Singleton.NewBattleMailRewardNum - 1;
        GameModel.Singleton.SetBattleMailRewardNum(newBattleMailRewardNum);
    }

    /// <summary>
    /// 响应增加其他邮件已读1按钮点击
    /// </summary>
    private void OnBtnAddReadOtherMailClick()
    {
        var newOtherMailNum = GameModel.Singleton.NewOtherMailNum + 1;
        GameModel.Singleton.SetmNewOtherMailNum(newOtherMailNum);
    }

    /// <summary>
    /// 响应减少其他邮件已读1按钮点击
    /// </summary>
    private void OnBtnMinusReadOtherMailClick()
    {
        var newOtherMailNum = GameModel.Singleton.NewOtherMailNum - 1;
        GameModel.Singleton.SetmNewOtherMailNum(newOtherMailNum);
    }

    /// <summary>
    /// 移除所有监听
    /// </summary>
    private void RemoveAllListeners()
    {
        BtnClose.onClick.RemoveListener(OnBtnCloseClick);
        BtnAddReadPublicMail.onClick.RemoveListener(OnBtnAddReadPublicMailClick);
        BtnMinusReadPublicMail.onClick.RemoveListener(OnBtnMinusReadPublicMailClick);
        BtnAddClaimPublicMail.onClick.RemoveListener(OnBtnAddClaimPublicMailClick);
        BtnMinusClaimPublicMail.onClick.RemoveListener(OnBtnMinusClaimPublicMailClick);
        BtnAddReadBattleMail.onClick.RemoveListener(OnBtnAddReadBattleMailClick);
        BtnMinusReadBattleMail.onClick.RemoveListener(OnBtnMinusReadBattleMailClick);
        BtnAddClaimBattleMail.onClick.RemoveListener(OnBtnAddClaimBattleMailClick);
        BtnMinusClaimBattleMail.onClick.RemoveListener(OnBtnMinusClaimBattleMailClick);
        BtnAddReadOtherMail.onClick.RemoveListener(OnBtnAddReadOtherMailClick);
        BtnMinusReadOtherMail.onClick.RemoveListener(OnBtnMinusReadOtherMailClick);
    }

    /// <summary>
    /// 响应关闭
    /// </summary>
    public void OnClose()
    {
        gameObject.SetActive(false);
        UnbindAllRedDotNames();
        RemoveAllListeners();
    }
}