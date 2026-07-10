/*
 * Description:             MailUIPublicMailRDH.cs
 * Author:                  TONYTANG
 * Create Date:             2026/04/06
 */

/// <summary>
/// MailUIPublicMailRDH.cs
/// 邮件界面公共邮件红点处理器
/// </summary>
public class MailUIPublicMailRDH : RedDotHandler
{
/// <summary>
    /// 初始化红点数据
    /// </summary>
    protected override void InitRedDotDatas()
    {
        AddBindRedDotUnit(RedDotUnit.NEW_PUBLIC_MAIL_NUM, "新公共邮件数", CaculateNewPublicMailNum, RedDotType.NUMBER);
        AddBindRedDotUnit(RedDotUnit.PUBLIC_MAIL_REWARD_NUM, "公共邮件可领奖数", CaculateNewPublicMailRewardNum, RedDotType.NUMBER);
    }

    /// <summary>
    /// 添加所有事件监听
    /// </summary>
    protected override void AddAllEvents()
    {
        base.AddAllEvents();
        AddEvent(EventId.NewPublicMailNumUpdate, OnNewPublicMailNumUpdate);
        AddEvent(EventId.NewPublicMailRewardNumUpdate, OnNewPublicMailRewardNumUpdate);
    }

    /// <summary>
    /// 响应新公共邮件数量更新
    /// </summary>
    /// <param name="param"></param>
    protected void OnNewPublicMailNumUpdate(object param)
    {
        RedDotManager.Singleton.MarkRedDotUnitDirty(RedDotUnit.NEW_PUBLIC_MAIL_NUM);
    }

    /// <summary>
    /// 响应新公共邮件可领奖数量更新
    /// </summary>
    /// <param name="param"></param>
    protected void OnNewPublicMailRewardNumUpdate(object param)
    {
        RedDotManager.Singleton.MarkRedDotUnitDirty(RedDotUnit.PUBLIC_MAIL_REWARD_NUM);
    }
    
    /// <summary>
    /// 计算新公共邮件数
    /// </summary>
    /// <returns></returns>
    protected int CaculateNewPublicMailNum()
    {
        return MailModel.Singleton.NewPublicMailNum;
    }
    
    /// <summary>
    /// 计算公共邮件可领奖数
    /// </summary>
    /// <returns></returns>
    protected int CaculateNewPublicMailRewardNum()
    {
        return MailModel.Singleton.NewPublicMailRewardNum;
    }
}