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
public class BackpackUI : BaseUI
{
    /// <summary>
    /// 关闭按钮组件
    /// </summary>
    [Header("关闭按钮组件")]
    public CommonBtnWidget CloseBtnWidget;

    /// <summary>
    /// 道具页签按钮组件
    /// </summary>
    [Header("道具页签按钮组件")]
    public CommonBtnWidget ItemTagBtnWidget;

    /// <summary>
    /// 资源页签按钮组件
    /// </summary>
    [Header("资源页签按钮组件")]
    public CommonBtnWidget ResourceTagBtnWidget;

    /// <summary>
    /// 装备页签按钮组件
    /// </summary>
    [Header("装备页签按钮组件")]
    public CommonBtnWidget EquipTagBtnWidget;

    /// <summary>
    /// 增加新道具1按钮组件
    /// </summary>
    [Header("增加新道具1按钮组件")]
    public CommonBtnWidget AddNewItemBtnWidget;

    /// <summary>
    /// 减少新道具1按钮组件
    /// </summary>
    [Header("减少新道具1按钮组件")]
    public CommonBtnWidget MinusNewItemBtnWidget;

    /// <summary>
    /// 当前选中页签索引文本
    /// </summary>
    [Header("当前选中页签索引文本")]
    public Text TxtSelectedTagIndex;

    /// <summary>
    /// 当前选择页签索引
    /// </summary>
    private int mSelectedTagIndex;

    /// <summary>
    /// 初始化数据
    /// </summary>
    /// <param name="args"></param>
    protected override void InitDatas(params object[] args)
    {
        base.InitDatas(args);
        mSelectedTagIndex = 1;
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
    /// 添加所有Listener
    /// </summary>
    protected override void AddAllListeners()
    {
        base.AddAllListeners();
        CloseBtnWidget.Init("关闭", OnBtnCloseClick);
        AddNewItemBtnWidget.Init("点击增加1个当前页签的新道具", OnBtnAddNewItemClick);
        MinusNewItemBtnWidget.Init("点击减少1个当前页签的新道具", OnBtnMinusNewItemClick);
    }
    
    /// <summary>
    /// 移除所有Listener
    /// </summary>
    protected override void RemoveAllListeners()
    {
        base.RemoveAllListeners();
        CloseBtnWidget.Init("关闭", null);
        AddNewItemBtnWidget.Init("点击增加1个当前页签的新道具", null);
        MinusNewItemBtnWidget.Init("点击减少1个当前页签的新道具", null);
    }

    /// <summary>
    /// 绑定所有红点名
    /// </summary>
    protected override void BindAllRedDotNames()
    {
        base.BindAllRedDotNames();
        ItemTagBtnWidget.Init("道具", OnBtnItemTagClick, RedDotNames.BACKPACK_UI_ITEM_TAG);
        ResourceTagBtnWidget.Init("资源", OnBtnResourceTagClick, RedDotNames.BACKPACK_UI_RESOURCE_TAG);
        EquipTagBtnWidget.Init("装备", OnBtnEquipTagClick, RedDotNames.BACKPACK_UI_EQUIP_TAG);
    }

    /// <summary>
    /// 解绑所有红点名
    /// </summary>
    protected override void UnbindAllRedDotNames()
    {
        base.UnbindAllRedDotNames();
        ItemTagBtnWidget.UnbindRedDotName();
        ResourceTagBtnWidget.UnbindRedDotName();
        EquipTagBtnWidget.UnbindRedDotName();
    }

    /// <summary>
    /// 初始化显示
    /// </summary>
    private void InitView()
    {
    }

    /// <summary>
    /// 刷新显示
    /// </summary>
    private void RefreshView()
    {
        RefreshSelectedTagTextView();
    }

    /// <summary>
    /// 刷新选中页签文本显示
    /// </summary>
    private void RefreshSelectedTagTextView()
    {
        TxtSelectedTagIndex.text = $"当前选择页签索引:{mSelectedTagIndex}";
    }

    /// <summary>
    /// 响应关闭按钮点击
    /// </summary>
    private void OnBtnCloseClick()
    {
        Close();
    }

    /// <summary>
    /// 响应道具页签按钮点击
    /// </summary>
    private void OnBtnItemTagClick()
    {
        mSelectedTagIndex = 1;
        RefreshSelectedTagTextView();
    }

    /// <summary>
    /// 响应资源页签按钮点击
    /// </summary>
    private void OnBtnResourceTagClick()
    {
        mSelectedTagIndex = 2;
        RefreshSelectedTagTextView();
    }

    /// <summary>
    /// 响应装备页签按钮点击
    /// </summary>
    private void OnBtnEquipTagClick()
    {
        mSelectedTagIndex = 3;
        RefreshSelectedTagTextView();
    }

    /// <summary>
    /// 响应增加新道具1按钮点击
    /// </summary>
    private void OnBtnAddNewItemClick()
    {
        if(mSelectedTagIndex == 1)
        {
            var newItemNum = BackpackModel.Singleton.NewItemNum + 1;
            BackpackModel.Singleton.SetNewItemNum(newItemNum);
        }
        else if(mSelectedTagIndex == 2)
        {
            var newResourceNum = BackpackModel.Singleton.NewResourceNum + 1;
            BackpackModel.Singleton.SetNewResourceNum(newResourceNum);
        }
        else if(mSelectedTagIndex == 3)
        {
            var newEquipNum = BackpackModel.Singleton.NewEquipNum + 1;
            BackpackModel.Singleton.SetmNewEquipNum(newEquipNum);
        }
    }

    /// <summary>
    /// 响应减少新道具1按钮点击
    /// </summary>
    private void OnBtnMinusNewItemClick()
    {
        if (mSelectedTagIndex == 1)
        {
            var newItemNum = BackpackModel.Singleton.NewItemNum - 1;
            BackpackModel.Singleton.SetNewItemNum(newItemNum);
        }
        else if (mSelectedTagIndex == 2)
        {
            var newResourceNum = BackpackModel.Singleton.NewResourceNum - 1;
            BackpackModel.Singleton.SetNewResourceNum(newResourceNum);
        }
        else if (mSelectedTagIndex == 3)
        {
            var newEquipNum = BackpackModel.Singleton.NewEquipNum - 1;
            BackpackModel.Singleton.SetmNewEquipNum(newEquipNum);
        }
    }
}