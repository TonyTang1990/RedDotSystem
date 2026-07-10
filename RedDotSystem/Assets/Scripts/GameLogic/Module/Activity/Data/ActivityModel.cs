/*
 * Description:             ActivityModel.cs
 * Author:                  TONYTANG
 * Create Date:             2026/04/03
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ActivityModel.cs
/// 活动数据层单例类
/// </summary>
public class ActivityModel : BaseModel<ActivityModel>
{
    /// <summary>
    /// 活动数据Map<活动id, 活动数据>
    /// </summary>
    private Dictionary<int, ActivityData> activityDataMap = new Dictionary<int, ActivityData>();

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
        activityDataMap.Clear();
    }

    /// <summary>
    /// 添加指定活动配置id的活动数据
    /// </summary>
    /// <param name="activityConfId"></param>
    /// <returns></returns>
    public bool AddActivityData(int activityConfId)
    {
        var activityData = GetActivityData(activityConfId);
        if (activityData != null)
        {
            Debug.LogError($"活动数据层已存在活动配置Id:{activityConfId}的活动数据，添加失败！");
            return false;
        }
        activityData = new ActivityData(activityConfId, true);
        activityDataMap.Add(activityConfId, activityData);
        EventManager.Singleton.DispatchEvent(EventId.ActivityDataAdd, activityConfId);
        return true;
    }

    /// <summary>
    /// 移除指定活动配置id的活动数据
    /// </summary>
    /// <param name="activityConfId"></param>
    /// <returns></returns>
    public bool RemoveActivityData(int activityConfId)
    {
        var activityData = GetActivityData(activityConfId);
        if (activityData == null)
        {
            Debug.LogError($"活动数据层不存在活动配置Id:{activityConfId}的活动数据，移除失败！");
            return false;
        }
        var result = activityDataMap.Remove(activityConfId, out var removeActivityData);
        if(result)
        {
            removeActivityData.Dispose();
            EventManager.Singleton.DispatchEvent(EventId.ActivityDataRemove, activityConfId);
        }
        return result;
    }

    /// <summary>
    /// 获取指定活动id的活动数据
    /// </summary>
    /// <param name="activityConfId"></param>
    /// <returns></returns>
    public ActivityData GetActivityData(int activityConfId)
    {
        if (activityDataMap.TryGetValue(activityConfId, out var activityData))
        {
            return activityData;
        }
        return null;
    }

    /// <summary>
    /// 获取所有激活的活动配置id列表
    /// </summary>
    /// <returns></returns>
    public List<int> GetAllActiveActivityConfIds()
    {
        var activeActivityConfIdList = new List<int>();
        foreach (var activityData in activityDataMap.Values)
        {
            if (activityData.IsActive)
            {
                activeActivityConfIdList.Add(activityData.ActivityConfId);
            }
        }
        return activeActivityConfIdList;
    }

    /// <summary>
    /// 指定活动配置id的活动是否激活
    /// </summary>
    /// <param name="activityConfId"></param>
    /// <returns></returns>
    public bool IsActivityActive(int activityConfId)
    {
        var activityData = GetActivityData(activityConfId);
        return activityData != null && activityData.IsActive;
    }

    /// <summary>
    /// 设置指定活动配置id激活状态
    /// </summary>
    /// <param name="activityConfId"></param>
    /// <param name="isActive"></param>
    public bool SetActivityActive(int activityConfId, bool isActive)
    {
        var activityData = GetActivityData(activityConfId);
        if (activityData == null)
        {
            Debug.LogError($"找不到活动配置Id:{activityConfId}的活动数据，设置活动激活状态:{isActive}失败！");
            return false;
        }
        activityData.SetIsActive(isActive);
        return true;
    }

    /// <summary>
    /// 设置指定活动配置id的活动红点数量1
    /// </summary>
    /// <param name="activityConfId"></param>
    /// <param name="redDotNum1"></param>
    /// <returns></returns>
    public bool SetActivityRedDotNum1(int activityConfId, int redDotNum1)
    {
        var activityData = GetActivityData(activityConfId);
        if (activityData == null)
        {
            Debug.LogError($"找不到活动配置Id:{activityConfId}的活动数据，设置活动红点数量1:{redDotNum1}失败！");
            return false;
        }
        activityData.SetRedDot1Num(redDotNum1);
        return true;
    }

    /// <summary>
    /// 设置指定活动配置id的活动红点数量2
    /// </summary>
    /// <param name="activityConfId"></param>
    /// <param name="redDotNum2"></param>
    /// <returns></returns>
    public bool SetActivityRedDotNum2(int activityConfId, int redDotNum2)
    {
        var activityData = GetActivityData(activityConfId);
        if (activityData == null)
        {
            Debug.LogError($"找不到活动配置Id:{activityConfId}的活动数据，设置活动红点数量2:{redDotNum2}失败！");
            return false;
        }
        activityData.SetRedDot2Num(redDotNum2);
        return true;
    }
}