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

    /// <summary>
    /// 响应打开
    /// </summary>
    public void OnOpen()
    {
        gameObject.SetActive(true);
        AddAllListeners();
        BindAllRedDotNames();
        RefreshView();
    }

    /// <summary>
    /// 添加所有监听
    /// </summary>
    private void AddAllListeners()
    {
        BtnClose.onClick.AddListener(OnBtnCloseClick);
        BtnAddWearableEquip.onClick.AddListener(OnBtnAddWearableEquipClick);
        BtnMinusWearableEquip.onClick.AddListener(OnBtnMinusWearableEquipClick);
        BtnAddUpgradableEquip.onClick.AddListener(OnBtnAddUpgradableEquipClick);
        BtnMinusUpgradableEquip.onClick.AddListener(OnBtnMinusUpgradableEquipClick);
    }

    /// <summary>
    /// 绑定所有红点名
    /// </summary>
    private void BindAllRedDotNames()
    {
        RedDotManager.Singleton.BindRedDotName(RedDotNames.EQUIP_UI_WEARABLE, OnRedDotRefresh);
        RedDotManager.Singleton.BindRedDotName(RedDotNames.EQUIP_UI_UPGRADABLE, OnRedDotRefresh);
    }

    /// <summary>
    /// 刷新显示
    /// </summary>
    private void RefreshView()
    {
        RefreshRedDotView();
    }

    /// <summary>
    /// 刷新红点显示
    /// </summary>
    private void RefreshRedDotView()
    {
        (int result, RedDotType redDotType) redDotNameResult;
        redDotNameResult = RedDotManager.Singleton.GetRedDotNameResult(RedDotNames.EQUIP_UI_WEARABLE);
        OnRedDotRefresh(RedDotNames.EQUIP_UI_WEARABLE, redDotNameResult.result, redDotNameResult.redDotType);

        redDotNameResult = RedDotManager.Singleton.GetRedDotNameResult(RedDotNames.EQUIP_UI_UPGRADABLE);
        OnRedDotRefresh(RedDotNames.EQUIP_UI_UPGRADABLE, redDotNameResult.result, redDotNameResult.redDotType);
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
        if (string.Equals(redDotName, RedDotNames.EQUIP_UI_WEARABLE))
        {
            WearableEquipRedDot.SetActive(result > 0);
            WearableEquipRedDot.SetRedDotTxt(resultText);
        }
        else if (string.Equals(redDotName, RedDotNames.EQUIP_UI_UPGRADABLE))
        {
            UpgradableEquipRedDot.SetActive(result > 0);
            UpgradableEquipRedDot.SetRedDotTxt(resultText);
        }
    }

    /// <summary>
    /// 响应关闭按钮点击
    /// </summary>
    private void OnBtnCloseClick()
    {
        OnClose();
    }

    /// <summary>
    /// 响应增加可穿戴装备1按钮点击
    /// </summary>
    private void OnBtnAddWearableEquipClick()
    {
        var newWearableEquipNum = GameModel.Singleton.WearableEquipNum + 1;
        GameModel.Singleton.SetWearableEquipNum(newWearableEquipNum);
    }

    /// <summary>
    /// 响应减少可穿戴装备1按钮点击
    /// </summary>
    private void OnBtnMinusWearableEquipClick()
    {
        var newWearableEquipNum = GameModel.Singleton.WearableEquipNum - 1;
        GameModel.Singleton.SetWearableEquipNum(newWearableEquipNum);
    }

    /// <summary>
    /// 响应增加可升级装备1按钮点击
    /// </summary>
    private void OnBtnAddUpgradableEquipClick()
    {
        var newUpgradeableEquipNum = GameModel.Singleton.UpgradeableEquipNum + 1;
        GameModel.Singleton.SetUpgradeableEquipNum(newUpgradeableEquipNum);
    }

    /// <summary>
    /// 响应减少可升级装备1按钮点击
    /// </summary>
    private void OnBtnMinusUpgradableEquipClick()
    {
        var newUpgradeableEquipNum = GameModel.Singleton.UpgradeableEquipNum - 1;
        GameModel.Singleton.SetUpgradeableEquipNum(newUpgradeableEquipNum);
    }

    /// <summary>
    /// 解绑所有红点名
    /// </summary>
    private void UnbindAllRedDotNames()
    {
        RedDotManager.Singleton.UnbindRedDotName(RedDotNames.EQUIP_UI_WEARABLE, OnRedDotRefresh);
        RedDotManager.Singleton.UnbindRedDotName(RedDotNames.EQUIP_UI_UPGRADABLE, OnRedDotRefresh);
    }

    /// <summary>
    /// 移除所有监听
    /// </summary>
    private void RemoveAllListeners()
    {
        BtnClose.onClick.RemoveListener(OnBtnCloseClick);
        BtnAddWearableEquip.onClick.RemoveListener(OnBtnAddWearableEquipClick);
        BtnMinusWearableEquip.onClick.RemoveListener(OnBtnMinusWearableEquipClick);
        BtnAddUpgradableEquip.onClick.RemoveListener(OnBtnAddUpgradableEquipClick);
        BtnMinusUpgradableEquip.onClick.RemoveListener(OnBtnMinusUpgradableEquipClick);
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