/*
 * Description:             BackpackRDInitializer.cs
 * Author:                  TONYTANG
 * Create Date:             2026/04/06
 */

/// <summary>
/// BackpackRDInitializer.cs
/// 背包红点初始化器
/// </summary>
public class BackpackRDInitializer : FuncRDInitializer
{
    /// <summary>
    /// 初始化红点数据
    /// </summary>
    protected override void InitRedDotInfos()
    {
        AddRedDotInfo<BackpackUIItemTagRDH>(RedDotNames.BACKPACK_UI_ITEM_TAG, "背包界面道具页签红点");
        AddRedDotInfo<BackpackUIResourceTagRDH>(RedDotNames.BACKPACK_UI_RESOURCE_TAG, "背包界面资源页签红点");
        AddRedDotInfo<BackpackUIEquipTagRDH>(RedDotNames.BACKPACK_UI_EQUIP_TAG, "背包界面装备页签红点");
    }
}