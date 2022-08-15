/*
 * Description:             BackpackUI.cs
 * Author:                  TONYTANG
 * Create Date:             2022/08/14
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// BackpackUI.cs
/// 背包UI
/// </summary>
public class BackpackUI : MonoBehaviour
{
    /// <summary>
    /// 关闭按钮
    /// </summary>
    [Header("关闭按钮")]
    public Button BtnMenu;

    /// <summary>
    /// 道具页签按钮
    /// </summary>
    [Header("道具页签按钮")]
    public Button BtnItemTag;

    /// <summary>
    /// 道具页签红点
    /// </summary>
    [Header("道具页签红点")]
    public RedDotWidget ItemTagRedDot;

    /// <summary>
    /// 资源页签按钮
    /// </summary>
    [Header("资源页签按钮")]
    public Button BtnResourceTag;

    /// <summary>
    /// 资源页签红点
    /// </summary>
    [Header("资源页签红点")]
    public RedDotWidget ResourceTagRedDot;

    /// <summary>
    /// 装备页签按钮
    /// </summary>
    [Header("装备页签按钮")]
    public Button BtnEquipTag;

    /// <summary>
    /// 装备页签红点
    /// </summary>
    [Header("装备页签红点")]
    public RedDotWidget EquipTagRedDot;

    /// <summary>
    /// 增加新道具1按钮
    /// </summary>
    [Header("增加新道具1按钮")]
    public Button BtnAddNewItem;

    /// <summary>
    /// 减少新道具1按钮
    /// </summary>
    [Header("减少新道具1按钮")]
    public Button BtnMinusNewItem;

    /// <summary>
    /// 响应打开
    /// </summary>
    public void OnOpen()
    {
        AddAllListeners();
        BindAllRedDotNames();
        RefreshView();
    }

    /// <summary>
    /// 添加所有监听
    /// </summary>
    private void AddAllListeners()
    {

    }

    /// <summary>
    /// 绑定所有红点名
    /// </summary>
    private void BindAllRedDotNames()
    {

    }

    /// <summary>
    /// 刷新显示
    /// </summary>
    private void RefreshView()
    {

    }

    /// <summary>
    /// 解绑所有红点名
    /// </summary>
    private void UnbindAllRedDotNames()
    {

    }

    /// <summary>
    /// 移除所有监听
    /// </summary>
    private void RemoveAllListeners()
    {

    }

    /// <summary>
    /// 响应关闭
    /// </summary>
    public void OnClose()
    {
        UnbindAllRedDotNames();
        RemoveAllListeners();
    }
}