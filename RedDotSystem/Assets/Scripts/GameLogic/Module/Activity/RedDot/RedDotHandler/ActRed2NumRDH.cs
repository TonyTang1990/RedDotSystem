/*
 * Description:             ActRed2NumRDH.cs
 * Author:                  TONYTANG
 * Create Date:             2026/07/09
 */

/// <summary>
/// ActRed2NumRDH.cs
/// 活动红点2数量红点处理器
/// </summary>
public class ActRed2NumRDH : DynamicRedDotHandler<int>
{
    /// <summary>
    /// 初始化红点信息
    /// </summary>
    protected override void InitRedDotDatas()
    {
        var actRed2NumRDU = RedDotUtilities.GetDynamicRedDotUnit(RedDotUnit.ACT_RED_2_NUM, Data);
        AddBindRedDotUnit(actRed2NumRDU, $"活动{Data}红点2数量", CaculateActRed2Num, RedDotType.NUMBER);
    }

    /// <summary>
    /// 添加所有事件
    /// </summary>
    protected override void AddAllEvents()
    {
        base.AddAllEvents();
        AddEvent(EventId.RedDot2NumUpdate, OnRedDot2NumUpdate);
    }

    /// <summary>
    /// 红点2数量更新事件
    /// </summary>
    /// <param name="evt"></param>
    private void OnRedDot2NumUpdate(BaseEvent evt)
    {
        var eventParams = evt.EventParams;
        var activityConfId = (int)eventParams[0];
        if(activityConfId != Data)
        {
            return;
        }
        var dynamicRedDotUnit = RedDotUtilities.GetDynamicRedDotUnit(RedDotUnit.ACT_RED_2_NUM, activityConfId);
        RedDotManager.Singleton.MarkRedDotUnitDirty(dynamicRedDotUnit);
    }

    /// <summary>
    /// 计算活动红点2红点数量
    /// </summary>
    /// <returns></returns>
    private int CaculateActRed2Num()
    {
        var activityData = ActivityModel.Singleton.GetActivityData(Data);
        if (activityData == null || !activityData.IsActive)
        {
            return 0;
        }
        return activityData.RedDot2Num;
    }
}