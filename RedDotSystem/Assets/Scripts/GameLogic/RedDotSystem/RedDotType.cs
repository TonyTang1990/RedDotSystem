/*
 * Description:             RedDotType.cs
 * Author:                  TONYTANG
 * Create Date:             2022/08/14
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// RedDotType.cs
/// 红点类型
/// </summary>
[Flags]
public enum RedDotType
{
    NONE = 0,                                                                   // 无
    RED = 0x0001,                                                               // 纯红点
    NUMBER = 0x0002,                                                            // 数字红点
    NEW = 0x0004,                                                               // 新红点
    RED_NUMBER = RedDotType.RED | RedDotType.NUMBER,                            // 纯+数字红点
    RED_NEW = RedDotType.RED | RedDotType.NEW,                                  // 纯+新红点
    NUMBER_NEW = RedDotType.NUMBER | RedDotType.NEW,                            // 数字+新红点
    RED_NUMBER_NEW = RedDotType.RED | RedDotType.NUMBER | RedDotType.NEW,       // 纯+数字+新红点
}