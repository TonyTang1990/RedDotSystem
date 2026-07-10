/*
 * Description:             BaseModel.cs
 * Author:                  TONYTANG
 * Create Date:             2026/06/15
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// BaseModel.cs
/// 基类数据Model抽象
/// </summary>
public class BaseModel<T> : SingletonTemplate<T> where T : class, new()
{
    /// <summary>
    /// 初始化是否完成
    /// </summary>
    public bool IsInitCompelte
    {
        get;
        private set;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    public BaseModel()
    {
        IsInitCompelte = false;
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        if (IsInitCompelte)
        {
            Debug.LogError($"请勿重复初始化!");
            return;
        }
        IsInitCompelte = true;
        OnInit();
    }

    /// <summary>
    /// 响应初始化(子类重写自定义初始化)
    /// </summary>
    protected virtual void OnInit()
    {
        
    }

    /// <summary>
    /// 释放
    /// </summary>
    public virtual void Dispose()
    {
        OnDispose();
        IsInitCompelte = false;
    }

    /// <summary>
    /// 响应释放(子类重写自定义释放)
    /// </summary>
    protected virtual void OnDispose()
    {
        
    }
}