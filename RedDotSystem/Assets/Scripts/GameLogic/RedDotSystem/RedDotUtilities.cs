/*
 * Description:             RedDotUtilities.cs
 * Author:                  TONYTANG
 * Create Date:             2022/08/14
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// RedDotUtilities.cs
/// 红点工具类
/// </summary>
public static class RedDotUtilities
{
    #region 红点辅助方法部分
    /// <summary>
    /// 获取指定红点数量和类型的文本显示
    /// Note:
    /// 红点显示类型优先级:
    /// 新 > 纯数字 > 纯红点
    /// </summary>
    /// <param name="result"></param>
    /// <param name="redDotType"></param>
    /// <returns></returns>
    public static string GetRedDotResultText(int result, RedDotType redDotType)
    {
        if(result <= 0)
        {
            return string.Empty;
        }
        var redDotText = string.Empty;
        if((redDotType & RedDotType.NEW) != RedDotType.NONE)
        {
            redDotText = "新";
        }
        else if((redDotType & RedDotType.NUMBER) != RedDotType.NONE)
        {
            redDotText = result.ToString();
        }
        return redDotText;
    }
    #endregion

    #region 红点数据以及逻辑运算部分
    #region 主界面部分
    /// <summary>
    /// 计算主界面动态新功能1解锁
    /// </summary>
    /// <returns></returns>
    public static int CaculateNewFunc1()
    {
        return GameModel.Singleton.NewFunc1 ? 1 : 0;
    }

    /// <summary>
    /// 计算主界面动态新功能2解锁
    /// </summary>
    /// <returns></returns>
    public static int CaculateNewFunc2()
    {
        return GameModel.Singleton.NewFunc2 ? 1 : 0;
    }
    #endregion

    #region 背包界面部分
    /// <summary>
    /// 计算新道具数
    /// </summary>
    /// <returns></returns>
    public static int CaculateNewItemNum()
    {
        return GameModel.Singleton.NewItemNum;
    }

    /// <summary>
    /// 计算新资源数
    /// </summary>
    /// <returns></returns>
    public static int CaculateNewResourceNum()
    {
        return GameModel.Singleton.NewResourceNum;
    }

    /// <summary>
    /// 计算新装备数
    /// </summary>
    /// <returns></returns>
    public static int CaculateNewEquipNum()
    {
        return GameModel.Singleton.NewEquipNum;
    }
    #endregion

    #region 邮件界面部分
    /// <summary>
    /// 计算新公共邮件数
    /// </summary>
    /// <returns></returns>
    public static int CaculateNewPublicMailNum()
    {
        return GameModel.Singleton.NewPublicMailNum;
    }

    /// <summary>
    /// 计算新战斗邮件数
    /// </summary>
    /// <returns></returns>
    public static int CaculateNewBattleMailNum()
    {
        return GameModel.Singleton.NewBattleMailNum;
    }

    /// <summary>
    /// 计算新其他邮件数
    /// </summary>
    /// <returns></returns>
    public static int CaculateNewOtherMailNum()
    {
        return GameModel.Singleton.NewOtherMailNum;
    }

    /// <summary>
    /// 计算公共邮件可领奖数
    /// </summary>
    /// <returns></returns>
    public static int CaculateNewPublicMailRewardNum()
    {
        return GameModel.Singleton.NewPublicMailRewardNum;
    }

    /// <summary>
    /// 计算战斗邮件可领奖数
    /// </summary>
    /// <returns></returns>
    public static int CaculateNewBattleMailRewardNum()
    {
        return GameModel.Singleton.NewBattleMailRewardNum;
    }
    #endregion

    #region 装备界面部分
    /// <summary>
    /// 计算可穿戴装备数
    /// </summary>
    /// <returns></returns>
    public static int CaculateWearableEquipNum()
    {
        return GameModel.Singleton.WearableEquipNum;
    }

    /// <summary>
    /// 计算可升级装备数
    /// </summary>
    /// <returns></returns>
    public static int CaculateUpgradeableEquipNum()
    {
        return GameModel.Singleton.UpgradeableEquipNum;
    }
    #endregion
    #endregion
}