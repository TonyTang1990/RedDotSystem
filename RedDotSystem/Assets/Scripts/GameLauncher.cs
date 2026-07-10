/*
 * Description:             GameLauncher.cs
 * Author:                  TONYTANG
 * Create Date:             2022/08/10
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GameLauncher.cs
/// 游戏启动器
/// </summary>
public class GameLauncher : MonoBehaviour
{
    /// <summary>
    /// 单例对象
    /// </summary>
    public static GameLauncher Singleton
    {
        get
        {
            return mSingleton;
        }
    }
    private static GameLauncher mSingleton;

    /// <summary>
    /// 主界面
    /// </summary>
    [Header("主界面")]
    public MainUI MainUI;

    /// <summary>
    /// 背包界面
    /// </summary>
    [Header("背包界面")]
    public BackpackUI BackpackUI;

    /// <summary>
    /// 装备界面
    /// </summary>
    [Header("装备界面")]
    public EquipUI EquipUI;

    /// <summary>
    /// 邮件界面
    /// </summary>
    [Header("邮件界面")]
    public MailUI MailUI;

    /// <summary>
    /// 关卡入口界面
    /// </summary>
    [Header("关卡入口界面")]
    public LevelEntryUI LevelEntryUI;

    /// <summary>
    /// 关卡界面
    /// </summary>
    [Header("关卡界面")]
    public LevelUI LevelUI;

    /// <summary>
    /// 活动界面
    /// </summary>
    [Header("活动界面")]
    public ActivityUI ActivityUI;

    /// <summary>
    /// PVE界面
    /// </summary>
    [Header("PVE界面")]
    public PVEUI PVEUI;

    private void Awake()
    {
        mSingleton = this;
        // 初始化所有游戏Model数据
        InitAllModelData();
        // 所有数据初始化完成后触发一次红点运算单元计算
        RedDotManager.Singleton.MarkAllRedDotUnitDirty();
        MainUI.Open();
    }

    /// <summary>
    /// 初始化所有Model数据
    /// </summary>
    private void InitAllModelData()
    {
        // 红点系统先初始化
        RedDotManager.Singleton.Init();
        GameModel.Singleton.Init();
        // 红点初始化器依赖了系统解锁模块
        // 所以系统解锁模块必须在RedDotInitializer前初始化
        SystemUnlockModel.Singleton.Init();
        // 后初始化基础红点数据
        RedDotInitializer.Singleton.Init();
        BackpackModel.Singleton.Init();
        LevelModel.Singleton.Init();
        EquipModel.Singleton.Init();
        PVEModel.Singleton.Init();
        MailModel.Singleton.Init();
        ActivityModel.Singleton.Init();
        // 确保所有游戏Model数据初始化完成再初始化红点数据Model
        // 避免标脏相同红点运算单元反复标脏触发重复计算
        RedDotModel.Singleton.Init();
    }

    public void Update()
    {
        RedDotManager.Singleton.Update();
    }

    void OnDestroy()
    {
        RedDotManager.Singleton.Dispose();
        RedDotInitializer.Singleton.Dispose();
        DisposeAllModelData();
    }

    /// <summary>
    /// 释放所有Model数据
    /// </summary>
    private void DisposeAllModelData()
    {
        GameModel.Singleton.Dispose();
        SystemUnlockModel.Singleton.Dispose();
        BackpackModel.Singleton.Dispose();
        LevelModel.Singleton.Dispose();
        EquipModel.Singleton.Dispose();
        PVEModel.Singleton.Dispose();
        MailModel.Singleton.Dispose();
        ActivityModel.Singleton.Dispose();
        RedDotModel.Singleton.Dispose();
    }
}