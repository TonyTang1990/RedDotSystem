/*
 * Description:             EquipUIUpgradableRDH.cs
 * Author:                  TONYTANG
 * Create Date:             2026/04/06
 */

/// <summary>
/// EquipUIUpgradableRDH.cs
/// 装备界面可升级红点处理器
/// </summary>
public class EquipUIUpgradableRDH : RedDotHandler
{
/// <summary>
    /// 初始化红点数据
    /// </summary>
    protected override void InitRedDotDatas()
    {
        AddBindRedDotUnit(RedDotUnit.UPGRADEABLE_EQUIP_NUM, "可升级装备数", CaculateUpgradeableEquipNum, RedDotType.NUMBER);
    }

    /// <summary>
    /// 添加所有事件监听
    /// </summary>
    protected override void AddAllEvents()
    {
        base.AddAllEvents();
        AddEvent(EventId.UpgradeableEquipNumUpdate, OnUpgradeableEquipNumUpdate);
    }

    /// <summary>
    /// 响应可升级装备数量更新
    /// </summary>
    /// <param name="param"></param>

    protected void OnUpgradeableEquipNumUpdate(object param)
    {
        RedDotManager.Singleton.MarkRedDotUnitDirty(RedDotUnit.UPGRADEABLE_EQUIP_NUM);
    }

    /// <summary>
    /// 计算可升级装备数
    /// </summary>
    /// <returns></returns>
    protected int CaculateUpgradeableEquipNum()
    {
        return EquipModel.Singleton.UpgradeableEquipNum;
    }
}