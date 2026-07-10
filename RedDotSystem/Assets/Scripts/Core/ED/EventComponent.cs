/*
 * Description:             EventComponent.cs
 * Author:                  TONYTANG
 * Create Date:             2026/04/06
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// EventComponent.cs
/// 事件组件(用于组合的方式快速支持事件功能
/// </summary>
public class EventComponent : IRecycle
{
    /// <summary>
    /// 事件响应回调映射map
    /// Key为事件Id，Value为事件响应回调
    /// </summary>
    private Dictionary<EventId, Action<BaseEvent>> mEventMap;

    public EventComponent()
    {
        mEventMap = new Dictionary<EventId, Action<BaseEvent>>();
    }

    /// <summary>
    /// 出池
    /// </summary>
    public void OnCreate()
    {
        ResetDatas();
    }

    /// <summary>
    /// 入池
    /// </summary>
    public void OnDispose()
    {
        ResetDatas();
    }

    /// <summary>
    /// 重置数据
    /// </summary>
    protected virtual void ResetDatas()
    {
        mEventMap.Clear();
    }

    /// <summary>
    /// 清理操作
    /// </summary>
    public void Clear()
    {
        RemoveAllEvents();
    }

    /// <summary>
    /// 清除所有事件
    /// </summary>
    public void RemoveAllEvents()
    {
        var eventIDs = mEventMap.Keys.ToList();
        foreach (var eventID in eventIDs)
        {
            RemoveEvent(eventID, mEventMap[eventID]);
        }
    }
    
    /// <summary>
    /// 添加事件监听回调
    /// </summary>
    /// <param name="eventID">事件Id</param>
    /// <param name="eventCB">事件回调</param>
    public bool AddEvent(EventId eventID, Action<BaseEvent> eventCB)
    {
        var result = EventManager.Singleton.AddEvent(eventID, eventCB);
        if(!result)
        {
            return result;
        }
        if(mEventMap.ContainsKey(eventID))
        {
            mEventMap[eventID] += eventCB;
        }
        else
        {
            mEventMap.Add(eventID, eventCB);
        }
        return result;
    }

    /// <summary>
    /// 移除事件监听回调
    /// </summary>
    /// <param name="eventID">事件Id</param>
    /// <param name="eventCB">事件回调</param>
    public bool RemoveEvent(EventId eventID, Action<BaseEvent> eventCB)
    {
        var result = EventManager.Singleton.RemoveEvent(eventID, eventCB);
        if(!result)
        {
            return result;
        }
        if (mEventMap.ContainsKey(eventID))
        {
            mEventMap[eventID] -= eventCB;
            if (mEventMap[eventID] == null)
            {
                mEventMap.Remove(eventID);
            }
            return true;
        }
        Debug.LogError(string.Format("不包含EventId:{0}的监听!",eventID));
        return false;
    }

    /// <summary>
    /// 是否有监听特定事件
    /// </summary>
    /// <param name="eventID">事件Id</param>
    /// <returns></returns>
    public bool HasEvent(EventId eventID)
    {
        return mEventMap.ContainsKey(eventID);
    }
}