/*
 * Description:             MailUIOtherMailRDH.cs
 * Author:                  TONYTANG
 * Create Date:             2026/04/06
 */

/// <summary>
/// MailUIOtherMailRDH.cs
/// 邮件界面其他邮件红点处理器
/// </summary>
public class MailUIOtherMailRDH : RedDotHandler
{
/// <summary>
    /// 初始化红点数据
    /// </summary>
    protected override void InitRedDotDatas()
    {
        AddBindRedDotUnit(RedDotUnit.NEW_OTHER_MAIL_NUM, "新其他邮件数", CaculateNewOtherMailNum, RedDotType.NUMBER);
    }

    /// <summary>
    /// 添加所有事件监听
    /// </summary>
    protected override void AddAllEvents()
    {
        base.AddAllEvents();
        AddEvent(EventId.NewOtherMailNumUpdate, OnNewOtherMailNumUpdate);
    }

    /// <summary>
    /// 响应新其他邮件数量更新
    /// </summary>
    /// <param name="param"></param>
    protected void OnNewOtherMailNumUpdate(object param)
    {
        RedDotManager.Singleton.MarkRedDotUnitDirty(RedDotUnit.NEW_OTHER_MAIL_NUM);
    }
    
    /// <summary>
    /// 计算新其他邮件数
    /// </summary>
    /// <returns></returns>
    protected int CaculateNewOtherMailNum()
    {
        return MailModel.Singleton.NewOtherMailNum;
    }
}