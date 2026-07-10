/*
 * Description:             RedDotUnitInfo.cs
 * Author:                  TONYTANG
 * Create Date:             2022/08/14
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// RedDotUnitInfo.cs
/// 红点运算单元信息类
/// </summary>
public class RedDotUnitInfo : IRecycle
{
    /// <summary>
    /// 红点运算单元类型
    /// </summary>
    public string RedDotUnit
    {
        get;
        private set;
    }

    /// <summary>
    /// 红点运算单元描述
    /// </summary>
    public string RedDotUnitDes
    {
        get;
        private set;
    }

    /// <summary>
    /// 红点运算单元对应显示的红点类型
    /// </summary>
    public RedDotType RedDotType
    {
        get;
        private set;
    }

    /// <summary>
    /// 红点运算单元对应红点计算回调
    /// </summary>
    public Func<int> RedDotUnitCalculateFunc
    {
        get;
        private set;
    }
    
    /// <summary>
    /// 出池
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
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
        RedDotUnit = null;
        RedDotUnitDes = null;
        RedDotUnitCalculateFunc = null;
        RedDotType = RedDotType.NONE;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    public RedDotUnitInfo()
    {

    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="redDotUnit">红点运算单元类型</param>
    /// <param name="redDotUnitDes">红点运算单元描述</param>
    /// <param name="redDotUnitCalculateFunc">红点运算单元计算方法</param>
    /// <param name="redDotType">红点显示类型</param>
    public void Init(string redDotUnit, string redDotUnitDes, Func<int> redDotUnitCalculateFunc, RedDotType redDotType)
    {
        RedDotUnit = redDotUnit;
        RedDotUnitDes = redDotUnitDes;
        RedDotUnitCalculateFunc = redDotUnitCalculateFunc;
        RedDotType = redDotType;
    }

    /// <summary>
    /// 清理
    /// </summary>
    public void Clear()
    {
        RedDotUnit = null;
        RedDotUnitDes = null;
        RedDotUnitCalculateFunc = null;
        RedDotType = RedDotType.NONE;
    }
}