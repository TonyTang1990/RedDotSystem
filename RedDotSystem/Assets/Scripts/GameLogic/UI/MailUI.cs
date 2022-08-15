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
    /// 增加其他邮件可领奖1按钮
    /// </summary>
    [Header("增加其他邮件可领奖1按钮")]
    public Button BtnAddClaimOtherMail;

    /// <summary>
    /// 减少其他邮件可领奖1按钮
    /// </summary>
    [Header("减少其他邮件可领奖1按钮")]
    public Button BtnMinusClaimOtherMail;

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
        AddAllListeners();
        BindAllRedDotNames();
        RefreshView();
    }

    /// <summary>
    /// 添加所有监听
    /// </summary>
    private void AddAllListeners()
    {

    }

    /// <summary>
    /// 绑定所有红点名
    /// </summary>
    private void BindAllRedDotNames()
    {

    }

    /// <summary>
    /// 刷新显示
    /// </summary>
    private void RefreshView()
    {

    }

    /// <summary>
    /// 解绑所有红点名
    /// </summary>
    private void UnbindAllRedDotNames()
    {

    }

    /// <summary>
    /// 移除所有监听
    /// </summary>
    private void RemoveAllListeners()
    {

    }

    /// <summary>
    /// 响应关闭
    /// </summary>
    public void OnClose()
    {
        UnbindAllRedDotNames();
        RemoveAllListeners();
    }
}