/*
 * Description:             ActivityData.cs
 * Author:                  TONYTANG
 * Create Date:             2026/05/22
 */

/// <summary>
/// ActivityData.cs
/// 活动数据类
/// </summary>
public class ActivityData
{
    /// <summary>
    /// 活动配置id
    /// </summary>
    public int ActivityConfId
    {
        get;
        private set;
    }

    /// <summary>
    /// 是否激活
    /// </summary>
    public bool IsActive
    {
        get;
        private set;
    }

    /// <summary>
    /// 红点数量1
    /// </summary>
    public int RedDot1Num
    {
        get;
        private set;
    }

    /// <summary>
    /// 红点数量2
    /// </summary>
    public int RedDot2Num
    {
        get;
        private set;
    }

    public ActivityData(int activityConfId, bool isActive)
    {
        ActivityConfId = activityConfId;
        SetIsActive(isActive);
    }
    
    /// <summary>
    /// 设置是否激活
    /// </summary>
    /// <param name="isActive"></param>
    public void SetIsActive(bool isActive)
    {
        if(IsActive == isActive)
        {
            return;
        }
        IsActive = isActive;
        if(IsActive)
        {
            OnActive();
        }
        else
        {
            OnInactive();
        }
        EventManager.Singleton.DispatchEvent(EventId.IsActiveUpdate, ActivityConfId, IsActive);
    }

    /// <summary>
    /// 释放
    /// </summary>
    public void Dispose()
    {
        // 释放时如果活动没关闭要关闭活动
        if(IsActive)
        {
            SetIsActive(false);
        }
    }

    /// <summary>
    /// 设置红点1数量
    /// </summary>
    /// <param name="redDot1Num"></param>
    public void SetRedDot1Num(int redDot1Num)
    {
        redDot1Num = redDot1Num >= 0 ? redDot1Num : 0;
        if (RedDot1Num == redDot1Num)
        {
            return;
        }
        RedDot1Num = redDot1Num;
        EventManager.Singleton.DispatchEvent(EventId.RedDot1NumUpdate, ActivityConfId, redDot1Num);
    }

    /// <summary>
    /// 设置红点2数量
    /// </summary>
    /// <param name="redDot2Num"></param>
    public void SetRedDot2Num(int redDot2Num)
    {
        redDot2Num = redDot2Num >= 0 ? redDot2Num : 0;
        if (RedDot2Num == redDot2Num)
        {
            return;
        }
        RedDot2Num = redDot2Num;
        EventManager.Singleton.DispatchEvent(EventId.RedDot2NumUpdate, ActivityConfId, redDot2Num);
    }

    /// <summary>
    /// 响应活动激活
    /// </summary>
    protected void OnActive()
    {
        InitRedDotInfos();
    }

    /// <summary>
    /// 响应活动不激活
    /// </summary>
    protected void OnInactive()
    {
        RemoveAllRedDotInfos();
    }

    /// <summary>
    /// 初始化红点信息
    /// </summary>
    protected void InitRedDotInfos()
    {
        var actRedDotEntryName = ActivityAgent.GetActEntryRedDotName(ActivityConfId);
        RedDotModel.Singleton.AddRedDotInfo(actRedDotEntryName, $"活动Id:{ActivityConfId}入口红点");
        var actRedDot1EntryName = ActivityAgent.GetActRedDot1EntryName(ActivityConfId);
        RedDotModel.Singleton.AddDynamicRedDotInfo<ActRed1NumRDH, int>(actRedDot1EntryName, $"活动Id:{ActivityConfId}红点1入口红点", ActivityConfId);
        var actRedDot2EntryName = ActivityAgent.GetActRedDot2EntryName(ActivityConfId);
        RedDotModel.Singleton.AddDynamicRedDotInfo<ActRed2NumRDH, int>(actRedDot2EntryName, $"活动Id:{ActivityConfId}红点2入口红点", ActivityConfId);
    }

    /// <summary>
    /// 移除所有红点信息
    /// </summary>
    protected void RemoveAllRedDotInfos()
    {
        var actRedDotEntryName = ActivityAgent.GetActEntryRedDotName(ActivityConfId);
        RedDotModel.Singleton.RemoveRedDotInfo(actRedDotEntryName);
        var actRedDot1EntryName = ActivityAgent.GetActRedDot1EntryName(ActivityConfId);
        RedDotModel.Singleton.RemoveRedDotInfo(actRedDot1EntryName);
        var actRedDot2EntryName = ActivityAgent.GetActRedDot2EntryName(ActivityConfId);
        RedDotModel.Singleton.RemoveRedDotInfo(actRedDot2EntryName);
    }
}