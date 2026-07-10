/*
 * Description:             EquipAgent.cs
 * Author:                  TONYTANG
 * Create Date:             2026/07/10
 */

using UnityEngine;

/// <summary>
/// EquipAgent.cs
/// 装备代理
/// </summary>
public static class EquipAgent
{
    /// <summary>
    /// 打开装备界面
    /// </summary>
    /// <returns></returns>
    public static bool OpenEquipUI()
    {
        if(!SystemUnlockModel.Singleton.IsSystemUnlock(SystemType.Equip))
        {
            Debug.LogError($"装备系统未解锁，无法打开装备界面");
            return false;
        }
        GameLauncher.Singleton.EquipUI.Open();
        return true;
    }
}