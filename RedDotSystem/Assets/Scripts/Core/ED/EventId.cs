/*
 * Description:             EventId.cs
 * Author:                  TONYTANG
 * Create Date:             2018/12/09
 */

/// <summary>
/// EventId.cs
/// 事件ID枚举定义
/// </summary>
public enum EventId
{
    /// <summary>
    /// 默认事件id
    /// </summary>
    DefaultEvent = 1,

    /// <summary>
    /// 系统解锁状态更新事件
    /// </summary>
    SystemUnlockStateUpdate,

    /// <summary>
    /// 新道具数量更新
    /// </summary>
    NewItemNumUpdate,

    /// <summary>
    /// 新资源数量更新
    /// </summary>
    NewResourceNumUpdate,

    /// <summary>
    /// 新装备数量更新
    /// </summary>
    NewEquipNumUpdate,

    /// <summary>
    /// 新公共邮件数量更新
    /// </summary>
    NewPublicMailNumUpdate,

    /// <summary>
    /// 新战斗邮件数量更新
    /// </summary>
    NewBattleMailNumUpdate,

    /// <summary>
    /// 新其他邮件数量更新
    /// </summary>
    NewOtherMailNumUpdate,

    /// <summary>
    /// 新公共邮件可领奖数量更新
    /// </summary>
    NewPublicMailRewardNumUpdate,

    /// <summary>
    /// 新战斗邮件可领奖数量更新
    /// </summary>
    NewBattleMailRewardNumUpdate,

    /// <summary>
    /// 可穿戴装备数量更新
    /// </summary>
    WearableEquipNumUpdate,

    /// <summary>
    /// 可升级装备数量更新
    /// </summary>
    UpgradeableEquipNumUpdate,

    /// <summary>
    /// 指定关卡id可升级数量更新
    /// </summary>
    LevelUpgradeNumUpdate,

    /// <summary>
    /// 指定关卡id可领奖数量更新
    /// </summary>
    LevelRewardNumUpdate,

    /// <summary>
    /// PVE可领奖数量更新
    /// </summary>
    PVERewardNumUpdate,

    /// <summary>
    /// 活动数据增加
    /// </summary>
    ActivityDataAdd,

    /// <summary>
    /// 活动数据移除
    /// </summary>
    ActivityDataRemove,

    /// <summary>
    /// 活动激活数据更新
    /// </summary>
    IsActiveUpdate,

    /// <summary>
    /// 活动红点数量1更新
    /// </summary>
    RedDot1NumUpdate,

    /// <summary>
    /// 活动红点数量2更新
    /// </summary>
    RedDot2NumUpdate,
}