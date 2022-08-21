/*
 * Description:             RedDotInfo.cs
 * Author:                  TONYTANG
 * Create Date:             2022/08/14
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// RedDotInfo.cs
/// 红点信息类
/// </summary>
public class RedDotInfo
{
    /// <summary>
    /// 红点名
    /// </summary>
    public string RedDotName
    {
        get;
        private set;
    }

    /// <summary>
    /// 红点描述
    /// </summary>
    public string RedDotDes
    {
        get;
        private set;
    }

    /// <summary>
    /// 红点刷新委托(理论上只会绑定一个回调)
    /// </summary>
    public Action<string, int, RedDotType> RefreshDelegate
    {
        get;
        private set;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="redDotName"></param>
    /// <param name="redDotDes"></param>
    public RedDotInfo(string redDotName, string redDotDes)
    {
        RedDotName = redDotName;
        RedDotDes = redDotDes;
    }

    /// <summary>
    /// 红点对象刷新绑定
    /// </summary>
    /// <param name="refreshDelegate"></param>
    public void Bind(Action<string, int, RedDotType> refreshDelegate)
    {
        if(RefreshDelegate != null)
        {
            Debug.LogError($"红点名:{RedDotName}重复绑定刷新回调!");
        }
        RefreshDelegate = refreshDelegate;
    }

    /// <summary>
    /// 红点对象解绑定
    /// </summary>
    public void UnBind()
    {
        RefreshDelegate = null;
    }

    /// <summary>
    /// 触发刷新结果
    /// </summary>
    /// <param name="result"></param>
    /// <param name="redDotType"></param>
    public void TriggerUpdate(int result, RedDotType redDotType)
    {
        if(RefreshDelegate != null)
        {
            RefreshDelegate(RedDotName, result, redDotType);
        }
    }
}