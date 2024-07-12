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

    private void Awake()
    {
        mSingleton = this;
        RedDotModel.Singleton.Init();
        RedDotManager.Singleton.Init();
        // 所有数据初始化完成后触发一次红点运算单元计算
        RedDotManager.Singleton.DoAllRedDotUnitCaculate();
    }

    private void Start()
    {
        MainUI.OnOpen();
    }

    public void Update()
    {
        RedDotManager.Singleton.Update();        
    }
}