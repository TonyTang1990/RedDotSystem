/*
 * Description:             EquipUI.cs
 * Author:                  TONYTANG
 * Create Date:             2022/08/14
 */

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// EquipUI.cs
/// 装备UI
/// </summary>
public class EquipUI : BaseUI
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
    protected override void OnOpen()
    {
        base.OnOpen();
        RefreshView();
    }

    /// <summary>
    /// 添加所有Listener
    /// </summary>
    protected override void AddAllListeners()
    {
        base.AddAllListeners();
        BtnClose.onClick.AddListener(OnBtnCloseClick);
        BtnAddWearableEquip.onClick.AddListener(OnBtnAddWearableEquipClick);
        BtnMinusWearableEquip.onClick.AddListener(OnBtnMinusWearableEquipClick);
        BtnAddUpgradableEquip.onClick.AddListener(OnBtnAddUpgradableEquipClick);
        BtnMinusUpgradableEquip.onClick.AddListener(OnBtnMinusUpgradableEquipClick);
    }

    /// <summary>
    /// 移除所有Listener
    /// </summary>
    protected override void RemoveAllListeners()
    {
        base.RemoveAllListeners();
        BtnClose.onClick.RemoveListener(OnBtnCloseClick);
        BtnAddWearableEquip.onClick.RemoveListener(OnBtnAddWearableEquipClick);
        BtnMinusWearableEquip.onClick.RemoveListener(OnBtnMinusWearableEquipClick);
        BtnAddUpgradableEquip.onClick.RemoveListener(OnBtnAddUpgradableEquipClick);
        BtnMinusUpgradableEquip.onClick.RemoveListener(OnBtnMinusUpgradableEquipClick);
    }

    /// <summary>
    /// 绑定所有红点名
    /// </summary>
    protected override void BindAllRedDotNames()
    {
        base.BindAllRedDotNames();
        WearableEquipRedDot.Init(RedDotNames.EQUIP_UI_WEARABLE);
        UpgradableEquipRedDot.Init(RedDotNames.EQUIP_UI_UPGRADABLE);
    }

    /// <summary>
    /// 解绑所有红点名
    /// </summary>
    protected override void UnbindAllRedDotNames()
    {
        base.UnbindAllRedDotNames();
        WearableEquipRedDot.UnbindRedDotName();
        UpgradableEquipRedDot.UnbindRedDotName();
    }

    /// <summary>
    /// 刷新显示
    /// </summary>
    private void RefreshView()
    {

    }

    /// <summary>
    /// 响应关闭按钮点击
    /// </summary>
    private void OnBtnCloseClick()
    {
        Close();
    }

    /// <summary>
    /// 响应增加可穿戴装备1按钮点击
    /// </summary>
    private void OnBtnAddWearableEquipClick()
    {
        var newWearableEquipNum = EquipModel.Singleton.WearableEquipNum + 1;
        EquipModel.Singleton.SetWearableEquipNum(newWearableEquipNum);
    }

    /// <summary>
    /// 响应减少可穿戴装备1按钮点击
    /// </summary>
    private void OnBtnMinusWearableEquipClick()
    {
        var newWearableEquipNum = EquipModel.Singleton.WearableEquipNum - 1;
        EquipModel.Singleton.SetWearableEquipNum(newWearableEquipNum);
    }

    /// <summary>
    /// 响应增加可升级装备1按钮点击
    /// </summary>
    private void OnBtnAddUpgradableEquipClick()
    {
        var newUpgradeableEquipNum = EquipModel.Singleton.UpgradeableEquipNum + 1;
        EquipModel.Singleton.SetUpgradeableEquipNum(newUpgradeableEquipNum);
    }

    /// <summary>
    /// 响应减少可升级装备1按钮点击
    /// </summary>
    private void OnBtnMinusUpgradableEquipClick()
    {
        var newUpgradeableEquipNum = EquipModel.Singleton.UpgradeableEquipNum - 1;
        EquipModel.Singleton.SetUpgradeableEquipNum(newUpgradeableEquipNum);
    }
}