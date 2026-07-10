/*
 * Description:             MainUI.cs
 * Author:                  TONYTANG
 * Create Date:             2022/08/14
 */

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// MainUI.cs
/// 主UI
/// </summary>
public class MainUI : BaseUI
{
    /// <summary>
    /// 菜单组
    /// </summary>
    [Header("菜单组")]
    public GameObject MenuGroup;

    /// <summary>
    /// 菜单按钮组件
    /// </summary>
    [Header("菜单按钮组件")]
    public CommonBtnWidget MenuBtnWidget;

    /// <summary>
    /// 邮件按钮组件
    /// </summary>
    [Header("邮件按钮组件")]
    public CommonBtnWidget MailBtnWidget;

    /// <summary>
    /// 背包按钮组件
    /// </summary>
    [Header("背包按钮组件")]
    public CommonBtnWidget BackpackBtnWidget;

    /// <summary>
    /// 装备按钮组件
    /// </summary>
    [Header("装备按钮组件")]
    public CommonBtnWidget EquipBtnWidget;

    /// <summary>
    /// 关卡入口按钮组件
    /// </summary>
    [Header("关卡入口按钮组件")]
    public CommonBtnWidget LevelEntryBtnWidget;

    /// <summary>
    /// 激活装备功能按钮组件
    /// </summary>
    [Header("激活装备功能按钮组件")]
    public Button ActiveEquipFuncBtnWidget;

    /// <summary>
    /// 取消激活装备功能按钮组件
    /// </summary>
    [Header("取消激活装备功能按钮组件")]
    public Button InactiveEquipFuncBtnWidget;

    /// <summary>
    /// 激活Level功能按钮组件
    /// </summary>
    [Header("激活Level功能按钮组件")]
    public Button ActiveLevelFuncBtnWidget;

    /// <summary>
    /// 取消激活Level功能按钮组件
    /// </summary>
    [Header("取消激活Level功能按钮组件")]
    public Button InactiveLevelFuncBtnWidget;

    /// <summary>
    /// 激活PVE功能按钮组件
    /// </summary>
    [Header("激活PVE功能按钮组件")]
    public Button ActivePVEFuncBtnWidget;

    /// <summary>
    /// 取消激活PVE功能按钮组件
    /// </summary>
    [Header("取消激活PVE功能按钮组件")]
    public Button InactivePVEFuncBtnWidget;

    /// <summary>
    /// 添加活动1按钮组件
    /// </summary>
    [Header("添加活动1按钮组件")]
    public Button AddAct1Btn;

    /// <summary>
    /// 移除活动1按钮组件
    /// </summary>
    [Header("移除活动1按钮组件")]
    public Button RemoveAct1Btn;

    /// <summary>
    /// 活动父节点
    /// </summary>
    [Header("活动父节点")]
    public Transform ActivityParent;

    /// <summary>
    /// 活动入口模板
    /// </summary>
    [Header("活动入口模板")]
    public GameObject ActivityEntryTemplate;

    /// <summary>
    /// 菜单组是否激活
    /// </summary>
    private bool mMenuGroupActive;

    /// <summary>
    /// 激活的活动配置ID列表
    /// </summary>
    private List<int> mActiveActivityConfIds;

    /// <summary>
    /// 初始化数据
    /// </summary>
    /// <param name="args"></param>
    protected override void InitDatas(params object[] args)
    {
        base.InitDatas(args);
        mMenuGroupActive = false;
        UpdateActiveActivityConfIds();
    }

    /// <summary>
    /// 响应打开
    /// </summary>
    protected override void OnOpen()
    {
        base.OnOpen();
        InitView();
        RefreshView();
    }
    
    /// <summary>
    /// 响应关闭
    /// </summary>
    protected override void OnClose()
    {
        base.OnClose();
    }

    /// <summary>
    /// 添加所有事件
    /// </summary>
    protected override void AddAllEvents()
    {
        base.AddAllEvents();
        AddEvent(EventId.ActivityDataAdd, OnActivityDataAdd);
        AddEvent(EventId.ActivityDataRemove, OnActivityDataRemove);
    }

    /// <summary>
    /// 绑定所有红点名
    /// </summary>
    protected override void BindAllRedDotNames()
    {
        base.BindAllRedDotNames();
        MenuBtnWidget.Init("菜单", OnBtnMenuClick, RedDotNames.MAIN_UI_MENU);
        MailBtnWidget.Init("邮件", OnBtnMailClick, RedDotNames.MAIN_UI_MAIL);
        BackpackBtnWidget.Init("背包", OnBtnBackpackClick, RedDotNames.MAIN_UI_MENU_BACKPACK);
        EquipBtnWidget.Init("装备", OnBtnEquipClick, RedDotNames.MAIN_UI_MENU_EQUIP);
        LevelEntryBtnWidget.Init("关卡", OnLevelEntryBtnClick, RedDotNames.MAIN_UI_LEVEL);
    }

    /// <summary>
    /// 解绑所有红点名
    /// </summary>
    protected override void UnbindAllRedDotNames()
    {
        base.UnbindAllRedDotNames();
        MenuBtnWidget.UnbindRedDotName();
        MailBtnWidget.UnbindRedDotName();
        BackpackBtnWidget.UnbindRedDotName();
        EquipBtnWidget.UnbindRedDotName();
        LevelEntryBtnWidget.UnbindRedDotName();
    }

