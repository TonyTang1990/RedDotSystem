/*
 * Description:             BackpackUIEquipTagRDH.cs
 * Author:                  TONYTANG
 * Create Date:             2026/04/06
 */

/// <summary>
/// BackpackUIEquipTagRDH.cs
/// 背包界面装备页签红点处理器
/// </summary>
public class BackpackUIEquipTagRDH : RedDotHandler
{
    /// <summary>
    /// 初始化红点数据
    /// </summary>
    protected override void InitRedDotDatas()
    {
        AddBindRedDotUnit(RedDotUnit.NEW_EQUIP_NUM, "新装备数", CaculateNewEquipNum, RedDotType.NUMBER);
    }

    /// <summary>
    /// 添加所有事件监听
    /// </summary>
    protected override void AddAllEvents()
    {
        base.AddAllEvents();
        AddEvent(EventId.NewEquipNumUpdate, OnNewEquipNumUpdate);
    }

    /// <summary>
    /// 响应装备数量更新
    /// </summary>
    /// <param name="param"></param>
    protected void OnNewEquipNumUpdate(object param)
    {
        RedDotManager.Singleton.MarkRedDotUnitDirty(RedDotUnit.NEW_EQUIP_NUM);
    }

    /// <summary>
    /// 计算新装备数
    /// </summary>
    /// <returns></returns>
    protected int CaculateNewEquipNum()
    {
        return BackpackModel.Singleton.NewEquipNum;
    }
}