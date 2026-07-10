/*
 * Description:             RedDotHandler.cs
 * Author:                  TONYTANG
 * Create Date:             2026/04/03
 */

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// RedDotHandler.cs
/// 红点处理器基类抽象
/// 主要负责维护单个红点相关的逻辑和数据(比如红点单元初始化，绑定，解绑定，事件监听添加，解除事件监听和标脏等)
/// </summary>
public abstract class RedDotHandler : IRecycle
{
    /// <summary>
    /// 绑定的红点信息
    /// </summary>
    public RedDotInfo BindRedDotInfo
    {
        get;
        private set;
    }

    /// <summary>
    /// 此处理器添加绑定的红点单元Map[红点单元名字, 红点单元名字]
    /// </summary>
    protected Dictionary<string, string> mBindRedDotUnitMap = new Dictionary<string, string>();

    /// <summary>
    /// 事件组件
    /// </summary>
    protected EventComponent mEventComponent = new EventComponent();

    public void OnCreate()
    {
        ResetDatas();
    }

    public void OnDispose()
    {
        ResetDatas();
    }

    /// <summary>
    /// 重置数据
    /// </summary>
    protected virtual void ResetDatas()
    {
        BindRedDotInfo = null;
        mBindRedDotUnitMap.Clear();
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="bindRedDotInfo">绑定的红点信息</param>
    public void Init(RedDotInfo bindRedDotInfo)
    {
        Debug.Assert(bindRedDotInfo != null, "不允许传空绑定红点信息，初始化红点处理器失败！");
        BindRedDotInfo = bindRedDotInfo;
        InitRedDotDatas();
        AddAllEvents();
    }

    /// <summary>
    /// 初始化红点数据
    /// </summary>
    protected abstract void InitRedDotDatas();

    /// <summary>
    /// 添加所有事件
    /// </summary>
    protected virtual void AddAllEvents()
    {
        
    }

    /// <summary>
    /// 清理
    /// </summary>
    public void Clear()
    {
        RemoveAllEvents();
        ClearRedDotDatas();
        ClearBindRedDotInfo();
    }

    /// <summary>
    /// 清除红点数据
    /// </summary>
    protected void ClearRedDotDatas()
    {
        var redDotUnits = mBindRedDotUnitMap.Keys.ToList();
        foreach(var redDotUnit in redDotUnits)
        {
            RemoveBindRedDotUnit(redDotUnit);
        }
    }

    /// <summary>
    /// 清除绑定红点信息
    /// </summary>
    protected void ClearBindRedDotInfo()
    {
        BindRedDotInfo = null;
    }

    /// <summary>
    /// 添加并绑定指定红点单元
    /// </summary>
    /// <param name="redDotUnit"></param>
    /// <param name="redDotUnitDes"></param>
    /// <param name="caculateFunc"></param>
    /// <param name="redDotType"></param>
    /// <param name="isRecusive">是否递归添加并绑定红点单元到父级红点处理器</param>
    /// <returns></returns>
    protected bool AddBindRedDotUnit(string redDotUnit, string redDotUnitDes,
                                     Func<int> caculateFunc, RedDotType redDotType = RedDotType.NUMBER,
                                     bool isRecusive = true)
    {
        var (result, redDotUnitInfo) = RedDotModel.Singleton.AddRedDotUnitInfo(redDotUnit, redDotUnitDes, caculateFunc, redDotType);
        if(!result)
        {
            Debug.LogError($"红点处理器绑定的红点单元:{redDotUnit}的红点单元信息已存在，添加并绑定红点单元失败!");
            return false;
        }
        mBindRedDotUnitMap.Add(redDotUnit, redDotUnit);
        BindRedDotInfo.AddRedDotUnit(redDotUnit, isRecusive);
        return true;
    }

    /// <summary>
    /// 移除并解绑定指定红点单元
    /// </summary>
    /// <param name="redDotUnit"></param>
    /// <returns></returns>
    protected bool RemoveBindRedDotUnit(string redDotUnit)
    {
        if(!mBindRedDotUnitMap.TryGetValue(redDotUnit, out var redDotUnitValue))
        {
            Debug.LogError($"绑定红点名:{BindRedDotInfo.RedDotName}的红点处理器未添加红点单元:{redDotUnit}绑定信息，移除并解绑定红点单元失败!");
            return false;
        }
        // 谁绑定，谁解绑
        mBindRedDotUnitMap.Remove(redDotUnit);
        BindRedDotInfo.RemoveRedDotUnit(redDotUnitValue);
        // 只有RedDotHandler才能明确是通过自身Handler添加到红点单元信息
        // 这里才能确保主动移除红点单元信息，其他上层直接添加的红点单元信息由上层自行处理移除情况
        RedDotModel.Singleton.RemoveRedDotUnitInfo(redDotUnitValue);
        return true;
    }

    /// <summary>
    /// 是否已经添加并绑定指定红点单元
    /// </summary>
    /// <param name="redDotUnit"></param>
    /// <returns></returns>
    protected bool HasBindRedDotUnit(string redDotUnit)
    {
        return mBindRedDotUnitMap.ContainsKey(redDotUnit);
    }

    #region 事件相关
    /// <summary>
    /// 清除所有事件
    /// </summary>
    protected void RemoveAllEvents()
    {
        mEventComponent.RemoveAllEvents();
    }
    
    /// <summary>
    /// 添加事件监听回调
    /// </summary>
    /// <param name="eventID">事件Id</param>
    /// <param name="eventCB">事件回调</param>
    public bool AddEvent(EventId eventID, Action<BaseEvent> eventCB)
    {
        return mEventComponent.AddEvent(eventID, eventCB);
    }

    /// <summary>
    /// 移除事件监听回调
    /// </summary>
    /// <param name="eventID">事件Id</param>
    /// <param name="eventCB">事件回调</param>
    public bool RemoveEvent(EventId eventID, Action<BaseEvent> eventCB)
    {
        return mEventComponent.RemoveEvent(eventID, eventCB);
    }

    /// <summary>
    /// 是否有监听特定事件
    /// </summary>
    /// <param name="eventID">事件Id</param>
    /// <returns></returns>
    public bool HasEvent(EventId eventID)
    {
        return mEventComponent.HasEvent(eventID);
    }
    #endregion
}