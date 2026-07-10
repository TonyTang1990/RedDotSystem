/*
 * Description:             FuncRDInitializer.cs
 * Author:                  TONYTANG
 * Create Date:             2026/04/02
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// FuncRDInitializer.cs
/// 功能红点数据初始化器基类抽象
/// </summary>
public abstract class FuncRDInitializer
{
    /// <summary>
    /// 获取指定系统红点初始化器名字
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string GetInitializerName<T>() where T : FuncRDInitializer
    {
        return typeof(T).Name;
    }

    /// <summary>
    /// 获取指定系统红点初始化器实例对象名字
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string GetInitializerName<T>(T instance) where T : FuncRDInitializer
    {
        if(instance == null)
        {
            Debug.LogError($"功能红点初始化器实例对象为null，无法获取初始化器名字!");
            return string.Empty;
        }
        return instance.GetType().Name;
    }

    /// <summary>
    /// 是否已激活
    /// Note:
    /// 单个功能红点初始化器激活受限于解锁条件(IsUnlock())
    /// 但一旦子功能红点初始化器决定激活，依赖的红点初始化器必须优先强制激活(不受解锁条件限制)
    /// 每个功能红点初始化器只支持激活一次
    /// 功能红点初始化器支持取消激活，取消激活时优先反激活最里层功能红点初始化器
    /// 所属功能的动态红点激活和取消由上层逻辑自行处理，框架层只处理静态功能红点的激活和取消激活
    /// </summary>
    public bool IsActive
    {
        get;
        protected set;
    }

    /// <summary>
    /// 依赖的功能红点初始化器
    /// Note:
    /// 设计上不允许多依赖，把每一层都拆分好，一般只会依赖一层
    /// </summary>
    public FuncRDInitializer DependencyInitializer
    {
        get;
        protected set;
    }

    /// <summary>
    /// 嵌套的功能红点初始化器Map[红点初始化名字, 功能红点初始化器实例]
    /// </summary>
    public Dictionary<string, FuncRDInitializer> NestedInitializerMap
    {
        get;
        private set;
    } = new Dictionary<string, FuncRDInitializer>();

    /// <summary>
    /// 红点数据Map[红点数据名字, 红点数据]
    /// </summary>
    protected Dictionary<string, RedDotInfo> mRedDotInfoMap = new Dictionary<string, RedDotInfo>();

    /// <summary>
    /// 事件组件
    /// </summary>
    protected EventComponent mEventComponent = new EventComponent();

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="dependencyInitializer">依赖的功能红点初始化器</param>
    public virtual void Init(FuncRDInitializer dependencyInitializer = null)
    {
        Debug.Log($"功能红点初始化器:{GetInitializerName(this)}初始化!");
        DependencyInitializer = dependencyInitializer;
        // 优先激活自身红点初始化器
        // 避免里层红点初始化器判定依赖初始化器时，依赖初始化器还未初始化问题
        if(IsUnlock())
        {
            Active();
        }
        InitNestedInitializers();
        AddAllEvents();
    }

    /// <summary>
    /// 释放
    /// </summary>
    public virtual void Dispose()
    {
        Debug.Log($"功能红点初始化器:{GetInitializerName(this)}释放!");
        RemoveAllEvents();
        // 优先清理里层红点初始化器
        DestroyAllNestedInitializer();
        RemoveAllRedDotInfos();
        DependencyInitializer = null;
    }

#region 事件相关
    /// <summary>
    /// 添加所有事件
    /// </summary>
    protected virtual void AddAllEvents()
    {
        
    }

    /// <summary>
    /// 清除所有事件
    /// </summary>
    protected virtual void RemoveAllEvents()
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

    /// <summary>
    /// 初始化嵌套的功能红点初始化器
    /// </summary>
    protected virtual void InitNestedInitializers()
    {
        
    }

    /// <summary>
    /// 销毁所有已创建的嵌套功能红点初始化器
    /// </summary>
    protected void DestroyAllNestedInitializer()
    {
        var initializerNames = NestedInitializerMap.Keys.ToList();
        foreach(var initializerName in initializerNames)
        {
            var funcRedDotInitializer = NestedInitializerMap[initializerName];
            RedDotInitializer.Singleton.DestroyInitializer(funcRedDotInitializer);
            NestedInitializerMap.Remove(initializerName);
        }
    }

    /// <summary>
    /// 功能红点初始化器是否解锁
    /// </summary>
    /// <returns></returns>
    protected virtual bool IsUnlock()
    {
        // 为未来做动态按需创建做准备，目前设想如下：
        // 只有依赖的数据能尽量统一到一个独立功能系统里时才能避免过多逻辑数据耦合
        // 比如做一个功能解锁系统，那么RedDotInitializer只需要在功能解锁系统数据初始化完成后初始化即可
        // 如果一开始特定功能红点初始化器没初始化，可以通过统一监听功能解锁事件的方式去触发对应初始化
        // 比如:
        // 功能解锁系统.Init()(含消息数据请求完成)
        // RedDotInitializer.Singleton.Init()
        // ******(其他功能系统初始化)
        // RedDotModel.Singleton.Init()
        return true;
    }

    /// <summary>
    /// 执行功能红点初始化器激活
    /// </summary>
    public void Active()
    {
        if(IsActive)
        {
            Debug.LogError($"功能红点初始化器:{GetInitializerName(this)}已激活，不允许重复激活!");
            return;
        }
        Debug.Log($"功能红点初始化器:{GetInitializerName(this)}激活!");
        IsActive = true;
        // 优先激活依赖的红点初始化器，避免里层红点初始化时，顶层红点信息还未初始化
        ActiveDependencyInitializers();
        InitRedDotInfos();
    }

    /// <summary>
    /// 取消功能红点初始化器激活
    /// </summary>
    public void Inactive()
    {
        if(!IsActive)
        {
            Debug.LogError($"功能红点初始化器:{GetInitializerName(this)}未激活，不允许取消激活!");
            return;
        }
        Debug.Log($"功能红点初始化器:{GetInitializerName(this)}取消激活!");
        IsActive = false;
        // 优先取消激活里层功能红点初始化器
        InactiveNestedInitializers();
        RemoveAllRedDotInfos();
    }

    /// <summary>
    /// 激活依赖的红点初始化器
    /// </summary>
    protected void ActiveDependencyInitializers()
    {
        if(DependencyInitializer != null && !DependencyInitializer.IsActive)
        {
            DependencyInitializer.Active();
        }
    }

    /// <summary>
    /// 取消嵌套的红点初始化器激活
    /// </summary>

    protected void InactiveNestedInitializers()
    {
        foreach(var funcRedDotInitializer in NestedInitializerMap.Values)
        {
            if(funcRedDotInitializer.IsActive)
            {
                funcRedDotInitializer.Inactive();
            }
        }
    }

    /// <summary>
    /// 初始化功能红点数据
    /// </summary>
    protected abstract void InitRedDotInfos();

    /// <summary>
    /// 清除红点数据
    /// </summary>
    protected void RemoveAllRedDotInfos()
    {
        // 确保从里层往顶层清理
        var redDotInfos = mRedDotInfoMap.Values.ToList();
        redDotInfos.Sort(SortRedDotInfo);
        foreach(var redDotInfo in redDotInfos)
        {
            var redDotName = redDotInfo.RedDotName;
            RemoveRedDotInfo(redDotName);
        }
    }
    
    /// <summary>
    /// 排序红点信息(深度大的在前面，方便从里层往顶层清理节点数据)
    /// </summary>
    /// <param name="redDotInfo1"></param>
    /// <param name="redDotInfo2"></param>
    /// <returns></returns>
    private int SortRedDotInfo(RedDotInfo redDotInfo1, RedDotInfo redDotInfo2)
    {
        if(redDotInfo1.RedDotNameDepth != redDotInfo2.RedDotNameDepth)
        {
            return redDotInfo2.RedDotNameDepth.CompareTo(redDotInfo1.RedDotNameDepth);
        }
        return redDotInfo2.RedDotName.CompareTo(redDotInfo1.RedDotName);
    }

    /// <summary>
    /// 添加嵌套功能红点初始化器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="initializer"></param>
    /// <returns></returns>
    protected bool AddNestedInitializer<T>(T initializer) where T : FuncRDInitializer, new()
    {
        var initializerName = GetInitializerName(initializer);
        if(HasNestedInitializer(initializer))
        {
            Debug.LogError($"嵌套功能红点初始化器名:{initializerName}的已添加，请勿重复添加!");
            return false;
        }
        NestedInitializerMap.Add(initializerName, initializer);
        return true;
    }
    /// <summary>
    /// 是否已创建指定类型的嵌套功能红点初始化器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    protected bool HasNestedInitializer<T>(T instance) where T : FuncRDInitializer
    {
        var initializerName = GetInitializerName(instance);
        return HasNestedInitializer(initializerName);
    }

    /// <summary>
    /// 是否已创建指定类型的嵌套功能红点初始化器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    protected bool HasNestedInitializer(string initializerName)
    {
        return NestedInitializerMap.ContainsKey(initializerName);
    }

    /// <summary>
    /// 创建指定嵌套功能红点初始化器
    /// </summary>
    protected (bool, T) CreateNestedInitializer<T>() where T : FuncRDInitializer, new()
    {
        (var result, var redDotInitializer) = RedDotInitializer.Singleton.CreateInitializer<T>(this);
        if(!result)
        {
            Debug.LogError($"功能红点初始化器:{GetInitializerName(this)}创建嵌套功能红点初始化器:{GetInitializerName<T>()}失败!");
            return (false, null);
        }
        AddNestedInitializer<T>(redDotInitializer);
        return (true, redDotInitializer);
    }
    
    /// <summary>
    /// 添加指定红点信息(不带红点处理器)
    /// 没有独立红点单元信息的红点无需构造红点处理器
    /// </summary>
    /// <param name="redDotName"></param>
    /// <param name="redDotDes"></param>
    /// <returns></returns>
    protected (bool, RedDotInfo) AddRedDotInfo(string redDotName, string redDotDes)
    {
        if (mRedDotInfoMap.TryGetValue(redDotName, out var existRedDotInfo))
        {
            Debug.LogError($"功能红点初始化器:{GetInitializerName(this)}已添加红点名:{redDotName}的红点信息,请勿重复添加,添加红点信息失败!");
            return (false, existRedDotInfo);
        }
        var (result, redDotInfo) = RedDotModel.Singleton.AddRedDotInfo(redDotName, redDotDes);
        if(!result)
        {
            Debug.LogError($"全局已添加红点名:{redDotName}的红点信息,功能红点初始化器:{GetInitializerName(this)}里请勿重复添加,添加红点信息失败!");
            return (false, redDotInfo);
        }
        mRedDotInfoMap.Add(redDotName, redDotInfo);
        return (result, redDotInfo);
    }

    /// <summary>
    /// 添加指定红点信息(带红点处理器)
    /// 有独立红点单元信息的红点需要构造红点处理器
    /// </summary>
    /// <param name="redDotName"></param>
    /// <param name="redDotDes"></param>
    /// <returns></returns>
    protected (bool, RedDotInfo) AddRedDotInfo<T>(string redDotName, string redDotDes) where T : RedDotHandler, new()
    {
        if (mRedDotInfoMap.TryGetValue(redDotName, out var existRedDotInfo))
        {
            Debug.LogError($"功能红点初始化器:{GetInitializerName(this)}已添加红点名:{redDotName}的红点信息,请勿重复添加,添加红点信息失败!");
            return (false, existRedDotInfo);
        }
        var (result, redDotInfo) = RedDotModel.Singleton.AddRedDotInfo<T>(redDotName, redDotDes);
        if(!result)
        {
            Debug.LogError($"全局已添加红点名:{redDotName}的红点信息,功能红点初始化器:{GetInitializerName(this)}里请勿重复添加,添加红点信息失败!");
            return (false, redDotInfo);
        }
        mRedDotInfoMap.Add(redDotName, redDotInfo);
        return (result, redDotInfo);
    }

    /// <summary>
    /// 创建指定动态红点信息(带红点处理器，带动态参数)
    /// 有独立红点单元信息的红点需要构造红点处理器
    /// </summary>
    /// <param name="redDotName"></param>
    /// <param name="redDotDes"></param>
    /// <param name="dynamicData"></param>
    /// <returns></returns>
    protected (bool, RedDotInfo) AddDynamicRedDotInfo<T, W>(string redDotName, string redDotDes, W dynamicData) where T : DynamicRedDotHandler<W>, new()
    {
        if (mRedDotInfoMap.TryGetValue(redDotName, out var existRedDotInfo))
        {
            Debug.LogError($"功能红点初始化器:{GetInitializerName(this)}已添加红点名:{redDotName}的红点信息,请勿重复添加,添加动态红点信息失败!");
            return (false, existRedDotInfo);
        }
        var (result, redDotInfo) = RedDotModel.Singleton.AddDynamicRedDotInfo<T, W>(redDotName, redDotDes, dynamicData);
        if(!result)
        {
            Debug.LogError($"全局已添加红点名:{redDotName}的红点信息,功能红点初始化器:{GetInitializerName(this)}里请勿重复添加,添加动态红点信息失败!");
            return (false, redDotInfo);
        }
        mRedDotInfoMap.Add(redDotName, redDotInfo);
        return (result, redDotInfo);
    }

    /// <summary>
    /// 移除指定红点信息
    /// </summary>
    /// <param name="redDotName"></param>
    /// <returns></returns>
    protected bool RemoveRedDotInfo(string redDotName)
    {
        if(!mRedDotInfoMap.TryGetValue(redDotName, out var redDotInfo))
        {
            Debug.LogError($"功能红点初始化器:{GetInitializerName(this)}未添加红点名:{redDotName}的红点信息,无法移除!");
            return false;
        }
        mRedDotInfoMap.Remove(redDotName);
        RedDotModel.Singleton.RemoveRedDotInfo(redDotName);
        return true;
    }
}