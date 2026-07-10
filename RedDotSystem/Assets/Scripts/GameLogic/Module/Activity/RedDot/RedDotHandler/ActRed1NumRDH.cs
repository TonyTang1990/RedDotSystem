/*
 * Description:             ActRed1NumRDH.cs
 * Author:                  TONYTANG
 * Create Date:             2026/07/09
 */

/// <summary>
/// ActRed1NumRDH.cs
/// 活动红点1数量红点处理器
/// </summary>
public class ActRed1NumRDH : DynamicRedDotHandler<int>
{
    /// <summary>
    /// 初始化红点信息
    /// </summary>
    protected override void InitRedDotDatas()
    {
        var actRed1NumRDU = RedDotUtilities.GetDynamicRedDotUnit(RedDotUnit.ACT_RED_1_NUM, Data);
        AddBindRedDotUnit(actRed1NumRDU, $"活动{Data}红点1数量", CaculateActRed1Num, RedDotType.NUMBER);
    }

    /// <summary>
    /// 添加所有事件
    /// </summary>
    protected override void AddAllEvents()
    {
        base.AddAllEvents();
        AddEvent(EventId.RedDot1NumUpdate, OnRedDot1NumUpdate);
    }

    /// <summary>
    /// 红点1数量更新事件
    /// </summary>
    /// <param name="evt"></param>
    private void OnRedDot1NumUpdate(BaseEvent evt)
    {
        var eventParams = evt.EventParams;
        var activityConfId = (int)eventParams[0];
        if(activityConfId != Data)
        {
            return;
        }
        var dynamicRedDotUnit = RedDotUtilities.GetDynamicRedDotUnit(RedDotUnit.ACT_RED_1_NUM, activityConfId);
        RedDotManager.Singleton.MarkRedDotUnitDirty(dynamicRedDotUnit);
    }

    /// <summary>
    /// 计算活动红点1红点数量
    /// </summary>
    /// <returns></returns>
    private int CaculateActRed1Num()
    {
        var activityData = ActivityModel.Singleton.GetActivityData(Data);
        if (activityData == null || !activityData.IsActive)
        {
            return 0;
        }
        return activityData.RedDot1Num;
    }
}