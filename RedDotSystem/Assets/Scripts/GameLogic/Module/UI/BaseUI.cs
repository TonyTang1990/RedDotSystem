/*
 * Description:             BaseUI.cs
 * Author:                  TONYTANG
 * Create Date:             2026/07/09
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// BaseUI.cs
/// 窗口基类
/// </summary>
public abstract class BaseUI : MonoBehaviour
{
    /// <summary>
    /// 窗口是否打开
    /// </summary>
    public bool IsOpen
    {
        get;
        private set;
    }

    /// <summary>
    /// 事件组件
    /// </summary>
    protected EventComponent mEventComponent = new EventComponent();

    /// <summary>
    /// 打开窗口
    /// </summary>
    /// <param name="args"></param>
    public virtual void Open(params object[] args)
    {
        if(IsOpen)
        {
            Debug.LogError($"窗口:{gameObject.name}已经打开，不能重复打开！");
            return;
        }
        IsOpen = true;
        gameObject.SetActive(true);
        InitDatas(args);
        RemoveAllListeners();
        AddAllListeners();
        AddAllEvents();
        UnbindAllRedDotNames();
        BindAllRedDotNames();
        OnOpen();
    }

    /// <summary>
    /// 初始化数据
    /// </summary>
    /// <param name="args"></param>
    protected virtual void InitDatas(params object[] args)
    {
        
    }

    /// <summary>
    /// 添加所有Listener
    /// </summary>
    protected virtual void AddAllListeners()
    {
        
    }

    /// <summary>
    /// 添加所有事件监听
    /// </summary>
    protected virtual void AddAllEvents()
    {
        
    }

    /// <summary>
    /// 绑定所有红点名
    /// </summary>
    protected virtual void BindAllRedDotNames()
    {

    }

    /// <summary>
    /// 响应打开窗口
    /// </summary>
    protected virtual void OnOpen()
    {
        
    }

    /// <summary>
    /// 关闭窗口
    /// </summary>
    public virtual void Close()
    {
        if(!IsOpen)
        {
            Debug.LogError($"窗口:{gameObject.name}已经关闭，不能重复关闭！");
            return;
        }
        IsOpen = false;
        gameObject.SetActive(false);
        RemoveAllListeners();
        RemoveAllEvents();
        UnbindAllRedDotNames();
        OnClose();
    }

    /// <summary>
    /// 移除所有Listener
    /// </summary>
    protected virtual void RemoveAllListeners()
    {
        
    }

    /// <summary>
    /// 移除所有事件
    /// </summary>
    protected virtual void RemoveAllEvents()
    {
        mEventComponent.RemoveAllEvents();
    }

    /// <summary>
    /// 解绑所有红点名
    /// </summary>
    protected virtual void UnbindAllRedDotNames()
    {

    }

    /// <summary>
    /// 响应关闭窗口
    /// </summary>
    protected virtual void OnClose()
    {
        
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
}