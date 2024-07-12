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
    /// 标记功能1新按钮
    /// </summary>
    [Header("标记功能1新按钮")]
    public Button BtnFunc1MarkNew;

    /// <summary>
    /// 标记功能2新按钮
    /// </summary>
    [Header("标记功能2新按钮")]
    public Button BtnFunc2MarkNew;

    /// <summary>
    /// 菜单组是否激活
    /// </summary>
    private bool mMenuGroupActive;

    /// <summary>
    /// 响应打开
    /// </summary>
    public void OnOpen()
    {
        gameObject.SetActive(true);
        mMenuGroupActive = false;
        AddAllListeners();
        BindAllRedDotNames();
        RefreshView();
    }

    /// <summary>
    /// 添加所有监听
    /// </summary>
    private void AddAllListeners()
    {
        BtnMenu.onClick.AddListener(OnBtnMenuClick);
        BtnMail.onClick.AddListener(OnBtnMailClick);
        BtnBackpack.onClick.AddListener(OnBtnBackpackClick);
        BtnEquip.onClick.AddListener(OnBtnEquipClick);
        BtnDynamicFunc1.onClick.AddListener(OnBtnDynamicFunc1Click);
        BtnDynamicFunc2.onClick.AddListener(OnBtnDynamicFunc2Click);
        BtnFunc1MarkNew.onClick.AddListener(OnBtnFunc1MarkNewClick);
        BtnFunc2MarkNew.onClick.AddListener(OnBtnFunc2MarkNewClick);
    }

    /// <summary>
    /// 绑定所有红点名
    /// </summary>
    private void BindAllRedDotNames()
    {
        RedDotManager.Singleton.BindRedDotName(RedDotNames.MAIN_UI_MENU, OnRedDotRefresh);
        RedDotManager.Singleton.BindRedDotName(RedDotNames.MAIN_UI_MAIL, OnRedDotRefresh);
        RedDotManager.Singleton.BindRedDotName(RedDotNames.MAIN_UI_MENU_BACKPACK, OnRedDotRefresh);
        RedDotManager.Singleton.BindRedDotName(RedDotNames.MAIN_UI_MENU_EQUIP, OnRedDotRefresh);
        RedDotManager.Singleton.BindRedDotName(RedDotNames.MAIN_UI_NEW_FUNC1, OnRedDotRefresh);
        RedDotManager.Singleton.BindRedDotName(RedDotNames.MAIN_UI_NEW_FUNC2, OnRedDotRefresh);
    }

    /// <summary>
    /// 刷新显示
    /// </summary>
    private void RefreshView()
    {
        RefreshMenuGroupView();
        RefreshRedDotView();
    }

    /// <summary>
    /// 刷新菜单组显示
    /// </summary>
    private void RefreshMenuGroupView()
    {
        MenuGroup.SetActive(mMenuGroupActive);
    }

    /// <summary>
    /// 刷新红点显示
    /// </summary>
    private void RefreshRedDotView()
    {
        (int result, RedDotType redDotType) redDotNameResult;
        redDotNameResult = RedDotManager.Singleton.GetRedDotNameResult(RedDotNames.MAIN_UI_MENU);
        OnRedDotRefresh(RedDotNames.MAIN_UI_MENU, redDotNameResult.result, redDotNameResult.redDotType);

        redDotNameResult = RedDotManager.Singleton.GetRedDotNameResult(RedDotNames.MAIN_UI_MAIL);
        OnRedDotRefresh(RedDotNames.MAIN_UI_MAIL, redDotNameResult.result, redDotNameResult.redDotType);

        redDotNameResult = RedDotManager.Singleton.GetRedDotNameResult(RedDotNames.MAIN_UI_MENU_BACKPACK);
        OnRedDotRefresh(RedDotNames.MAIN_UI_MENU_BACKPACK, redDotNameResult.result, redDotNameResult.redDotType);

        redDotNameResult = RedDotManager.Singleton.GetRedDotNameResult(RedDotNames.MAIN_UI_MENU_EQUIP);
        OnRedDotRefresh(RedDotNames.MAIN_UI_MENU_EQUIP, redDotNameResult.result, redDotNameResult.redDotType);

        redDotNameResult = RedDotManager.Singleton.GetRedDotNameResult(RedDotNames.MAIN_UI_NEW_FUNC1);
        OnRedDotRefresh(RedDotNames.MAIN_UI_NEW_FUNC1, redDotNameResult.result, redDotNameResult.redDotType);

        redDotNameResult = RedDotManager.Singleton.GetRedDotNameResult(RedDotNames.MAIN_UI_NEW_FUNC2);
        OnRedDotRefresh(RedDotNames.MAIN_UI_NEW_FUNC2, redDotNameResult.result, redDotNameResult.redDotType);
    }

    /// <summary>
    /// 响应红点刷新
    /// </summary>
    /// <param name="redDotName"></param>
    /// <param name="result"></param>
    /// <param name="redDotType"></param>
    private void OnRedDotRefresh(string redDotName, int result, RedDotType redDotType)
    {
        var resultText = RedDotUtilities.GetRedDotResultText(result, redDotType);
        if (string.Equals(redDotName, RedDotNames.MAIN_UI_MENU))
        {
            MenuRedDot.SetActive(result > 0);
            MenuRedDot.SetRedDotTxt(resultText);
        }
        else if (string.Equals(redDotName, RedDotNames.MAIN_UI_MAIL))
        {
            MailRedDot.SetActive(result > 0);
            MailRedDot.SetRedDotTxt(resultText);
        }
        else if (string.Equals(redDotName, RedDotNames.MAIN_UI_MENU_BACKPACK))
        {
            BackpackRedDot.SetActive(result > 0);
            BackpackRedDot.SetRedDotTxt(resultText);
        }
        else if (string.Equals(redDotName, RedDotNames.MAIN_UI_MENU_EQUIP))
        {
            EquipRedDot.SetActive(result > 0);
            EquipRedDot.SetRedDotTxt(resultText);
        }
        else if (string.Equals(redDotName, RedDotNames.MAIN_UI_NEW_FUNC1))
        {
            DynamicFunc1RedDot.SetActive(result > 0);
            DynamicFunc1RedDot.SetRedDotTxt(resultText);
        }
        else if (string.Equals(redDotName, RedDotNames.MAIN_UI_NEW_FUNC2))
        {
            DynamicFunc2RedDot.SetActive(result > 0);
            DynamicFunc2RedDot.SetRedDotTxt(resultText);
        }
    }

    /// <summary>
    /// 响应菜单按钮点击
    /// </summary>
    private void OnBtnMenuClick()
    {
        mMenuGroupActive = !mMenuGroupActive;
        RefreshMenuGroupView();
    }

    /// <summary>
    /// 响应邮件按钮点击
    /// </summary>
    private void OnBtnMailClick()
    {
        GameLauncher.Singleton.MailUI.OnOpen();
    }

    /// <summary>
    /// 响应背包按钮点击
    /// </summary>
    private void OnBtnBackpackClick()
    {
        GameLauncher.Singleton.BackpackUI.OnOpen();
    }

    /// <summary>
    /// 响应装备按钮点击
    /// </summary>
    private void OnBtnEquipClick()
    {
        GameLauncher.Singleton.EquipUI.OnOpen();
    }

    /// <summary>
    /// 响应动态功能1按钮点击
    /// </summary>
    private void OnBtnDynamicFunc1Click()
    {
        GameModel.Singleton.SetNewFunc1(false);
    }

    /// <summary>
    /// 响应动态功能2按钮点击
    /// </summary>
    private void OnBtnDynamicFunc2Click()
    {
        GameModel.Singleton.SetNewFunc2(false);
    }

    /// <summary>
    /// 响应标记功能1新按钮点击
    /// </summary>
    private void OnBtnFunc1MarkNewClick()
    {
        GameModel.Singleton.SetNewFunc1(true);
    }

    /// <summary>
    /// 响应标记功能2新按钮点击
    /// </summary>
    private void OnBtnFunc2MarkNewClick()
    {
        GameModel.Singleton.SetNewFunc2(true);
    }

    /// <summary>
    /// 解绑所有红点名
    /// </summary>
    private void UnbindAllRedDotNames()
    {
        RedDotManager.Singleton.UnbindRedDotName(RedDotNames.MAIN_UI_MENU, OnRedDotRefresh);
        RedDotManager.Singleton.UnbindRedDotName(RedDotNames.MAIN_UI_MAIL, OnRedDotRefresh);
        RedDotManager.Singleton.UnbindRedDotName(RedDotNames.MAIN_UI_MENU_BACKPACK, OnRedDotRefresh);
        RedDotManager.Singleton.UnbindRedDotName(RedDotNames.MAIN_UI_MENU_EQUIP, OnRedDotRefresh);
        RedDotManager.Singleton.UnbindRedDotName(RedDotNames.MAIN_UI_NEW_FUNC1, OnRedDotRefresh);
        RedDotManager.Singleton.UnbindRedDotName(RedDotNames.MAIN_UI_NEW_FUNC2, OnRedDotRefresh);
    }

    /// <summary>
    /// 移除所有监听
    /// </summary>
    private void RemoveAllListeners()
    {
        BtnMenu.onClick.RemoveListener(OnBtnMenuClick);
        BtnMail.onClick.RemoveListener(OnBtnMailClick);
        BtnBackpack.onClick.RemoveListener(OnBtnBackpackClick);
        BtnEquip.onClick.RemoveListener(OnBtnEquipClick);
        BtnDynamicFunc1.onClick.RemoveListener(OnBtnDynamicFunc1Click);
        BtnDynamicFunc2.onClick.RemoveListener(OnBtnDynamicFunc2Click);
        BtnFunc1MarkNew.onClick.RemoveListener(OnBtnFunc1MarkNewClick);
        BtnFunc2MarkNew.onClick.RemoveListener(OnBtnFunc2MarkNewClick);
    }

    /// <summary>
    /// 响应关闭
    /// </summary>
    public void OnClose()
    {
        gameObject.SetActive(false);
        UnbindAllRedDotNames();
        RemoveAllListeners();
    }
}