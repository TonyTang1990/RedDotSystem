/*
 * Description:             LevelModel.cs
 * Author:                  TONYTANG
 * Create Date:             2026/04/03
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// LevelModel.cs
/// 关卡数据层单例类
/// </summary>
public class LevelModel : BaseModel<LevelModel>
{
    /// <summary>
    /// 最大关卡Id
    /// </summary>
    public const int MaxLevelId = 5;

    /// <summary>
    /// 关卡可升级数量Map<关卡id, 可升级数量>
    /// </summary>
    public Dictionary<int, int> LevelUpgradeNumMap
    {
        get;
        private set;
    } = new Dictionary<int, int>();

    /// <summary>
    /// 关卡可领奖数量Map<关卡id, 可领奖数量>
    /// </summary>
    public Dictionary<int, int> LevelRewardNumMap
    {
        get;
        private set;
    } = new Dictionary<int, int>();

    /// <summary>
    /// 响应初始化
    /// </summary>
    protected override void OnInit()
    {
        base.OnInit();
    }

    /// <summary>
    /// 响应释放
    /// </summary>
    protected override void OnDispose()
    {
        base.OnDispose();
        LevelUpgradeNumMap.Clear();
        LevelRewardNumMap.Clear();
    }

    /// <summary>
    /// 获取指定关卡id可升级数量
    /// </summary>
    /// <param name="levelId"></param>
    /// <returns></returns>
    public int GetLevelUpgradeNum(int levelId)
    {
        if (LevelUpgradeNumMap.TryGetValue(levelId, out var availableUpgradeNum))
        {
            return availableUpgradeNum;
        }
        return 0;
    }

    /// <summary>
    /// 设置指定关卡id可升级数量
    /// </summary>
    /// <param name="levelId"></param>
    /// <param name="upgradeNum"></param>
    /// <returns></returns>
    public void SetLevelUpgradeNum(int levelId, int upgradeNum)
    {
        var oldUpgradeNum = GetLevelUpgradeNum(levelId);
        if(upgradeNum == oldUpgradeNum)
        {
            return;
        }
        upgradeNum = upgradeNum >= 0 ? upgradeNum : 0;
        if(!LevelUpgradeNumMap.ContainsKey(levelId))
        {
            LevelUpgradeNumMap.Add(levelId, upgradeNum);
        }
        else
        {
            LevelUpgradeNumMap[levelId] = upgradeNum;
        }
        EventManager.Singleton.DispatchEvent(EventId.LevelUpgradeNumUpdate, levelId, upgradeNum);
    }

    /// <summary>
    /// 获取指定关卡id可领奖数量
    /// </summary>
    /// <param name="levelId"></param>
    /// <returns></returns>
    public int GetLevelRewardNum(int levelId)
    {
        if (LevelRewardNumMap.TryGetValue(levelId, out var availableRewardNum))
        {
            return availableRewardNum;
        }
        return 0;
    }

    /// <summary>
    /// 设置指定关卡id可领奖数量
    /// </summary>
    /// <param name="levelId"></param>
    /// <param name="rewardNum"></param>
    public void SetLevelRewardNum(int levelId, int rewardNum)
    {
        var oldRewardNum = GetLevelRewardNum(levelId);
        if (rewardNum == oldRewardNum)
        {
            return;
        }
        if (!LevelRewardNumMap.ContainsKey(levelId))
        {
            LevelRewardNumMap.Add(levelId, rewardNum);
        }
        else
        {
            LevelRewardNumMap[levelId] = rewardNum;
        }
        EventManager.Singleton.DispatchEvent(EventId.LevelRewardNumUpdate, levelId, rewardNum);
    }
}