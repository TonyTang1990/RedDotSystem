/*
 * Description:             RedDotInfo.cs
 * Author:                  TONYTANG
 * Create Date:             2022/08/14
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Note:
// 红点单元绑定推荐用红点处理器的方式

/// <summary>
/// RedDotInfo.cs
/// 红点信息类
/// </summary>
public class RedDotInfo : IRecycle
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
    public List<string> RedDotUnitList
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
    /// 红点处理器
    /// </summary>
    public RedDotHandler RedDotHandler
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

    /// <summary>
    /// 红点名深度
    /// </summary>
    public int RedDotNameDepth
    {
        get;
        private set;
    }

    /// <summary>
    /// 红点单元绑定信息Map[红点单元名字, 红点单元绑定信息]
    /// </summary>
    protected Dictionary<string, RedDotUnitBindData> mRedDotUnitBindDataMap;
    
    /// <summary>
    /// 临时红点名列表
    /// </summary>
    private List<string> mTempRedDotNameList;

    public RedDotInfo()
    {
        RedDotUnitList = new List<string>();
        mRedDotUnitBindDataMap = new Dictionary<string, RedDotUnitBindData>();
        mTempRedDotNameList = new List<string>();
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
        RedDotName = null;
        RedDotDes = null;
        RedDotHandler = null;
        RefreshDelegate = null;
        RedDotUnitList.Clear();
        mRedDotUnitBindDataMap.Clear();
        mTempRedDotNameList.Clear();
        RedDotNameDepth = 0;
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="redDotName"></param>
    /// <param name="redDotDes"></param>
    public void Init(string redDotName, string redDotDes, RedDotHandler redDotHandler = null)
    {
        RedDotName = redDotName;
        RedDotDes = redDotDes;
        RedDotNameDepth = RedDotUtilities.GetRedDotNameDepth(RedDotName);
        BindRedDotHandler(redDotHandler);
    }

    /// <summary>
    /// 清除所有数据
    /// </summary>
    public void Clear()
    {
        RemoveAllRedDotUnit();
        RedDotName = null;
        RedDotDes = null;
        RefreshDelegate = null;
        RedDotNameDepth = 0;
    }

    /// <summary>
    /// 绑定红点处理器
    /// </summary>
    /// <param name="redDotHandler"></param>
    /// <returns></returns>
    public bool BindRedDotHandler(RedDotHandler redDotHandler)
    {
        if(redDotHandler == null)
        {
            return false;
        }
        if(RedDotHandler != null)
        {
            Debug.LogError($"红点名:{RedDotName}已绑定红点处理器,请先清理原有红点处理器,绑定失败!");
            return false;
        }
        RedDotHandler = redDotHandler;
        return true;
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
        RefreshDelegate?.Invoke(RedDotName, result, redDotType);
    }

    /// <summary>
    /// 添加影响的红点运算单元
    /// </summary>
    /// <param name="redDotUnit"></param>
    /// <param name="recusive">是否递归添加(向上找有效红点名)</param>
    /// <param name="useBind">是否使用绑定的红点处理器方式添加红点单元(特殊情况比如多入口显示时只负责添加不负责绑定传false)</param>
    /// <returns></returns>
    public bool AddRedDotUnit(string redDotUnit, bool recusive = true, bool useBind = true)
    {
        if (RedDotUnitList.Contains(redDotUnit))
        {
            //Debug.LogError($"红点名:{RedDotName}重复添加影响红点运算单元:{redDotUnit.ToString()}，添加失败！");
            return false;
        }
        if(useBind)
        {
            AddRedDotUnitBindData(redDotUnit, recusive);
        }
        RedDotUnitList.Add(redDotUnit);
        RedDotModel.Singleton.AddRedDotUnitAndName(redDotUnit, RedDotName);
        if(recusive)
        {
            mTempRedDotNameList.Clear();
            RedDotModel.Singleton.GetParentRedDotNameList(RedDotName, ref mTempRedDotNameList);
            foreach(var parentRedDotName in mTempRedDotNameList)
            {
                var parentRedDotInfo = RedDotModel.Singleton.GetRedDotInfoByName(parentRedDotName);
                if(parentRedDotInfo != null)
                {
                    parentRedDotInfo.AddRedDotUnit(redDotUnit, recusive, useBind);
                }
            }
        }
        // 为了支持动态红点的创建的删除，在给红点名添加红点运算单元时默认标脏一次红点名运算单元
        // 确保动态添加的红点单元能触发红点名刷新回调
        RedDotManager.Singleton.MarkRedDotUnitDirty(redDotUnit);
        return true;
    }

    /// <summary>
    /// 添加红点单元绑定信息
    /// </summary>
    /// <param name="redDotUnit"></param>
    /// <param name="isRecusive"></param>
    /// <returns></returns>
    protected bool AddRedDotUnitBindData(string redDotUnit, bool isRecusive)
    {
        if(mRedDotUnitBindDataMap.ContainsKey(redDotUnit))
        {
            Debug.LogError($"红点名:{RedDotName}已添加红点单元:{redDotUnit}绑定信息，添加失败!");
            return false;
        }
        var redDotBindData = ObjectPool.Singleton.pop<RedDotUnitBindData>();
        redDotBindData.Init(redDotUnit, isRecusive);
        mRedDotUnitBindDataMap.Add(redDotUnit, redDotBindData);
        return true;
    }

    /// <summary>
    /// 清除红点单元绑定信息
    /// </summary>
    /// <param name="redDotUnit"></param>
    /// <returns></returns>
    protected bool RemoveRedDotUnitBindData(string redDotUnit)
    {
        if(!mRedDotUnitBindDataMap.TryGetValue(redDotUnit, out var redDotUnitBindData))
        {
            Debug.LogError($"红点名:{RedDotName}未添加红点单元:{redDotUnit}绑定信息，清除红点单元绑定信息失败!");
            return false;
        }
        mRedDotUnitBindDataMap.Remove(redDotUnit);
        ObjectPool.Singleton.push(redDotUnitBindData);
        return true;
    }

    /// <summary>
    /// 获取红点单元绑定信息
    /// </summary>
    /// <param name="redDotUnit"></param>
    /// <returns></returns>
    protected RedDotUnitBindData GetRedDotUnitBindData(string redDotUnit)
     {
         if(mRedDotUnitBindDataMap.TryGetValue(redDotUnit, out var redDotUnitBindData))
         {
             return redDotUnitBindData;
         }
         return null;
     }

    /// <summary>
    /// 移除影响的红点运算单元
    /// Note:
    /// 解绑是否递归清除由绑定时的数据决定，所以不支持主动传isRecursive
    /// </summary>
    /// <param name="redDotUnit"></param>
    /// <returns></returns>
    public bool RemoveRedDotUnit(string redDotUnit)
    {
        if (!RedDotUnitList.Contains(redDotUnit))
        {
            Debug.LogError($"红点名:{RedDotName}未添加影响红点运算单元:{redDotUnit.ToString()}，移除失败!");
            return false;
        }
        var result = RedDotUnitList.Remove(redDotUnit);
        if(result)
        {
            RedDotModel.Singleton.RemoveRedDotUnitAndName(redDotUnit, RedDotName); 
        }
        // 怎么绑定的就怎么解绑
        var redDotUnitBindData = GetRedDotUnitBindData(redDotUnit);
        if(redDotUnitBindData != null)
        {
            RemoveRedDotUnitBindData(redDotUnit);
            if(redDotUnitBindData.IsRecursive)
            {
                mTempRedDotNameList.Clear();
                RedDotModel.Singleton.GetParentRedDotNameList(RedDotName, ref mTempRedDotNameList);
                foreach(var parentRedDotName in mTempRedDotNameList)
                {
                    var parentRedDotInfo = RedDotModel.Singleton.GetRedDotInfoByName(parentRedDotName);
                    if(parentRedDotInfo != null)
                    {
                        parentRedDotInfo.RemoveRedDotUnit(redDotUnit);
                    }
                }
            }
        }
        // 为了支持动态红点单元删除，在移除红点单元时可能出现红点结果不变但需要刷新的情况
        // 确保动态添加的红点名能正确刷新显示，这里强制刷新对应红点名
        RedDotManager.Singleton.ForceRefreshRedDotName(RedDotName);
        return result;
    }

    /// <summary>
    /// 清理绑定的红点处理器
    /// </summary>
    private void ClearRedDotHandler()
    {
        if(RedDotHandler != null)
        {
            RedDotHandler.Clear();
            ObjectPool.Singleton.push(RedDotHandler);
            RedDotHandler = null;
        }
    }
    
    /// <summary>
    /// 移除所有红点运算单元
    /// </summary>
    private void RemoveAllRedDotUnit()
    {
        // 因为可能存在通过红点处理器方式绑定和直接通过RedDotInfo绑定红点单元两种方式
        // 优先清除红点处理器，避免重复清除红点运算单元数据
        ClearRedDotHandler();
        for(int index = RedDotUnitList.Count - 1; index >= 0; index--)
        {
            RemoveRedDotUnit(RedDotUnitList[index]);
        }
    }
}