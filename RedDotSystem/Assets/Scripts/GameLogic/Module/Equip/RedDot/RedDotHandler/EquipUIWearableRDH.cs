/*
 * Description:             EquipUIWearableRDH.cs
 * Author:                  TONYTANG
 * Create Date:             2026/04/06
 */

/// <summary>
/// EquipUIWearableRDH.cs
/// 装备界面可穿戴红点处理器
/// </summary>
public class EquipUIWearableRDH : RedDotHandler
{
    /// <summary>
    /// 初始化红点数据
    /// </summary>
    protected override void InitRedDotDatas()
    {
        AddBindRedDotUnit(RedDotUnit.WEARABLE_EQUIP_NUM, "可穿戴装备数", CaculateWearableEquipNum, RedDotType.NUMBER);
    }

    /// <summary>
    /// 添加所有事件监听
    /// </summary>
    protected override void AddAllEvents()
    {
        base.AddAllEvents();
        AddEvent(EventId.WearableEquipNumUpdate, OnWearableEquipNumUpdate);
    }

    /// <summary>
    /// 响应可穿戴装备数量更新
    /// </summary>
    /// <param name="param"></param>
    protected void OnWearableEquipNumUpdate(object param)
    {
        RedDotManager.Singleton.MarkRedDotUnitDirty(RedDotUnit.WEARABLE_EQUIP_NUM);
    }

    /// <summary>
    /// 计算可穿戴装备数
    /// </summary>
    /// <returns></returns>
    protected int CaculateWearableEquipNum()
    {
        return EquipModel.Singleton.WearableEquipNum;
    }
}