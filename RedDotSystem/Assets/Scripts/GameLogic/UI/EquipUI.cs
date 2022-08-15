/*
 * Description:             EquipUI.cs
 * Author:                  TONYTANG
 * Create Date:             2022/08/14
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// EquipUI.cs
/// 装备UI
/// </summary>
public class EquipUI : MonoBehaviour
{
    /// <summary>
    /// 关闭按钮
    /// </summary>
    [Header("关闭按钮")]
    public Button BtnClose;

    /// <summary>
    /// 增加可穿戴装备1按钮
    /// </summary>
    [Header("增加可穿戴装备1按钮")]
    public Button BtnAddWearableEquip;

    /// <summary>
    /// 减少可穿戴装备1按钮
    /// </summary>
    [Header("减少可穿戴装备1按钮")]
    public Button BtnMinusWearableEquip;

    /// <summary>
    /// 可穿戴装备红点
    /// </summary>
    [Header("可穿戴装备红点")]
    public RedDotWidget WearableEquipRedDot;

    /// <summary>
    /// 增加可升级装备1按钮
    /// </summary>
    [Header("增加可升级装备1按钮")]
    public Button BtnAddUpgradableEquip;

    /// <summary>
    /// 减少可升级装备1按钮
    /// </summary>
    [Header("减少可升级装备1按钮")]
    public Button BtnMinusUpgradableEquip;

    /// <summary>
    /// 可升级装备红点
    /// </summary>
    [Header("可升级装备红点")]
    public RedDotWidget UpgradableEquipRedDot;
}