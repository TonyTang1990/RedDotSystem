/*
 * Description:             RedDotManager.cs
 * Author:                  TONYTANG
 * Create Date:             2022/08/14
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 红点系统主要设计：
// 1. 采用前缀树数据结构，从红点命名上解决红点父子定义关联问题
// 2. 红点运算单元采用最小单元化定义，采用组合定义方式组装红点影响因素，从而实现高度自由的红点运算逻辑组装
// 3. 红点运算单元作为红点影响显示的最小单元，每一个都会对应逻辑层面的一个计算代码，从而实现和逻辑层关联实现自定义解锁和计算方式
// 4. 红点运算结果按红点运算单元为单位，采用标脏加延迟计算的方式避免重复运算和结果缓存
// 5. 红点运算单元支持多种显示类型定义(e.g. 1. 纯红点 2. 纯数字红点 3. 新红点 4. 混合红点等)，红点最终显示类型由所有影响他的红点运算单元计算结果组合而成(e.g. 红点运算单元1(新红点类型)+红点运算单元2(数字红点类型)=新红点类型)

// 静态红点支持设计:
// 1. 静态红点名和红点运算单元通过预定义在RedDotNames.cs和RedDotUnit.cs里定义

// 动态红点支持设计：
// 1. 动态红点通过自定义取名的方式定义动态红点名和动态红点运算单元名注册到RedDotModel里去实现动态红点逻辑支持

/// <summary>
/// RedDotManager.cs
/// 红点管理单例类
/// </summary>
public class RedDotManager : SingletonTemplate<RedDotManager>
{
    /// <summary>
    /// 单帧最大计算红点单元数量
    /// </summary>
    private const int CALCULATE_RED_DOT_UNIT_NUM_PER_FRAME = 30;

    /// <summary>
    /// 标脏的红点运算单元集合<红点运算单元>
    /// </summary>
    private HashSet<string> mDirtyRedDotUnitSet;

    /// <summary>
    /// 强制刷新红点名集合<红点名>
    /// </summary>
    private HashSet<string> mForceRefreshRedDotNameSet;

    /// <summary>
    /// 等待计算的红点运算单元集合
    /// Note:
    /// 避免触发红点单元计算时标脏红点单元报错问题
    /// </summary>
    private HashSet<string> mWaitCalculateRedDotUnitSet;

    /// <summary>
    /// 需要刷新的红点名数据集合<红点名>
    /// </summary>
    private HashSet<string> mNeedRefreshRedDotNameSet;

    /// <summary>
    /// 已经计算过的红点运算单元和结果是否变化Map<红点运算单元，结果是否变化>
    /// Note:
    /// 优化单个红点运算单元重复计算的问题
    /// </summary>
    private Dictionary<string, bool> mCaculatedRedDotUnitResultChangeMap;

    /// <summary>
    /// 标脏检测更新帧率
    /// </summary>
    private const int DIRTY_UPDATE_INTERVAL_FRAME = 10;

    /// <summary>
    /// 经历的帧数
    /// </summary>
    private int mFramePassed;

    public RedDotManager()
    {
        mDirtyRedDotUnitSet = new HashSet<string>();
        mForceRefreshRedDotNameSet = new HashSet<string>();
        mWaitCalculateRedDotUnitSet = new HashSet<string>();
        mCaculatedRedDotUnitResultChangeMap = new Dictionary<string, bool>();
        mNeedRefreshRedDotNameSet = new HashSet<string>();
        mFramePassed = 0;
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {

    }

    /// <summary>
    /// 释放
    /// </summary>
    public void Dispose()
    {
        mDirtyRedDotUnitSet.Clear();
        mForceRefreshRedDotNameSet.Clear();
        mWaitCalculateRedDotUnitSet.Clear();
        mNeedRefreshRedDotNameSet.Clear();
        mCaculatedRedDotUnitResultChangeMap.Clear();
        mFramePassed = 0;
    }

    /// <summary>
    /// 标脏所有红点名运算单元运算
    /// Note:
    /// 进入游戏后标脏一次所有,确保触发所有红点运算单元运算
    /// </summary>
    public void MarkAllRedDotUnitDirty()
    {
        var redDotUnitInfoMap = RedDotModel.Singleton.GetRedDotUnitInfoMap();
        foreach(var redDotUnitInfo in redDotUnitInfoMap)
        {
            MarkRedDotUnitDirty(redDotUnitInfo.Key);
        }
    }

    /// <summary>
    /// 触发红点名刷新回调
    /// </summary>
    /// <param name="redDotName"></param>
    public bool TriggerRedDotNameUpdate(string redDotName)
    {
        var redDotInfo = RedDotModel.Singleton.GetRedDotInfoByName(redDotName);
        if(redDotInfo == null)
        {
            Debug.LogError($"触发红点名:{redDotName}刷新回调失败!");
            return false;
        }
        var redDotNameResult = GetRedDotNameResult(redDotName);
        redDotInfo.TriggerUpdate(redDotNameResult.result, redDotNameResult.redDotType);
        return true;
    }

    /// <summary>
    /// 获取指定红点名的结果数据
    /// </summary>
    /// <param name="redDotName"></param>
    /// <returns></returns>
    public (int result, RedDotType redDotType) GetRedDotNameResult(string redDotName)
    {
        (int result, RedDotType redDotType) redDotNameResult = (0, RedDotType.NONE);
        var redDotUnitList = RedDotModel.Singleton.GetRedDotUnitsByName(redDotName);
        if (redDotUnitList != null)
        {
            var result = 0;
            var redDotType = RedDotType.NONE;
            foreach (var redDotUnit in redDotUnitList)
            {
                var redDotUnitResult = RedDotModel.Singleton.GetRedDotUnitResult(redDotUnit);
                if (redDotUnitResult > 0)
                {
                    var redDotType2 = RedDotModel.Singleton.GetRedDotUnitRedType(redDotUnit);
                    redDotType = redDotType | redDotType2;
                }
                result += redDotUnitResult;
            }
            redDotNameResult.result = result;
            redDotNameResult.redDotType = redDotType;
        }
        return redDotNameResult;
    }

    /// <summary>
    /// 绑定指定红点名刷新回调
    /// </summary>
    /// <param name="redDotName"></param>
    /// <param name="refreshDelegate"></param>
    /// <returns></returns>
    public void BindRedDotName(string redDotName, Action<string, int, RedDotType> refreshDelegate)
    {
        var redDotInfo = RedDotModel.Singleton.GetRedDotInfoByName(redDotName);
        if(redDotInfo == null)
        {
            //Debug.LogError($"找不到红点名:{redDotName}红点信息,绑定刷新失败!");
            return;
        }
        redDotInfo.Bind(refreshDelegate);
    }

    /// <summary>
    /// 解绑定指定红点名
    /// </summary>
    /// <param name="redDotName"></param>
    /// <param name="refreshDelegate"></param>
    /// <returns></returns>
    public void UnbindRedDotName(string redDotName, Action<string, int, RedDotType> refreshDelegate)
    {
        var redDotInfo = RedDotModel.Singleton.GetRedDotInfoByName(redDotName);
        if (redDotInfo == null)
        {
            //Debug.LogError($"找不到红点名:{redDotName}红点信息,解绑定失败!");
            return;
        }
        redDotInfo.UnBind(refreshDelegate);
    }

    /// <summary>
    /// 获取指定红点名结果
    /// </summary>
    /// <param name="redDotName"></param>
    /// <returns></returns>
    public int GetRedDotResult(string redDotName)
    {
        var redDotUnitList = RedDotModel.Singleton.GetRedDotUnitsByName(redDotName);
        if(redDotUnitList == null)
        {
            return 0;
        }
        var result = 0;
        foreach(var redDotUnit in redDotUnitList)
        {
            var redDotUnitResult = RedDotModel.Singleton.GetRedDotUnitResult(redDotUnit);
            result += redDotUnitResult;
        }
        return result;
    }

    /// <summary>
    /// 更新
    /// </summary>
    public void Update()
    {
        mFramePassed++;
        if(mFramePassed >= DIRTY_UPDATE_INTERVAL_FRAME)
        {
            CheckDirtyRedDotUnit();
            mFramePassed = 0;
        }
    }

    /// <summary>
    /// 标记指定红点名脏
    /// </summary>
    /// <param name="redDotName"></param>
    public void MarkRedDotNameDirty(string redDotName)
    {
        var redDotUnitList = RedDotModel.Singleton.GetRedDotUnitsByName(redDotName);
        if (redDotUnitList != null)
        {
            foreach (var redDotUnit in redDotUnitList)
            {
                MarkRedDotUnitDirty(redDotUnit);
            }
        }
    }

    /// <summary>
    /// 标记指定红点运算类型脏
    /// </summary>
    /// <param name="redDotUnit"></param>
    public void MarkRedDotUnitDirty(string redDotUnit)
    {
        mDirtyRedDotUnitSet.Add(redDotUnit);
    }

    /// <summary>
    /// 强制刷新指定红点名
    /// </summary>
    /// <param name="redDotName"></param>
    public void ForceRefreshRedDotName(string redDotName)
    {
        mForceRefreshRedDotNameSet.Add(redDotName);
    }

    /// <summary>
    /// 检查标脏红点运算单元
    /// </summary>
    private void CheckDirtyRedDotUnit()
    {
        if(!RedDotModel.Singleton.IsInitCompelte)
        {
            return;
        }
        if(mDirtyRedDotUnitSet.Count == 0 && mForceRefreshRedDotNameSet.Count == 0)
        {
            return;
        }
        // 限制每次最大计算红点单元数量，避免单帧计算过多导致过卡问题
        // 只计算标脏的红点单元，其他单元不重复计算
        // 通过从缓存结果直接获取结果避免过多的不必要红点单元运算
        // 红点单元的结果变化才通知相关红点名刷新
        var dirtyRedDotUnitNum = 0;
        mWaitCalculateRedDotUnitSet.Clear();
        foreach(var dirtyRedDotUnit in mDirtyRedDotUnitSet)
        {
            if(dirtyRedDotUnitNum >= CALCULATE_RED_DOT_UNIT_NUM_PER_FRAME)
            {
                continue;
            }
            mWaitCalculateRedDotUnitSet.Add(dirtyRedDotUnit);
            dirtyRedDotUnitNum++;
        }

        foreach(var redDotUnit in mWaitCalculateRedDotUnitSet)
        {
            mDirtyRedDotUnitSet.Remove(redDotUnit);
        }

        mCaculatedRedDotUnitResultChangeMap.Clear();
        mNeedRefreshRedDotNameSet.Clear();
        foreach(var redDotUnit in mWaitCalculateRedDotUnitSet)
        {
            // 逻辑层可能出现还未初始化但在标脏的红点单元的情况
            // 这种情况不需要触发红点单元运算，直接跳过
            if(!RedDotModel.Singleton.ExistsRedDotUnitInfo(redDotUnit))
            {
                continue;
            }
            bool resultChange = false;
            if(!mCaculatedRedDotUnitResultChangeMap.TryGetValue(redDotUnit, out resultChange))
            {
                resultChange = DoRedDotUnitCaculate(redDotUnit);
                mCaculatedRedDotUnitResultChangeMap.Add(redDotUnit, resultChange);
            }
            if(!resultChange)
            {
                continue;
            }
            var redDotNameList = RedDotModel.Singleton.GetRedDotNamesByUnit(redDotUnit);
            if(redDotNameList == null || redDotNameList.Count == 0)
            {
                continue;
            }
            foreach(var redDotName in redDotNameList)
            {
                mNeedRefreshRedDotNameSet.Add(redDotName);
            }
        }

        foreach(var redDotName in mForceRefreshRedDotNameSet)
        {
            mNeedRefreshRedDotNameSet.Add(redDotName);
        }
        mForceRefreshRedDotNameSet.Clear();
        
        // 通知上层相关红点名结果变化，触发刷新回调
        foreach (var resultChangedRedDotName in mNeedRefreshRedDotNameSet)
        {
            // 在动态红点名移除的情况下是可能出现红点名需要刷新但不存在的情况
            if(!RedDotModel.Singleton.IsRedDotNameExist(resultChangedRedDotName))
            {
                continue;
            }
            TriggerRedDotNameUpdate(resultChangedRedDotName);
        }
        mNeedRefreshRedDotNameSet.Clear();
    }

    /// <summary>
    /// 执行指定红点运算单元计算
    /// </summary>
    /// <param name="redDotUnit"></param>
    /// <returns></returns>
    private bool DoRedDotUnitCaculate(string redDotUnit)
    {
        var preResult = RedDotModel.Singleton.GetRedDotUnitResult(redDotUnit);
        var redDotUnitFunc = RedDotModel.Singleton.GetRedDotUnitFunc(redDotUnit);
        var result = 0;
        if(redDotUnitFunc != null)
        {
            result = redDotUnitFunc();
        }
        else
        {
            //Debug.LogError($"红点运算单元:{redDotUnit.ToString()}未绑定有效计算方法!");
        }
        RedDotModel.Singleton.SetRedDotUnitResult(redDotUnit, result);
        return preResult != result;
    }
}