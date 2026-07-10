/*
 * Description:             PVERewardRDH.cs
 * Author:                  TONYTANG
 * Create Date:             2026/07/09
 */

/// <summary>
/// PVERewardRDH.cs
/// PVE奖励入口红点处理器
/// </summary>
public class PVERewardRDH : RedDotHandler
{
    /// <summary>
    /// 初始化红点数据
    /// </summary>
    protected override void InitRedDotDatas()
    {
        AddBindRedDotUnit(RedDotUnit.PVE_REWARD_NUM, "PVE奖励数量", CaculatePVERewardNum, RedDotType.NUMBER);
    }
    
    /// <summary>
    /// 添加所有事件
    /// </summary>
    protected override void AddAllEvents()
    {
        base.AddAllEvents();
        AddEvent(EventId.PVERewardNumUpdate, OnPVERewardNumUpdate);
    }

    /// <summary>
    /// PVE奖励数量更新事件
    /// </summary>
    /// <param name="evt"></param>
    protected void OnPVERewardNumUpdate(BaseEvent evt)
    {
        RedDotManager.Singleton.MarkRedDotUnitDirty(RedDotUnit.PVE_REWARD_NUM);
    }

    /// <summary>
    /// 计算PVE奖励数量
    /// </summary>
    /// <returns></returns>
    private int CaculatePVERewardNum()
    {
        return PVEModel.Singleton.PVERewardNum;
    }
}