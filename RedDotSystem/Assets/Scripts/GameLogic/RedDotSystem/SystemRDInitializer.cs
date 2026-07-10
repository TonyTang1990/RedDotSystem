/*
 * Description:             SystemRDInitializer.cs
 * Author:                  TONYTANG
 * Create Date:             2026/07/09
 */

/// <summary>
/// SystemRDInitializer.cs
/// 系统红点初始化器(根据SystemUnlockModel.cs的系统解锁状态动态解锁的红点初始化器)
/// </summary>
public abstract class SystemRDInitializer : FuncRDInitializer
{
    /// <summary>
    /// 系统类型
    /// </summary>
    protected abstract SystemType SystemType
    {
        get;
    }

    /// <summary>
    /// 是否解锁
    /// </summary>
    /// <returns></returns>
    protected override bool IsUnlock()
    {
        return SystemUnlockModel.Singleton.IsSystemUnlock(SystemType);
    }

    /// <summary>
    /// 添加所有事件
    /// </summary>
    protected override void AddAllEvents()
    {
        base.AddAllEvents();
        AddEvent(EventId.SystemUnlockStateUpdate, OnSystemUnlockStateUpdate);
    }

    /// <summary>
    /// 响应系统解锁状态变化更新
    /// </summary>
    /// <param name="systemType"></param>
    /// <param name="isUnlock"></param>
    protected void OnSystemUnlockStateUpdate(BaseEvent eventData)
    {
        var updateSystemType = (SystemType)eventData.EventParams[0];
        if(updateSystemType != SystemType)
        {
            return;
        }
        var isSystemUnlock = (bool)eventData.EventParams[1];
        if (isSystemUnlock)
        {
            Active();
        }
        else
        {
            Inactive();
        }
    }

    /// <summary>
    /// 功能红点初始化器是否解锁
    /// </summary>
    protected override void InitRedDotInfos()
    {
    }
}