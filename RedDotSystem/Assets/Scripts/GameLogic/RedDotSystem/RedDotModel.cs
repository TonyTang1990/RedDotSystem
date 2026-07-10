/*
 * Description:             RedDotModel.cs
 * Author:                  TONYTANG
 * Create Date:             2022/08/14
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// RedDotModel.cs
/// 红点数据单例类
/// </summary>
public class RedDotModel : BaseModel<RedDotModel>
{
    /// <summary>
    /// 红点名分隔符
    /// </summary>
    public const string Separator = "|";

    /// <summary>
    /// 红点运算单元信息Map<红点运算单元名, 红点运算单元信息>
    /// </summary>
    private Dictionary<string, RedDotUnitInfo> mRedDotUnitInfoMap;

    /// <summary>
    /// 红点运算单元结果值Map<红点运算单元, 红点运算单元结果值>
    /// </summary>
    private Dictionary<string, int> mRedDotUnitResultMap;

    /// <summary>
    /// 红点名和红点信息Map<红点名, 红点信息>
    /// </summary>
    private Dictionary<string, RedDotInfo> mRedDotInfoMap;

    /// <summary>
    /// 红点单元和红点名列表Map
    /// </summary>
    private Dictionary<string, List<string>> mRedDotUnitNameMap;

    /// <summary>
    /// 红点前缀树
    /// </summary>
    public Trie RedDotTrie
    {
        get;
        private set;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    public RedDotModel() : base()
    {
        mRedDotUnitInfoMap = new Dictionary<string, RedDotUnitInfo>();
        mRedDotUnitResultMap = new Dictionary<string, int>();
        mRedDotInfoMap = new Dictionary<string, RedDotInfo>();
        mRedDotUnitNameMap = new Dictionary<string, List<string>>();
        RedDotTrie = new Trie(Separator);
    }

    /// <summary>
    /// 响应初始化
    /// </summary>
    protected override void OnInit()
    {
        base.OnInit();
    }

    /// <summary>
    /// 响应释放
    /// </summary>
    protected override void OnDispose()
    {
        base.OnDispose();
        mRedDotUnitInfoMap.Clear();
        mRedDotUnitResultMap.Clear();
        mRedDotInfoMap.Clear();
        mRedDotUnitNameMap.Clear();
        RedDotTrie.Dispose();
    }

    /// <summary>
    /// 添加指定红点信息(不带红点处理器)
    /// 没有独立红点单元信息的红点无需构造红点处理器
    /// </summary>
    /// <param name="redDotName"></param>
    /// <param name="redDotDes"></param>
    /// <returns></returns>
    public (bool, RedDotInfo) AddRedDotInfo(string redDotName, string redDotDes)
    {
        if (mRedDotInfoMap.TryGetValue(redDotName, out var existRedDotInfo))
        {
            Debug.LogError($"已添加红点名:{redDotName}的红点信息,请勿重复添加,添加失败!");
            return (false, existRedDotInfo);
        }
        var redDotInfo = ObjectPool.Singleton.pop<RedDotInfo>();
        redDotInfo.Init(redDotName, redDotDes);
        mRedDotInfoMap.Add(redDotName, redDotInfo);
        RedDotTrie.AddWord(redDotName);
        return (true, redDotInfo);
    }

    /// <summary>
    /// 添加指定红点信息(带红点处理器)
    /// 有独立红点单元信息的红点需要构造红点处理器
    /// </summary>
    /// <param name="redDotName"></param>
    /// <param name="redDotDes"></param>
    /// <returns></returns>
    public (bool, RedDotInfo) AddRedDotInfo<T>(string redDotName, string redDotDes) where T : RedDotHandler, new()
    {
        if (mRedDotInfoMap.TryGetValue(redDotName, out var existRedDotInfo))
        {
            Debug.LogError($"已添加红点名:{redDotName}的红点信息,请勿重复添加,添加失败!");
            return (false, existRedDotInfo);
        }
        var redDotHandler = new T();
        var redDotInfo = ObjectPool.Singleton.pop<RedDotInfo>();
        redDotInfo.Init(redDotName, redDotDes, redDotHandler);
        // 在红点处理器初始化之前必须先将红点信息添加进去
        // 不然后续可能找不到当前红点信息
        mRedDotInfoMap.Add(redDotName, redDotInfo);
        RedDotTrie.AddWord(redDotName);
        redDotHandler.Init(redDotInfo);
        return (true, redDotInfo);
    }

    /// <summary>
    /// 添加指定动态红点信息(带红点处理器，带动态参数)
    /// 有独立红点单元信息的红点需要构造红点处理器
    /// </summary>
    /// <param name="redDotName"></param>
    /// <param name="redDotDes"></param>
    /// <param name="dynamicData"></param>
    /// <returns></returns>
    public (bool, RedDotInfo) AddDynamicRedDotInfo<T, W>(string redDotName, string redDotDes, W dynamicData) where T : DynamicRedDotHandler<W>, new()
    {
        if (mRedDotInfoMap.TryGetValue(redDotName, out var existRedDotInfo))
        {
            Debug.LogError($"已添加红点名:{redDotName}的红点信息,请勿重复添加,添加失败!");
            return (false, existRedDotInfo);
        }
        var redDotHandler = new T();
        var redDotInfo = ObjectPool.Singleton.pop<RedDotInfo>();
        redDotInfo.Init(redDotName, redDotDes, redDotHandler);
        redDotHandler.Data = dynamicData;
        // 在红点处理器初始化之前必须先将红点信息添加进去
        // 不然后续可能找不到当前红点信息
        mRedDotInfoMap.Add(redDotName, redDotInfo);
        RedDotTrie.AddWord(redDotName);
        redDotHandler.Init(redDotInfo);

        return (true, redDotInfo);
    }

    /// <summary>
    /// 移除指定红点名信息
    /// </summary>
    /// <param name="redDotName"></param>
    /// <returns></returns>
    public bool RemoveRedDotInfo(string redDotName)
    {
        if(!mRedDotInfoMap.TryGetValue(redDotName, out var existRedDotInfo))
        {
            Debug.LogError($"找不到红点名:{redDotName}的红点名信息，移除失败！");
            return false;
        }
        existRedDotInfo.Clear();
        mRedDotInfoMap.Remove(redDotName);
        RedDotTrie.RemoveWord(redDotName);
        return true;
    }

    /// <summary>
    /// 添加红点运算单元信息
    /// </summary>
    /// <param name="redDotUnit"></param>
    /// <param name="redDotUnitDes"></param>
    /// <param name="caculateFunc"></param>
    /// <param name="redDotType"></param>
    /// <returns></returns>
    public (bool, RedDotUnitInfo) AddRedDotUnitInfo(string redDotUnit, string redDotUnitDes, Func<int> caculateFunc, RedDotType redDotType = RedDotType.NUMBER)
    {
        if(mRedDotUnitInfoMap.TryGetValue(redDotUnit, out var redDotUnitInfo))
        {
            Debug.LogError($"已添加红点运算单元:{redDotUnit}的红点运算单元信息,请勿重复添加,添加失败!");
            return (false, redDotUnitInfo);
        }
        redDotUnitInfo = ObjectPool.Singleton.pop<RedDotUnitInfo>();
        redDotUnitInfo.Init(redDotUnit, redDotUnitDes, caculateFunc, redDotType);
        mRedDotUnitInfoMap.Add(redDotUnit, redDotUnitInfo);
        return (true, redDotUnitInfo);
    }

    /// <summary>
    /// 清除指定红点运算单元信息
    /// </summary>
    /// <param name="redDotUnit"></param>
    /// <returns></returns>
    public bool RemoveRedDotUnitInfo(string redDotUnit)
    {
        if(!mRedDotUnitInfoMap.TryGetValue(redDotUnit, out var redDotUnitInfo))
        {
            Debug.LogError($"找不到红点运算单元:{redDotUnit}信息,删除失败!");
            return false;
        }
        redDotUnitInfo.Clear();
        ObjectPool.Singleton.push(redDotUnitInfo);
        mRedDotUnitInfoMap.Remove(redDotUnit);
        return true;
    }

    /// <summary>
    /// 是否包含指定红点运算单元信息
    /// </summary>
    /// <param name="redDotUnit"></param>
    /// <returns></returns>
    public bool ExistsRedDotUnitInfo(string redDotUnit)
    {
        return mRedDotUnitInfoMap.ContainsKey(redDotUnit);
    }

    /// <summary>
    /// 获取红点名和红点信息Map<红点名, 红点信息>
    /// </summary>
    /// <returns></returns>
    public Dictionary<string, RedDotInfo> GetRedDotInfoMap()
    {
        return mRedDotInfoMap;
    }

    /// <summary>
    /// 获取红点运算单元信息Map<红点运算单元门, 红点运算单元信息>
    /// </summary>
    /// <returns></returns>
    public Dictionary<string, RedDotUnitInfo> GetRedDotUnitInfoMap()
    {
        return mRedDotUnitInfoMap;
    }

    /// <summary>
    /// 获取指定红点名的红点信息
    /// </summary>
    /// <param name="redDotName"></param>
    /// <returns></returns>
    public RedDotInfo GetRedDotInfoByName(string redDotName)
    {
        RedDotInfo redDotInfo;
        if(!mRedDotInfoMap.TryGetValue(redDotName, out redDotInfo))
        {
            //Debug.LogError($"找不到红点名:{redDotName}的红点信息!");
        }
        return redDotInfo;
    }

    /// <summary>
    /// 获取指定红点运算单元的红点运算单元信息
    /// </summary>
    /// <param name="redDotUnit"></param>
    /// <returns></returns>
    public RedDotUnitInfo GetRedDotUnitInfo(string redDotUnit)
    {
        RedDotUnitInfo redDotUnitInfo;
        if(!mRedDotUnitInfoMap.TryGetValue(redDotUnit, out redDotUnitInfo))
        {
            Debug.LogError($"找不到红点运算单元:{redDotUnit.ToString()}的信息!");
        }
        return redDotUnitInfo;
    }

    /// <summary>
    /// 获取指定红点运算单元的计算委托
    /// </summary>
    /// <param name="redDotUnit"></param>
    /// <returns></returns>
    public Func<int> GetRedDotUnitFunc(string redDotUnit)
    {
        RedDotUnitInfo redDotUnitInfo = GetRedDotUnitInfo(redDotUnit);
        return redDotUnitInfo?.RedDotUnitCalculateFunc;
    }

    /// <summary>
    /// 获取指定红点运算单元的红点类型
    /// </summary>
    /// <param name="redDotUnit"></param>
    /// <returns></returns>
    public RedDotType GetRedDotUnitRedType(string redDotUnit)
    {
        RedDotUnitInfo redDotUnitInfo = GetRedDotUnitInfo(redDotUnit);
        if(redDotUnitInfo != null)
        {
            return redDotUnitInfo.RedDotType;
        }
        Debug.LogError($"获取红点运算单元:{redDotUnit}的红点类型失败!");
        return RedDotType.NONE;
    }

    /// <summary>
    /// 获取指定红点名的所有红点运算单元列表
    /// </summary>
    /// <param name="redDotName"></param>
    /// <returns></returns>
    public List<string> GetRedDotUnitsByName(string redDotName)
    {
        var redDotInfo = GetRedDotInfoByName(redDotName);
        return redDotInfo != null ? redDotInfo.RedDotUnitList : null;
    }

    /// <summary>
    /// 获取指定红点运算单元影响的所有红点名列表
    /// </summary>
    /// <param name="redDotUnit"></param>
    /// <returns></returns>
    public List<string> GetRedDotNamesByUnit(string redDotUnit)
    {
        List<string> redDotNames;
        if(!mRedDotUnitNameMap.TryGetValue(redDotUnit, out redDotNames))
        {
            Debug.LogError($"找不到红点运算单元:{redDotUnit}的影响红点名信息，获取失败！");
        }
        return redDotNames;
    }

    /// <summary>
    /// 添加指定红点运算单元影响的指定红点名
    /// </summary>
    /// <param name="redDotUnit"></param>
    /// <param name="redDotName"></param>
    /// <returns></returns>
    public bool AddRedDotUnitAndName(string redDotUnit, string redDotName)
    {
        if(!mRedDotUnitNameMap.TryGetValue(redDotUnit, out var redDotNames))
        {
            redDotNames = new List<string>();
            mRedDotUnitNameMap.Add(redDotUnit, redDotNames);
        }
        if(redDotNames.Contains(redDotName))
        {
           return false; 
        }
        redDotNames.Add(redDotName);
        return true;
    }

    /// <summary>
    /// 移除指定红点运算单元影响的指定红点名
    /// </summary>
    /// <param name="redDotUnit"></param>
    /// <param name="redDotName"></param>
    /// <returns></returns>
    public bool RemoveRedDotUnitAndName(string redDotUnit, string redDotName)
    {
        if(!mRedDotUnitNameMap.TryGetValue(redDotUnit, out var redDotNames))
        {
            Debug.LogError($"找不到红点运算单元:{redDotUnit}的影响的红点名数据，移除红点名:{redDotName}失败！");
            return false;
        }
        var result = redDotNames.Remove(redDotName);
        if(!result)
        {
            Debug.LogError($"找不到红点运算单元:{redDotUnit}影响的红点名:{redDotName}，移除失败！");
            return false;            
        }
        return result;
    }

    /// <summary>
    /// 获取指定红点运算单元的运算结果
    /// </summary>
    /// <param name="redDotUnit"></param>
    /// <returns></returns>
    public int GetRedDotUnitResult(string redDotUnit)
    {
        int result = 0;
        if(!mRedDotUnitResultMap.TryGetValue(redDotUnit, out result))
        {
        }
        return result;
    }

    /// <summary>
    /// 设置指定红点运算单元的运算结果
    /// </summary>
    /// <param name="redDotUnit"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public bool SetRedDotUnitResult(string redDotUnit, int result)
    {
        if(!mRedDotUnitResultMap.ContainsKey(redDotUnit))
        {
            mRedDotUnitResultMap.Add(redDotUnit, result);
            return true;
        }
        mRedDotUnitResultMap[redDotUnit] = result;
        return true;
    }

    /// <summary>
    /// 判断指定红点名是否存在
    /// </summary>
    /// <param name="redDotName"></param>
    /// <returns></returns>
    public bool IsRedDotNameExist(string redDotName)
    {
        return mRedDotInfoMap.ContainsKey(redDotName);
    }

    /// <summary>
    /// 判断指定红点运算单元是否存在
    /// </summary>
    /// <param name="redDotUnit"></param>
    /// <returns></returns>
    public bool IsRedDotUnitExist(string redDotUnit)
    {
        return mRedDotUnitInfoMap.ContainsKey(redDotUnit);
    }

    #region 前缀树相关
    /// <summary>
    /// 获取指定红点名的父红点名列表(不含自身)
    /// </summary>
    /// <param name="redDotName"></param>
    /// <param name="parentRedDotNameList"></param>
    public void GetParentRedDotNameList(string redDotName, ref List<string> parentRedDotNameList)
    {
        RedDotTrie.GetParentWordList(redDotName, ref parentRedDotNameList);
    }
    #endregion
}