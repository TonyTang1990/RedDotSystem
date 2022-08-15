/*
 * Description:             MainUI.cs
 * Author:                  TONYTANG
 * Create Date:             2022/08/14
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// MainUI.cs
/// 主UI
/// </summary>
public class MainUI : MonoBehaviour
{
    /// <summary>
    /// 菜单组
    /// </summary>
    [Header("菜单组")]
    public GameObject MenuGroup;

    /// <summary>
    /// 菜单按钮
    /// </summary>
    [Header("菜单按钮")]
    public Button BtnMenu;

    /// <summary>
    /// 菜单红点
    /// </summary>
    [Header("菜单红点")]
    public RedDotWidget MenuRedDot;

    /// <summary>
    /// 邮件按钮
    /// </summary>
    [Header("邮件按钮")]
    public Button BtnMail;

    /// <summary>
    /// 邮件红点
    /// </summary>
    [Header("邮件红点")]
    public RedDotWidget MailRedDot;

    /// <summary>
    /// 背包按钮
    /// </summary>
    [Header("背包按钮")]
    public Button BtnBackpack;

    /// <summary>
    /// 背包红点
    /// </summary>
    [Header("背包红点")]
    public RedDotWidget BackpackRedDot;

    /// <summary>
    /// 装备按钮
    /// </summary>
    [Header("装备按钮")]
    public Button BtnEquip;

    /// <summary>
    /// 装备红点
    /// </summary>
    [Header("装备红点")]
    public RedDotWidget EquipRedDot;

    /// <summary>
    /// 动态功能1按钮
    /// </summary>
    [Header("动态功能1按钮")]
    public Button BtnDynamicFunc1;

    /// <summary>
    /// 动态功能1红点
    /// </summary>
    [Header("动态功能1红点")]
    public RedDotWidget DynamicFunc1RedDot;

    /// <summary>
    /// 动态功能2按钮
    /// </summary>
    [Header("动态功能2按钮")]
    public Button BtnDynamicFunc2;

    /// <summary>
    /// 动态功能2红点
    /// </summary>
    [Header("动态功能2红点")]
    public RedDotWidget DynamicFunc2RedDot;

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