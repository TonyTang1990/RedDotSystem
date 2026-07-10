/*
 * Description:             BackpackUIResourceTagRDH.cs
 * Author:                  TONYTANG
 * Create Date:             2026/04/06
 */

/// <summary>
/// BackpackUIResourceTagRDH.cs
/// 背包界面资源页签红点处理器
/// </summary>
public class BackpackUIResourceTagRDH : RedDotHandler
{
    /// <summary>
    /// 初始化红点数据
    /// </summary>
    protected override void InitRedDotDatas()
    {
        AddBindRedDotUnit(RedDotUnit.NEW_RESOURCE_NUM, "新资源数", CaculateNewResourceNum, RedDotType.NUMBER);
    }

    /// <summary>
    /// 添加所有事件监听
    /// </summary>
    protected override void AddAllEvents()
    {
        base.AddAllEvents();
        AddEvent(EventId.NewResourceNumUpdate, OnNewResourceNumUpdate);
    }


    /// <summary>
    /// 响应道具数量更新
    /// </summary>
    /// <param name="param"></param>
    protected void OnNewResourceNumUpdate(object param)
    {
        RedDotManager.Singleton.MarkRedDotUnitDirty(RedDotUnit.NEW_RESOURCE_NUM);
    }

    /// <summary>
    /// 计算新资源数
    /// </summary>
    /// <returns></returns>
    public static int CaculateNewResourceNum()
    {
        return BackpackModel.Singleton.NewResourceNum;
    }
}