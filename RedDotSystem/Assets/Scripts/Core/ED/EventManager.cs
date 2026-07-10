using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// EventManager.cs
/// 全局事件分发管理类
/// </summary>
public class EventManager : SingletonTemplate<EventManager>
{
    /// <summary>
    /// 事件响应回调映射map
    /// Key为事件Id，Value为事件响应回调
    /// </summary>
    private Dictionary<EventId, Action<BaseEvent>> mEventMap;

    public EventManager()
    {
        mEventMap = new Dictionary<EventId, Action<BaseEvent>>();
    }

    /// <summary>
    /// 添加事件监听回调
    /// </summary>
    /// <param name="eventID">事件Id</param>
    /// <param name="eventCB">事件回调</param>
    public bool AddEvent(EventId eventID, Action<BaseEvent> eventCB)
    {
        if(eventCB == null)
        {
            Debug.LogError(string.Format("添加EventId:{0}的事件回调不能为空!", eventID));
            return false;
        }
        if(mEventMap.ContainsKey(eventID))
        {
            mEventMap[eventID] += eventCB;
        }
        else
        {
            mEventMap.Add(eventID, eventCB);
        }
        return true;
    }

    /// <summary>
    /// 移除事件监听回调
    /// </summary>
    /// <param name="eventID">事件Id</param>
    /// <param name="eventCB">事件回调</param>
    public bool RemoveEvent(EventId eventID, Action<BaseEvent> eventCB)
    {
        if (eventCB == null)
        {
            Debug.LogError(string.Format("移除EventId:{0}的事件回调不能为空!", eventID));
            return false;
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

    /// <summary>
    /// 分发特定事件
    /// </summary>
    /// <param name="eventID"></param>
    /// <param name="eventParams"></param>
    public void DispatchEvent(EventId eventID, params object[] eventParams)
    {
        if(!mEventMap.ContainsKey(eventID))
        {
            return;
        }
        var baseEvent = ObjectPool.Singleton.pop<BaseEvent>();
        baseEvent.Init(eventID, eventParams);
        mEventMap[eventID].Invoke(baseEvent);
        ObjectPool.Singleton.push<BaseEvent>(baseEvent);
    }
}
