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
    /// 所有影响此红点的红点单元列表
    /// </summary>
    public List<RedDotUnit> RedDotUnitList
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
    /// 红点刷新委托(一般只会绑定一个回调)
    /// </summary>
    public Action<string, int, RedDotType> RefreshDelegate
    {
        get;
        private set;
    }

    private RedDotInfo()
    {
        RedDotUnitList = new List<RedDotUnit>();
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
        RedDotUnitList = new List<RedDotUnit>();
    }

    /// <summary>
    /// 红点对象刷新绑定
    /// </summary>
    /// <param name="refreshDelegate"></param>
    public bool Bind(Action<string, int, RedDotType> refreshDelegate)
    {
        if(refreshDelegate == null)
        {
            Debug.LogError($"红点名:{RedDotName}不允许绑定空刷新回调!");
            return false;
        }
        RefreshDelegate += refreshDelegate;
        return true;
    }

    /// <summary>
    /// 红点对象解绑定
    /// </summary>
    /// <param name="refreshDelegate"></param>
    public void UnBind(Action<string, int, RedDotType> refreshDelegate)
    {
        RefreshDelegate -= refreshDelegate;
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
            RefreshDelegate.Invoke(RedDotName, result, redDotType);
        }
    }

    /// <summary>
    /// 添加影响的红点运算单元
    /// </summary>
    /// <param name="redDotUnit"></param>
    /// <returns></returns>
    public bool AddRedDotUnit(RedDotUnit redDotUnit)
    {
        if (RedDotUnitList.Contains(redDotUnit))
        {
            Debug.LogError($"红点名:{RedDotName}重复添加影响红点运算单元:{redDotUnit.ToString()}，添加失败！");
            return false;
        }
        RedDotUnitList.Add(redDotUnit);
        return true;
    }

    /// <summary>
    /// 移除影响的红点运算单元
    /// </summary>
    /// <param name="redDotName"></param>
    /// <returns></returns>
    public bool RemoveRedDotUnit(RedDotUnit redDotUnit)
    {
        if (!RedDotUnitList.Contains(redDotUnit))
        {
            Debug.LogError($"红点名:{RedDotName}未添加影响红点运算单元:{redDotUnit.ToString()}，移除失败!");
            return false;
        }
        return RedDotUnitList.Remove(redDotUnit);
    }
}