/*
 * Description:             MailUIBattleMailRDH.cs
 * Author:                  TONYTANG
 * Create Date:             2026/04/06
 */

/// <summary>
/// MailUIBattleMailRDH.cs
/// 邮件界面战斗邮件红点处理器
/// </summary>
public class MailUIBattleMailRDH : RedDotHandler
{
    /// <summary>
    /// 初始化红点数据
    /// </summary>
    protected override void InitRedDotDatas()
    {
        AddBindRedDotUnit(RedDotUnit.NEW_BATTLE_MAIL_NUM, "新战斗邮件数", CaculateNewBattleMailNum, RedDotType.NUMBER);
        AddBindRedDotUnit(RedDotUnit.BATTLE_MAIL_REWARD_NUM, "战斗邮件可领奖数", CaculateNewBattleMailRewardNum, RedDotType.NUMBER);
    }

    /// <summary>
    /// 添加所有事件监听
    /// </summary>
    protected override void AddAllEvents()
    {
        base.AddAllEvents();
        AddEvent(EventId.NewBattleMailNumUpdate, OnNewBattleMailNumUpdate);
        AddEvent(EventId.NewBattleMailRewardNumUpdate, OnNewBattleMailRewardNumUpdate);
    }

    /// <summary>
    /// 响应新战斗邮件数量更新
    /// </summary>
    /// <param name="param"></param>
    protected void OnNewBattleMailNumUpdate(object param)
    {
        RedDotManager.Singleton.MarkRedDotUnitDirty(RedDotUnit.NEW_BATTLE_MAIL_NUM);
    }

    /// <summary>
    /// 响应新战斗邮件可领奖数量更新
    /// </summary>
    /// <param name="param"></param>
    protected void OnNewBattleMailRewardNumUpdate(object param)
    {
        RedDotManager.Singleton.MarkRedDotUnitDirty(RedDotUnit.BATTLE_MAIL_REWARD_NUM);
    }
    
    /// <summary>
    /// 计算新战斗邮件数
    /// </summary>
    /// <returns></returns>
    protected int CaculateNewBattleMailNum()
    {
        return MailModel.Singleton.NewBattleMailNum;
    }

    /// <summary>
    /// 计算战斗邮件可领奖数
    /// </summary>
    /// <returns></returns>
    protected int CaculateNewBattleMailRewardNum()
    {
        return MailModel.Singleton.NewBattleMailRewardNum;
    }
}