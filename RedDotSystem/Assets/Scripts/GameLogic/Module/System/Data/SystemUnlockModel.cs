/*
 * Description:             SystemUnlockModel.cs
 * Author:                  TONYTANG
 * Create Date:             2026/07/09
 */

using System.Collections.Generic;

/// <summary>
/// SystemUnlockModel.cs
/// 系统解锁模块
/// </summary>
public class SystemUnlockModel : BaseModel<SystemUnlockModel>
{
    /// <summary>
    /// 系统解锁类型映射
    /// </summary>
    private Dictionary<SystemType, bool> mSystemUnlockTypeMap = new Dictionary<SystemType, bool>();

    /// <summary>
    /// 响应初始化
    /// </summary>
    protected override void OnInit()
    {
        base.OnInit();
    }

    /// <summary>
    /// 更新系统类型解锁状态
    /// </summary>
    /// <param name="systemType"></param>
    /// <param name="isUnlock"></param>
    public void UpdateSystemUnlockType(SystemType systemType, bool isUnlock)
    {
        var isSystemUnlock = IsSystemUnlock(systemType);
        if (isSystemUnlock == isUnlock)
        {
            return;
        }
        if (mSystemUnlockTypeMap.ContainsKey(systemType))
        {
            mSystemUnlockTypeMap[systemType] = isUnlock;
        }
        else
        {
            mSystemUnlockTypeMap.Add(systemType, isUnlock);
        }
        EventManager.Singleton.DispatchEvent(EventId.SystemUnlockStateUpdate, systemType, isUnlock);
    }

    /// <summary>
    /// 指定系统类型是否解锁
    /// </summary>
    /// <param name="systemType"></param>
    /// <returns></returns>
    public bool IsSystemUnlock(SystemType systemType)
    {
        if (mSystemUnlockTypeMap.ContainsKey(systemType))
        {
            return mSystemUnlockTypeMap[systemType];
        }
        return false;
    }
}