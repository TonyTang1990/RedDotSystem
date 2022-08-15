/*
 * Description:             RedDotManager.cs
 * Author:                  TONYTANG
 * Create Date:             2022/08/14
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// RedDotManager.cs
/// 红点管理单例类
/// </summary>
public class RedDotManager : SingletonTemplate<RedDotManager>
{
    /// <summary>
    /// 标脏检测更新帧率
    /// </summary>
    private const int mDirtyUpdateIntervalFrame = 10;

    /// <summary>
    /// 经历的帧数
    /// </summary>
    private int mFramePassed;

    public RedDotManager()
    {
        mFramePassed = 0;
    }

    /// <summary>
    /// 更新
    /// </summary>
    public void Update()
    {
        mFramePassed++;
        if(mFramePassed >= mDirtyUpdateIntervalFrame)
        {
            CheckDirtyRedDot();
            mFramePassed = 0;
        }
    }

    /// <summary>
    /// 检查标脏红点
    /// </summary>
    private void CheckDirtyRedDot()
    {

    }
}