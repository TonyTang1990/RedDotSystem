/*
 * Description:             RedDotManager.cs
 * Author:                  TONYTANG
 * Create Date:             2022/08/14
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// RedDotManager.cs
/// 红点管理单例类
/// </summary>
public class RedDotManager : SingletonTemplate<RedDotManager>
{
    /// <summary>
    /// 标脏的红点运算单元Map<红点运算单元, 红点运算单元>
    /// </summary>
    private Dictionary<RedDotUnit, RedDotUnit> mDirtyRedDotUnitMap;

    /// <summary>
    /// 结果变化的红点名数据Map<红点名, 红点名>
    /// </summary>
    private Dictionary<string, string> mResultChangedRedDotNameMap;

    /// <summary>
    /// 已经计算过的红点运算单元和结果是否变化Map<红点运算单元，结果是否变化>
    /// Note:
    /// 优化单个红点运算单元重复计算的问题
    /// </summary>
    private Dictionary<RedDotUnit, bool> mCaculatedRedDotUnitResultChangeMap;

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
        mFramePassed = 0;
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        InitRedDotData();
    }

    /// <summary>
    /// 初始化红点数据
    /// </summary>
    private void InitRedDotData()
    {
        mDirtyRedDotUnitMap = new Dictionary<RedDotUnit, RedDotUnit>();
        mCaculatedRedDotUnitResultChangeMap = new Dictionary<RedDotUnit, bool>();
        mResultChangedRedDotNameMap = new Dictionary<string, string>();
    }

    /// <summary>
    /// 执行所有红点名运算单元运算
    /// Note:
    /// 进入游戏后获取完相关数据后触发一次,确保第一次运算结果缓存
    /// </summary>
    public void DoAllRedDotUnitCaculate()
    {
        var redDotUnitInfoMap = RedDotModel.Singleton.GetRedDotUnitInfoMap();
        foreach(var redDotUnitInfo in redDotUnitInfoMap)
        {
            DoRedDotUnitCaculate(redDotUnitInfo.Key);
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
            Debug.LogError($"找不到红点名:{redDotName}红点信息,绑定刷新失败!");
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
            Debug.LogError($"找不到红点名:{redDotName}红点信息,解绑定失败!");
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
    public void MarkRedDotUnitDirty(RedDotUnit redDotUnit)
    {
        if (!mDirtyRedDotUnitMap.ContainsKey(redDotUnit))
        {
            mDirtyRedDotUnitMap.Add(redDotUnit, redDotUnit);
        }
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
        if(mDirtyRedDotUnitMap.Count == 0)
        {
            return;
        }
        // 部分红点单元单纯是影响因素，不会决定最终红点数量
        // 所以这里要把标脏的红点单元影响到的红点名所有红点单元都触发计算看是否有相关结论变化
        // 有相关红点单元的结果变化的红点名才需要通知上层逻辑回调刷新
        mCaculatedRedDotUnitResultChangeMap.Clear();
        mResultChangedRedDotNameMap.Clear();
        foreach(var dirtyRedDotUnit in mDirtyRedDotUnitMap)
        {
            var realRedDotUnit = dirtyRedDotUnit.Key;
            var redDotNameList = RedDotModel.Singleton.GetRedDotNamesByUnit(realRedDotUnit);
            if(redDotNameList == null || redDotNameList.Count == 0)
            {
                continue;
            }
            foreach (var redDotName in redDotNameList)
            {
                var redDotUnitList = RedDotModel.Singleton.GetRedDotUnitsByName(redDotName);
                if(redDotUnitList == null || redDotUnitList.Count == 0)
                {
                    continue;
                }
                foreach(var redDotUnit in redDotUnitList)
                {
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
                    if(!mResultChangedRedDotNameMap.ContainsKey(redDotName))
                    {
                        mResultChangedRedDotNameMap.Add(redDotName, redDotName);
                    }
                }
            }
        }
        mDirtyRedDotUnitMap.Clear();
        // 通过每个红点所有影响的红点运算单元得出红点显示结论并通知更新
        foreach (var resultChangedRedDotName in mResultChangedRedDotNameMap)
        {
            TriggerRedDotNameUpdate(resultChangedRedDotName.Key);
        }
        mResultChangedRedDotNameMap.Clear();
    }

    /// <summary>
    /// 执行指定红点运算单元计算
    /// </summary>
    /// <param name="redDotUnit"></param>
    /// <returns></returns>
    private bool DoRedDotUnitCaculate(RedDotUnit redDotUnit)
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