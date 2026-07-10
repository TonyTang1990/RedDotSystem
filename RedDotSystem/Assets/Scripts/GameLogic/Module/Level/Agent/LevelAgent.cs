/*
 * Description:             LevelAgent.cs
 * Author:                  TONYTANG
 * Create Date:             2026/07/10
 */

using UnityEngine;

/// <summary>
/// LevelAgent.cs
/// 关卡代理
/// </summary>
public static class LevelAgent
{
    /// <summary>
    /// 打开关卡入口界面
    /// </summary>
    /// <returns></returns>
    public static bool OpenLevelEntryUI()
    {
        if(!SystemUnlockModel.Singleton.IsSystemUnlock(SystemType.Level))
        {
            Debug.LogError($"关卡系统未解锁，无法打开关卡入口界面");
            return false;
        }
        GameLauncher.Singleton.LevelEntryUI.Open();
        return true;
    }

    /// <summary>
    /// 打开关卡界面
    /// </summary>
    /// <param name="levelId"></param>
    /// <returns></returns>
    public static bool OpenLevelUI(int levelId = 0)
    {
        if(!SystemUnlockModel.Singleton.IsSystemUnlock(SystemType.Level))
        {
            Debug.LogError($"关卡系统未解锁，无法打开关卡界面");
            return false;
        }
        GameLauncher.Singleton.LevelUI.Open(levelId);
        return true;
    }
}