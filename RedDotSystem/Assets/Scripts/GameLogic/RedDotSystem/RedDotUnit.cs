/*
 * Description:             RedDotUnit.cs
 * Author:                  TONYTANG
 * Create Date:             2022/08/14
 */

/// <summary>
/// RedDotUnit.cs
/// 红点运算单元名
/// Note:
/// 1. 游戏里的红点运算单元按最小单位定义，所有红点的影响因素通过红点运算单元组装而成
/// </summary>
public static class RedDotUnit
{
    #region 主UI部分
    /// <summary>
    /// 动态新功能1解锁
    /// </summary>
    public const string NEW_FUNC1 = "NEW_FUNC1";

    /// <summary>
    /// 动态新功能2解锁
    /// </summary>
    public const string NEW_FUNC2 = "NEW_FUNC2";
    #endregion

    #region 背包部分
    /// <summary>
    /// 新道具数
    /// </summary>
    public const string NEW_ITEM_NUM = "NEW_ITEM_NUM";

    /// <summary>
    /// 新资源数
    /// </summary>
    public const string NEW_RESOURCE_NUM = "NEW_RESOURCE_NUM";

    /// <summary>
    /// 新装备数
    /// </summary>
    public const string NEW_EQUIP_NUM = "NEW_EQUIP_NUM";
    #endregion

    #region 邮件部分
    /// <summary>
    /// 新公共邮件数
    /// </summary>
    public const string NEW_PUBLIC_MAIL_NUM = "NEW_PUBLIC_MAIL_NUM";

    /// <summary>
    /// 新战斗邮件数
    /// </summary>
    public const string NEW_BATTLE_MAIL_NUM = "NEW_BATTLE_MAIL_NUM";

    /// <summary>
    /// 新其他邮件数
    /// </summary>
    public const string NEW_OTHER_MAIL_NUM = "NEW_OTHER_MAIL_NUM";

    /// <summary>
    /// 公共邮件可领奖数
    /// </summary>
    public const string PUBLIC_MAIL_REWARD_NUM = "PUBLIC_MAIL_REWARD_NUM";

    /// <summary>
    /// 战斗邮件可领奖数
    /// </summary>
    public const string BATTLE_MAIL_REWARD_NUM = "BATTLE_MAIL_REWARD_NUM";
    #endregion

    #region 装备部分
    /// <summary>
    /// 可穿戴装备数
    /// </summary>
    public const string WEARABLE_EQUIP_NUM = "WEARABLE_EQUIP_NUM";

    /// <summary>
    /// 可升级装备数
    /// </summary>
    public const string UPGRADEABLE_EQUIP_NUM = "UPGRADEABLE_EQUIP_NUM";
    #endregion

    #region 关卡部分
    /// <summary>
    /// 关卡可升级数量(动态红点单元)
    /// </summary>
    public const string LEVEL_UPGRADE_NUM = "LEVEL_UPGRADE_NUM";

    /// <summary>
    /// 关卡可领奖数量(动态红点单元)
    /// </summary>
    public const string LEVEL_REWARD_NUM = "LEVEL_REWARD_NUM";
    #endregion

    #region PVE部分
    /// <summary>
    /// PVE奖励入口红点数量
    /// </summary>
    public const string PVE_REWARD_NUM = "PVE_REWARD_NUM";
    #endregion

    #region 活动部分
    /// <summary>
    /// 活动红点1数量(动态红点单元)
    /// </summary>
    public const string ACT_RED_1_NUM = "ACT_RED_1_NUM";

    /// <summary>
    /// 活动红点2数量(动态红点单元)
    /// </summary>
    public const string ACT_RED_2_NUM = "ACT_RED_2_NUM";
    #endregion
}