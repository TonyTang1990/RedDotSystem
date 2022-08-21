/*
 * Description:             RedDotNames.cs
 * Author:                  TONYTANG
 * Create Date:             2022/08/14
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    /// 主界面新功能1红点
    /// </summary>
    public const string MAIN_UI_NEW_FUNC1 = "MAIN_UI_NEW_FUNC1";

    /// <summary>
    /// 主界面新功能2红点
    /// </summary>
    public const string MAIN_UI_NEW_FUNC2 = "MAIN_UI_NEW_FUNC2";

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
    public const string MAIN_UI_MENU_EQUIP = "MAIN_UI_MENU|EQUIP";

    /// <summary>
    /// 主界面菜单背包红点
    /// </summary>
    public const string MAIN_UI_MENU_BACKPACK = "MAIN_UI_MENU|BACKPACK";
    #endregion

    #region 背包界面红点部分
    /// <summary>
    /// 背包界面道具页签红点
    /// </summary>
    public const string BACKPACK_UI_ITEM_TAG = "MAIN_UI_MENU|BACKPACK|ITEM_TAG";

    /// <summary>
    /// 背包界面资源页签红点
    /// </summary>
    public const string BACKPACK_UI_RESOURCE_TAG = "MAIN_UI_MENU|BACKPACK|RESOURCE_TAG";

    /// <summary>
    /// 背包界面装备页签红点
    /// </summary>
    public const string BACKPACK_UI_EQUIP_TAG = "MAIN_UI_MENU|BACKPACK|EQUIP_TAG";
    #endregion

    #region 邮件界面红点部分
    /// <summary>
    /// 邮件界面公共邮件红点
    /// </summary>
    public const string MAIL_UI_PUBLIC_MAIL = "MAIN_UI_MAIL|PUBLIC_MAIL";

    /// <summary>
    /// 邮件界面战斗邮件红点
    /// </summary>
    public const string MAIL_UI_BATTLE_MAIL = "MAIN_UI_MAIL|BATTLE_MAIL";

    /// <summary>
    /// 邮件界面其他邮件红点
    /// </summary>
    public const string MAIL_UI_OTHER_MAIL = "MAIN_UI_MAIL|OTHER_MAIL";
    #endregion

    #region 装备界面红点部分
    /// <summary>
    /// 装备界面可穿戴红点
    /// </summary>
    public const string EQUIP_UI_WEARABLE = "MAIN_UI_MENU|EQUIP|WEARABLE";

    /// <summary>
    /// 装备界面可升级红点
    /// </summary>
    public const string EQUIP_UI_UPGRADABLE = "MAIN_UI_MENU|EQUIP|UPGRADABLE";
    #endregion
}