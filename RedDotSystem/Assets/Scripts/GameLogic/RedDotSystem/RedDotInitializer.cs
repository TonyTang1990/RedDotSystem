/*
 * Description:             RedDotInitializer.cs
 * Author:                  TONYTANG
 * Create Date:             2026/04/02
 */

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// RedDotInitializer.cs
/// 红点初始化器单例类
/// </summary>
public class RedDotInitializer : SingletonTemplate<RedDotInitializer>
{
    /// <summary>
    /// 是否初始化完成
    /// </summary>
    public bool IsInitComplete
    {
        get;
        private set;
    }

    /// <summary>
    /// 所有的功能红点初始化器Map[红点初始化名字, 功能红点初始化器实例]
    /// </summary>
    public Dictionary<string, FuncRDInitializer> AllFuncRedDotInitializerMap
    {
        get;
        private set;
    }

    /// <summary>
    /// 根功能红点初始化器
    /// </summary>
    public RootRDInitializer RootRDInitializer
    {
        get;
        private set;
    }

    public RedDotInitializer()
    {
        IsInitComplete = false;
        AllFuncRedDotInitializerMap = new Dictionary<string, FuncRDInitializer>();
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        if(IsInitComplete)
        {
            Debug.LogError($"红点初始化器已完成初始化，请勿重复初始化!");
            return; 
        }
        // 按照红点层级开始初始化红点初始化器
        InitRootRDIntializer();
        IsInitComplete = true;
    }

    /// <summary>
    /// 释放
    /// </summary>
    public void Dispose()
    {
        DestroyAllInitializer();
        IsInitComplete = false;
    }

    /// <summary>
    /// 创建指定功能红点初始化器
    /// Note:
    /// 外部功能请勿直接调用，统一走RootRDInitializer层开始一层一层嵌套创建功能红点数据
    /// <param name="dependencyInitializer">依赖的功能红点初始化器</param>
    /// </summary>
    public (bool, T) CreateInitializer<T>(FuncRDInitializer dependencyInitializer = null) where T : FuncRDInitializer, new()
    {
        var initializerName = FuncRDInitializer.GetInitializerName<T>();
        var initializer = GetInitializer<T>(initializerName);
        if(initializer != null)
        {
            Debug.LogError($"功能红点初始化器名:{initializerName}的实例对象已存在，无法重复创建!");
            return (false, initializer);
        }
        initializer = new T();
        AllFuncRedDotInitializerMap.Add(initializerName, initializer);
        initializer.Init(dependencyInitializer);
        return (true, initializer);
    }

    /// <summary>
    /// 销毁指定初始化器名的红点初始化器
    /// </summary>
    /// <param name="initializerName"></param>
    /// <returns></returns>
    public bool DestroyInitializer(string initializerName)
    {
        if(!AllFuncRedDotInitializerMap.TryGetValue(initializerName, out var funcRedDotInitializer))
        {
            Debug.LogError($"功能红点初始化器名:{initializerName}的未注册，无法销毁!");
            return false;            
        }
        AllFuncRedDotInitializerMap.Remove(initializerName);
        funcRedDotInitializer.Dispose();
        return true;
    }

    /// <summary>
    /// 销毁指定功能红点初始化器实例对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="initializer"></param>
    /// <returns></returns>
    public bool DestroyInitializer<T>(T initializer) where T : FuncRDInitializer
    {
        if(initializer == null)
        {
            Debug.LogError($"功能红点初始化器实例对象为null，无法销毁!");
            return false;            
        }
        var initializerName = FuncRDInitializer.GetInitializerName(initializer);
        var existingInitializer = GetInitializer<T>(initializerName);
        if(existingInitializer == null)
        {
            Debug.LogError($"功能红点初始化器实例对象:{initializer}的未注册，无法销毁!");
            return false;
        }
        if(existingInitializer != initializer)
        {
            Debug.LogError($"功能红点初始化器实例对象:{initializer}与已注册的实例对象:{existingInitializer}不匹配，无法销毁!");
            return false;
        }
        return DestroyInitializer(initializerName);
    }

    /// <summary>
    /// 激活指定类型的红点初始化器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    protected bool ActiveInitializer<T>() where T : FuncRDInitializer
    {
        var initializerName = FuncRDInitializer.GetInitializerName<T>();
        return ActiveInitializer(initializerName);
    }

    /// <summary>
    /// 激活指定功能红点初始化器
    /// </summary>
    /// <param name="initializerName"></param>
    /// <returns></returns>
    protected bool ActiveInitializer(string initializerName)
    {
        if(!AllFuncRedDotInitializerMap.TryGetValue(initializerName, out var funcRedDotInitializer))
        {
            Debug.LogError($"功能红点初始化器名:{initializerName}的未注册，无法激活!");
            return false;
        
        }
        if(funcRedDotInitializer.IsActive)
        {
            Debug.LogWarning($"功能红点初始化器名:{initializerName}的实例对象已激活，请勿重复激活!");
            return false;
        }
        funcRedDotInitializer.Active();
        return true;
    }

    /// <summary>
    /// 取消激活指定类型的红点初始化器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    protected bool InactiveInitializer<T>() where T : FuncRDInitializer
    {
        var initializerName = FuncRDInitializer.GetInitializerName<T>();
        return InactiveInitializer(initializerName);
    }
    
    /// <summary>
    /// 取消激活指定功能红点初始化器
    /// </summary>
    /// <param name="initializerName"></param>
    /// <returns></returns>
    protected bool InactiveInitializer(string initializerName)
    {
        if(!AllFuncRedDotInitializerMap.TryGetValue(initializerName, out var funcRedDotInitializer))
        {
            Debug.LogError($"功能红点初始化器名:{initializerName}的未注册，无法反激活!");
            return false;            
        
        }
        if(!funcRedDotInitializer.IsActive)
        {
            return false;
        }
        funcRedDotInitializer.Inactive();
        return true;
    }

    /// <summary>
    /// 获取指定功能红点初始化器名的实力对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="initializerName"></param>
    /// <returns></returns>
    protected T GetInitializer<T>(string initializerName) where T : FuncRDInitializer
    {
        if(!AllFuncRedDotInitializerMap.TryGetValue(initializerName, out var funcRedDotInitializer))
        {
            return null;            
        }
        return funcRedDotInitializer as T;
    }

    /// <summary>
    /// 销毁所有已创建的功能红点初始化器
    /// </summary>
    protected void DestroyAllInitializer()
    {
        // 从根功能红点初始化器开始递归销毁，优先销毁里层功能红点初始化器
        // 为未来可能出现功能红点动态清除做准备
        DestroyInitializer(RootRDInitializer);
    }

    /// <summary>
    /// 是否已经初始化指定功能红点初始化器
    /// </summary>
    /// <param name="initializerName"></param>
    /// <returns></returns>
    public bool HasInitializer(string initializerName)
    {
        return AllFuncRedDotInitializerMap.ContainsKey(initializerName);
    }
    
    /// <summary>
    /// 初始化功能根红点初始化器
    /// </summary>
    private void InitRootRDIntializer()
    {
        var (result, rootRDInitializer) = CreateInitializer<RootRDInitializer>();
        RootRDInitializer = rootRDInitializer;
    }
}