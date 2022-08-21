/*
 * Description:             RedDotUnit.cs
 * Author:                  TONYTANG
 * Create Date:             2022/08/14
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// RedDotUnit.cs
/// 红点运算单元枚举
/// Note:
/// 游戏里的红点运算单元按最小单位定义，所有红点的影响因素通过红点运算单元组装而成
/// </summary>
public enum RedDotUnit
{
    INVALIDE = 0,                           // 无效类型
    #region 主UI部分
    NEW_FUNC1,                              // 动态新功能1解锁
    NEW_FUNC2,                              // 动态新功能2解锁
    #endregion
    #region 背包部分
    NEW_ITEM_NUM,                           // 新道具数
    NEW_RESOURCE_NUM,                       // 新资源数
    NEW_EQUIP_NUM,                          // 新装备数
    #endregion
    #region 邮件部分
    NEW_PUBLIC_MAIL_NUM,                    // 新公共邮件数
    NEW_BATTLE_MAIL_NUM,                    // 新战斗邮件数
    NEW_OTHER_MAIL_NUM,                     // 新其他邮件数
    PUBLIC_MAIL_REWARD_NUM,                 // 公共邮件可领奖数
    BATTLE_MAIL_REWARD_NUM,                 // 战斗邮件可领奖数
    #endregion
    #region 装备部分
    WEARABLE_EQUIP_NUM,                     // 可穿戴装备数
    UPGRADEABLE_EQUIP_NUM,                  // 可升级装备数
    #endregion
}

/// <summary>
/// 重写RedDotUnit比较相关接口函数，避免RedDotUnit作为Dictionary Key时，
/// 底层调用默认Equals(object obj)和DefaultCompare.GetHashCode()导致额外的堆内存分配
/// 参考:
/// http://gad.qq.com/program/translateview/7194373
/// </summary>
public class ResourceLoadTypeComparer : IEqualityComparer<RedDotUnit>
{
    public bool Equals(RedDotUnit x, RedDotUnit y)
    {
        return x == y;
    }

    public int GetHashCode(RedDotUnit x)
    {
        return (int)x;
    }
}