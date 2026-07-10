/*
 * Description:             EquipRDInitializer.cs
 * Author:                  TONYTANG
 * Create Date:             2026/04/06
 */

/// <summary>
/// EquipRDInitializer.cs
/// 装备红点初始化器
/// </summary>
public class EquipRDInitializer : SystemRDInitializer
{
    /// <summary>
    /// 系统类型
    /// </summary>
    protected override SystemType SystemType
    {
        get
        {
            return SystemType.Equip;
        }
    }

    /// <summary>
    /// 初始化红点数据
    /// </summary>
    protected override void InitRedDotInfos()
    {
        AddRedDotInfo<EquipUIWearableRDH>(RedDotNames.EQUIP_UI_WEARABLE, "装备界面可穿戴红点");
        AddRedDotInfo<EquipUIUpgradableRDH>(RedDotNames.EQUIP_UI_UPGRADABLE, "装备界面可升级红点");
    }
}