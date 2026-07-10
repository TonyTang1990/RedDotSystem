/*
 * Description:             RedDotNames.cs
 * Author:                  TONYTANG
 * Create Date:             2022/08/14
 */

/// <summary>
/// RedDotNames.cs
/// 红点名
/// Note:
/// 红点名采用前缀树风格定义父子关系(采用|分割父子关系)
/// 所有游戏里的单个静态红点都在这里一一定义对应
/// </summary>
public static class RedDotNames
{
    #region 主界面红点部分
    /// <summary>
    /// 主界面菜单红点
    /// </summary>
    public const string MAIN_UI_MENU = "MAIN_UI_MENU";

    /// <summary>
    /// 主界面邮件红点
    /// </summary>
    public const string MAIN_UI_MAIL = "MAIN_UI_MAIL";

    /// <summary>
    /// 主界面菜单装备红点
    /// </summary>
    public const string MAIN_UI_MENU_EQUIP = MAIN_UI_MENU + "|EQUIP";

    /// <summary>
    /// 主界面菜单背包红点
    /// </summary>
    public const string MAIN_UI_MENU_BACKPACK = MAIN_UI_MENU + "|BACKPACK";
    #endregion

    #region 背包界面红点部分
    /// <summary>
    /// 背包界面道具页签红点
    /// </summary>
    public const string BACKPACK_UI_ITEM_TAG = MAIN_UI_MENU_BACKPACK + "|ITEM_TAG";

    /// <summary>
    /// 背包界面资源页签红点
    /// </summary>
    public const string BACKPACK_UI_RESOURCE_TAG = MAIN_UI_MENU_BACKPACK + "|RESOURCE_TAG";

    /// <summary>
    /// 背包界面装备页签红点
    /// </summary>
    public const string BACKPACK_UI_EQUIP_TAG = MAIN_UI_MENU_BACKPACK + "|EQUIP_TAG";
    #endregion

    #region 邮件界面红点部分
    /// <summary>
    /// 邮件界面公共邮件红点
    /// </summary>
    public const string MAIL_UI_PUBLIC_MAIL = MAIN_UI_MAIL + "|PUBLIC_MAIL";

    /// <summary>
    /// 邮件界面战斗邮件红点
    /// </summary>
    public const string MAIL_UI_BATTLE_MAIL = MAIN_UI_MAIL + "|BATTLE_MAIL";

    /// <summary>
    /// 邮件界面其他邮件红点
    /// </summary>
    public const string MAIL_UI_OTHER_MAIL = MAIN_UI_MAIL + "|OTHER_MAIL";
    #endregion

    #region 装备界面红点部分
    /// <summary>
    /// 装备界面可穿戴红点
    /// </summary>
    public const string EQUIP_UI_WEARABLE = MAIN_UI_MENU_EQUIP + "|WEARABLE";

    /// <summary>
    /// 装备界面可升级红点
    /// </summary>
    public const string EQUIP_UI_UPGRADABLE = MAIN_UI_MENU_EQUIP + "|UPGRADABLE";
    #endregion

    #region 关卡界面红点部分
    // Note:
    // 注意这里使用了动态模板红点名的定义方式

    /// <summary>
    /// 主界面关卡红点
    /// </summary>
    public const string MAIN_UI_LEVEL = MAIN_UI_MENU + "|LEVEL";
    
    /// <summary>
    /// PVE入口红点
    /// </summary>
    public const string LEVEL_PVE_ENTRY = MAIN_UI_LEVEL + "|PVE_ENTRY";

    /// <summary>
    /// 关卡入口红点(动态红点名)
    /// </summary>
    public const string LEVEL_ENTRY = MAIN_UI_LEVEL + "|LEVEL_ENTRY_{0}";

    /// <summary>
    /// 关卡可升级红点(动态红点名)
    /// </summary>
    public const string LEVEL_UPGRADE = LEVEL_ENTRY + "|LEVEL_UPGRADE_{0}";

    /// <summary>
    /// 关卡可领奖红点(动态红点名)
    /// </summary>
    public const string LEVEL_REWARD = LEVEL_ENTRY + "|LEVEL_REWARD_{0}";

    #region PVE界面红点部分
    /// <summary>
    /// PVE可领奖红点
    /// </summary>
    public const string PVE_REWARD_ENTRY = LEVEL_PVE_ENTRY + "|PVE_REWARD_ENTRY";
    #endregion

    #endregion

    #region 活动红点部分
    /// <summary>
    /// 活动入口红点的动态红点名模版(动态红点名)
    /// </summary>
    public const string ACT_ENTRY_TEMPLATE = "{0}_ACT_ENTRY";

    /// <summary>
    /// 活动红点1入口红点的动态红点名模版(动态红点名)
    /// </summary>
    public const string ACT_RED_DOT_1_ENTRY_TEMPLATE = ACT_ENTRY_TEMPLATE + "|{0}_ACT_RED_DOT_1_ENTRY";

    /// <summary>
    /// 活动红点2入口红点的动态红点名模版(动态红点名)
    /// </summary>
    public const string ACT_RED_DOT_2_ENTRY_TEMPLATE = ACT_ENTRY_TEMPLATE + "|{0}_ACT_RED_DOT_2_ENTRY";
    #endregion
}