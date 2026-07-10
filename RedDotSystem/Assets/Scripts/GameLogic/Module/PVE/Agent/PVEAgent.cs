/*
 * Description:             PVEAgent.cs
 * Author:                  TONYTANG
 * Create Date:             2026/07/10
 */

using UnityEngine;

/// <summary>
/// PVEAgent.cs
/// PVE代理
/// </summary>
public static class PVEAgent
{
    /// <summary>
    /// 打开PVE界面
    /// </summary>
    /// <returns></returns>
    public static bool OpenPVEUI()
    {
        if(!SystemUnlockModel.Singleton.IsSystemUnlock(SystemType.PVE))
        {
            Debug.LogError($"PVE系统未解锁，无法打开PVE界面");
            return false;
        }
        GameLauncher.Singleton.PVEUI.Open();
        return true;
    }
}