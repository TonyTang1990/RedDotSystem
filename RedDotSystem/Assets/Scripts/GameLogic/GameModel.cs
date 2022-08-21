/*
 * Description:             GameModel.cs
 * Author:                  TONYTANG
 * Create Date:             2022/08/21
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GameModel.cs
/// 游戏数据层(用于测试红点数据保存和修改)
/// </summary>
public class GameModel : SingletonTemplate<GameModel>
{
    #region 主界面部分
    /// <summary>
    /// 主界面动态新功能1是否解锁
    /// </summary>
    public bool NewFunc1
    {
        get;
        private set;
    }

    /// <summary>
    /// 主界面动态新功能2是否解锁
    /// </summary>
    public bool NewFunc2
    {
        get;
        private set;
    }

    /// <summary>
    /// 设置主界面动态新功能1是否解锁
    /// </summary>
    /// <param name="newFunc1"></param>
    public void SetNewFunc1(bool newFunc1)
    {
        if(NewFunc1 != newFunc1)
        {
            NewFunc1 = newFunc1;
            RedDotManager.Singleton.MarkRedDotUnitDirty(RedDotUnit.NEW_FUNC1);
        }
    }

    /// <summary>
    /// 设置主界面动态新功能2是否解锁
    /// </summary>
    /// <param name="newFunc2"></param>
    public void SetNewFunc2(bool newFunc2)
    {
        if (NewFunc2 != newFunc2)
        {
            NewFunc2 = newFunc2;
            RedDotManager.Singleton.MarkRedDotUnitDirty(RedDotUnit.NEW_FUNC2);
        }
    }
    #endregion

    #region 背包界面部分
    /// <summary>
    /// 新道具数
    /// </summary>
    public int NewItemNum
    {
        get;
        private set;
    }

    /// <summary>
    /// 新资源数
    /// </summary>
    public int NewResourceNum
    {
        get;
        private set;
    }

    /// <summary>
    /// 新装备数
    /// </summary>
    public int NewEquipNum
    {
        get;
        private set;
    }

    /// <summary>
    /// 设置新道具数
    /// </summary>
    /// <param name="newItemNum"></param>
    public void SetNewItemNum(int newItemNum)
    {
        newItemNum = Mathf.Clamp(newItemNum, 0, newItemNum);
        if (NewItemNum != newItemNum)
        {
            NewItemNum = newItemNum;
            RedDotManager.Singleton.MarkRedDotUnitDirty(RedDotUnit.NEW_ITEM_NUM);
        }
    }

    /// <summary>
    /// 设置新资源数
    /// </summary>
    /// <param name="newResourceNum"></param>
    public void SetNewResourceNum(int newResourceNum)
    {
        newResourceNum = Mathf.Clamp(newResourceNum, 0, newResourceNum);
        if (NewResourceNum != newResourceNum)
        {
            NewResourceNum = newResourceNum;
            RedDotManager.Singleton.MarkRedDotUnitDirty(RedDotUnit.NEW_RESOURCE_NUM);
        }
    }

    /// <summary>
    /// 设置新装备数
    /// </summary>
    /// <param name="newEquipNum"></param>
    public void SetmNewEquipNum(int newEquipNum)
    {
        newEquipNum = Mathf.Clamp(newEquipNum, 0, newEquipNum);
        if (NewEquipNum != newEquipNum)
        {
            NewEquipNum = newEquipNum;
            RedDotManager.Singleton.MarkRedDotUnitDirty(RedDotUnit.NEW_EQUIP_NUM);
        }
    }
    #endregion

    #region 邮件界面部分
    /// <summary>
    /// 新公共邮件数
    /// </summary>
    public int NewPublicMailNum
    {
        get;
        private set;
    }

    /// <summary>
    /// 新战斗邮件数
    /// </summary>
    public int NewBattleMailNum
    {
        get;
        private set;
    }

    /// <summary>
    /// 新其他邮件数
    /// </summary>
    public int NewOtherMailNum
    {
        get;
        private set;
    }

    /// <summary>
    /// 公共邮件可领奖数
    /// </summary>
    public int NewPublicMailRewardNum
    {
        get;
        private set;
    }

    /// <summary>
    /// 战斗邮件可领奖数
    /// </summary>
    public int NewBattleMailRewardNum
    {
        get;
        private set;
    }

    /// <summary>
    /// 设置新公共邮件数
    /// </summary>
    /// <param name="newPublicMailNum"></param>
    public void SetNewPublicMailNum(int newPublicMailNum)
    {
        newPublicMailNum = Mathf.Clamp(newPublicMailNum, 0, newPublicMailNum);
        if (NewPublicMailNum != newPublicMailNum)
        {
            NewPublicMailNum = newPublicMailNum;
            RedDotManager.Singleton.MarkRedDotUnitDirty(RedDotUnit.NEW_PUBLIC_MAIL_NUM);
        }
    }

    /// <summary>
    /// 设置新战斗邮件数
    /// </summary>
    /// <param name="newBattleMailNum"></param>
    public void SetNewBattleMailNum(int newBattleMailNum)
    {
        newBattleMailNum = Mathf.Clamp(newBattleMailNum, 0, newBattleMailNum);
        if (NewBattleMailNum != newBattleMailNum)
        {
            NewBattleMailNum = newBattleMailNum;
            RedDotManager.Singleton.MarkRedDotUnitDirty(RedDotUnit.NEW_BATTLE_MAIL_NUM);
        }
    }

    /// <summary>
    /// 设置新其他邮件数
    /// </summary>
    /// <param name="newOtherMailNum"></param>
    public void SetmNewOtherMailNum(int newOtherMailNum)
    {
        newOtherMailNum = Mathf.Clamp(newOtherMailNum, 0, newOtherMailNum);
        if (NewOtherMailNum != newOtherMailNum)
        {
            NewOtherMailNum = newOtherMailNum;
            RedDotManager.Singleton.MarkRedDotUnitDirty(RedDotUnit.NEW_OTHER_MAIL_NUM);
        }
    }

    /// <summary>
    /// 设置公共邮件可领奖数
    /// </summary>
    /// <param name="publicMailRewardNum"></param>
    public void SetPublicMailRewardNum(int publicMailRewardNum)
    {
        publicMailRewardNum = Mathf.Clamp(publicMailRewardNum, 0, publicMailRewardNum);
        if (NewPublicMailRewardNum != publicMailRewardNum)
        {
            NewPublicMailRewardNum = publicMailRewardNum;
            RedDotManager.Singleton.MarkRedDotUnitDirty(RedDotUnit.PUBLIC_MAIL_REWARD_NUM);
        }
    }

    /// <summary>
    /// 设置战斗邮件可领奖数
    /// </summary>
    /// <param name="publicBattleRewardNum"></param>
    public void SetBattleMailRewardNum(int publicBattleRewardNum)
    {
        publicBattleRewardNum = Mathf.Clamp(publicBattleRewardNum, 0, publicBattleRewardNum);
        if (NewBattleMailRewardNum != publicBattleRewardNum)
        {
            NewBattleMailRewardNum = publicBattleRewardNum;
            RedDotManager.Singleton.MarkRedDotUnitDirty(RedDotUnit.BATTLE_MAIL_REWARD_NUM);
        }
    }
    #endregion

    #region 装备界面部分
    /// <summary>
    /// 可穿戴装备数
    /// </summary>
    public int WearableEquipNum
    {
        get;
        private set;
    }

    /// <summary>
    /// 可升级装备数
    /// </summary>
    public int UpgradeableEquipNum
    {
        get;
        private set;
    }

    /// <summary>
    /// 设置可穿戴装备数
    /// </summary>
    /// <param name="newWearableEquipNum"></param>
    public void SetWearableEquipNum(int newWearableEquipNum)
    {
        newWearableEquipNum = Mathf.Clamp(newWearableEquipNum, 0, newWearableEquipNum);
        if (WearableEquipNum != newWearableEquipNum)
        {
            WearableEquipNum = newWearableEquipNum;
            RedDotManager.Singleton.MarkRedDotUnitDirty(RedDotUnit.WEARABLE_EQUIP_NUM);
        }
    }

    /// <summary>
    /// 设置可升级装备数
    /// </summary>
    /// <param name="newUpgradeableEquipNum"></param>
    public void SetUpgradeableEquipNum(int newUpgradeableEquipNum)
    {
        newUpgradeableEquipNum = Mathf.Clamp(newUpgradeableEquipNum, 0, newUpgradeableEquipNum);
        if (UpgradeableEquipNum != newUpgradeableEquipNum)
        {
            UpgradeableEquipNum = newUpgradeableEquipNum;
            RedDotManager.Singleton.MarkRedDotUnitDirty(RedDotUnit.UPGRADEABLE_EQUIP_NUM);
        }
    }
    #endregion
}