    /// <summary>
    /// 更新激活的活动配置ID列表
    /// </summary>
    private void UpdateActiveActivityConfIds()
    {
        mActiveActivityConfIds = ActivityModel.Singleton.GetAllActiveActivityConfIds();
    }

    /// <summary>
    /// 初始化显示
    /// </summary>
    private void InitView()
    {
        ActiveEquipFuncBtnWidget.onClick.AddListener(OnActiveEquipFuncBtnClick);
        InactiveEquipFuncBtnWidget.onClick.AddListener(OnInactiveEquipFuncBtnClick);
        ActiveLevelFuncBtnWidget.onClick.AddListener(OnActiveLevelFuncBtnClick);
        InactiveLevelFuncBtnWidget.onClick.AddListener(OnInactiveLevelFuncBtnClick);
        ActivePVEFuncBtnWidget.onClick.AddListener(OnActivePVEFuncBtnClick);
        InactivePVEFuncBtnWidget.onClick.AddListener(OnInactivePVEFuncBtnClick);
        AddAct1Btn.onClick.AddListener(OnAddAct1BtnClick);
        RemoveAct1Btn.onClick.AddListener(OnRemoveAct1BtnClick);
    }

    /// <summary>
    /// 刷新显示
    /// </summary>
    private void RefreshView()
    {
        RefreshMenuGroupView();
        RefreshActivityView();
    }

    /// <summary>
    /// 刷新菜单组显示
    /// </summary>
    private void RefreshMenuGroupView()
    {
        MenuGroup.SetActive(mMenuGroupActive);
    }

    /// <summary>
    /// 刷新活动显示
    /// </summary>
    private void RefreshActivityView()
    {
        // 清空活动父节点下的所有子节点
        foreach (Transform child in ActivityParent)
        {
            Destroy(child.gameObject);
        }

        // 遍历激活的活动配置ID列表，创建活动入口
        foreach (int confId in mActiveActivityConfIds)
        {
            GameObject activityEntry = Instantiate(ActivityEntryTemplate, ActivityParent);
            activityEntry.SetActive(true);
            // 这里可以根据confId设置活动入口的显示内容，比如名称、图标等
            // 假设有一个ActivityEntryWidget组件来处理显示
            ActivityEntryWidget widget = activityEntry.GetComponent<ActivityEntryWidget>();
            if (widget != null)
            {
                widget.Init(confId);
            }
        }
    }

    /// <summary>
    /// 响应活动数据添加事件
    /// </summary>
    /// <param name="evt"></param>
    private void OnActivityDataAdd(BaseEvent evt)
    {
        UpdateActiveActivityConfIds();
        RefreshActivityView();
    }

    /// <summary>
    /// 响应活动数据移除事件
    /// </summary>
    /// <param name="evt"></param>
    private void OnActivityDataRemove(BaseEvent evt)
    {
        UpdateActiveActivityConfIds();
        RefreshActivityView();
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
        GameLauncher.Singleton.MailUI.Open();
    }

    /// <summary>
    /// 响应背包按钮点击
    /// </summary>
    private void OnBtnBackpackClick()
    {
        GameLauncher.Singleton.BackpackUI.Open();
    }

    /// <summary>
    /// 响应装备按钮点击
    /// </summary>
    private void OnBtnEquipClick()
    {
        EquipAgent.OpenEquipUI();
    }

    /// <summary>
    /// 响应关卡入口点击
    /// </summary>
    private void OnLevelEntryBtnClick()
    {
        LevelAgent.OpenLevelEntryUI();
    }

    /// <summary>
    /// 响应激活装备功能按钮点击
    /// </summary>
    private void OnActiveEquipFuncBtnClick()
    {
        SystemUnlockModel.Singleton.UpdateSystemUnlockType(SystemType.Equip, true);
    }

    /// <summary>
    /// 响应取消装备背包功能按钮点击
    /// </summary>
    private void OnInactiveEquipFuncBtnClick()
    {
        SystemUnlockModel.Singleton.UpdateSystemUnlockType(SystemType.Equip, false);
    }

    /// <summary>
    /// 响应激活Level功能按钮点击
    /// </summary>
    private void OnActiveLevelFuncBtnClick()
    {
        SystemUnlockModel.Singleton.UpdateSystemUnlockType(SystemType.Level, true);
    }

    /// <summary>
    /// 响应取消激活Level功能按钮点击
    /// </summary>
    private void OnInactiveLevelFuncBtnClick()
    {
        SystemUnlockModel.Singleton.UpdateSystemUnlockType(SystemType.Level, false);
    }
    
    /// <summary>
    /// 响应激活PVE功能按钮点击
    /// </summary>
    private void OnActivePVEFuncBtnClick()
    {
        SystemUnlockModel.Singleton.UpdateSystemUnlockType(SystemType.PVE, true);
    }

    /// <summary>
    /// 响应取消激活PVE功能按钮点击
    /// </summary>
    private void OnInactivePVEFuncBtnClick()
    {
        SystemUnlockModel.Singleton.UpdateSystemUnlockType(SystemType.PVE, false);
    }

    /// <summary>
    /// 响应激活活动1按钮点击
    /// </summary>
    private void OnAddAct1BtnClick()
    {
        ActivityModel.Singleton.AddActivityData(1);
    }

    /// <summary>
    /// 响应取消激活活动1按钮点击
    /// </summary>
    private void OnRemoveAct1BtnClick()
    {
        ActivityModel.Singleton.RemoveActivityData(1);
    }
}