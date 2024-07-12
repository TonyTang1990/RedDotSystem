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
    public Button BtnClose;

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
    /// 当前选中页签索引文本
    /// </summary>
    [Header("当前选中页签索引文本")]
    public Text TxtSelectedTagIndex;

    /// <summary>
    /// 当前选择页签索引
    /// </summary>
    private int mSelectedTagIndex;

    /// <summary>
    /// 响应打开
    /// </summary>
    public void OnOpen()
    {
        gameObject.SetActive(true);
        mSelectedTagIndex = 1;
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
        BtnItemTag.onClick.AddListener(OnBtnItemTagClick);
        BtnResourceTag.onClick.AddListener(OnBtnResourceTagClick);
        BtnEquipTag.onClick.AddListener(OnBtnEquipTagClick);
        BtnAddNewItem.onClick.AddListener(OnBtnAddNewItemClick);
        BtnMinusNewItem.onClick.AddListener(OnBtnMinusNewItemClick);
    }

    /// <summary>
    /// 绑定所有红点名
    /// </summary>
    private void BindAllRedDotNames()
    {
        RedDotManager.Singleton.BindRedDotName(RedDotNames.BACKPACK_UI_ITEM_TAG, OnRedDotRefresh);
        RedDotManager.Singleton.BindRedDotName(RedDotNames.BACKPACK_UI_RESOURCE_TAG, OnRedDotRefresh);
        RedDotManager.Singleton.BindRedDotName(RedDotNames.BACKPACK_UI_EQUIP_TAG, OnRedDotRefresh);
    }

    /// <summary>
    /// 刷新显示
    /// </summary>
    private void RefreshView()
    {
        RefreshTagRedDotView();
        RefreshSelectedTagTextView();
    }

    /// <summary>
    /// 刷新页签红点显示
    /// </summary>
    private void RefreshTagRedDotView()
    {
        (int result, RedDotType redDotType) redDotNameResult;
        redDotNameResult = RedDotManager.Singleton.GetRedDotNameResult(RedDotNames.BACKPACK_UI_ITEM_TAG);
        OnRedDotRefresh(RedDotNames.BACKPACK_UI_ITEM_TAG, redDotNameResult.result, redDotNameResult.redDotType);

        redDotNameResult = RedDotManager.Singleton.GetRedDotNameResult(RedDotNames.BACKPACK_UI_RESOURCE_TAG);
        OnRedDotRefresh(RedDotNames.BACKPACK_UI_RESOURCE_TAG, redDotNameResult.result, redDotNameResult.redDotType);

        redDotNameResult = RedDotManager.Singleton.GetRedDotNameResult(RedDotNames.BACKPACK_UI_EQUIP_TAG);
        OnRedDotRefresh(RedDotNames.BACKPACK_UI_EQUIP_TAG, redDotNameResult.result, redDotNameResult.redDotType);
    }

    /// <summary>
    /// 刷新选中页签文本显示
    /// </summary>
    private void RefreshSelectedTagTextView()
    {
        TxtSelectedTagIndex.text = $"当前选择页签索引:{mSelectedTagIndex}";
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
        if (string.Equals(redDotName, RedDotNames.BACKPACK_UI_ITEM_TAG))
        {
            ItemTagRedDot.SetActive(result > 0);
            ItemTagRedDot.SetRedDotTxt(resultText);
        }
        else if(string.Equals(redDotName, RedDotNames.BACKPACK_UI_RESOURCE_TAG))
        {
            ResourceTagRedDot.SetActive(result > 0);
            ResourceTagRedDot.SetRedDotTxt(resultText);
        }
        else if(string.Equals(redDotName, RedDotNames.BACKPACK_UI_EQUIP_TAG))
        {
            EquipTagRedDot.SetActive(result > 0);
            EquipTagRedDot.SetRedDotTxt(resultText);
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
            var newItemNum = GameModel.Singleton.NewItemNum + 1;
            GameModel.Singleton.SetNewItemNum(newItemNum);
        }
        else if(mSelectedTagIndex == 2)
        {
            var newResourceNum = GameModel.Singleton.NewResourceNum + 1;
            GameModel.Singleton.SetNewResourceNum(newResourceNum);
        }
        else if(mSelectedTagIndex == 3)
        {
            var newEquipNum = GameModel.Singleton.NewEquipNum + 1;
            GameModel.Singleton.SetmNewEquipNum(newEquipNum);
        }
    }

    /// <summary>
    /// 响应减少新道具1按钮点击
    /// </summary>
    private void OnBtnMinusNewItemClick()
    {
        if (mSelectedTagIndex == 1)
        {
            var newItemNum = GameModel.Singleton.NewItemNum - 1;
            GameModel.Singleton.SetNewItemNum(newItemNum);
        }
        else if (mSelectedTagIndex == 2)
        {
            var newResourceNum = GameModel.Singleton.NewResourceNum - 1;
            GameModel.Singleton.SetNewResourceNum(newResourceNum);
        }
        else if (mSelectedTagIndex == 3)
        {
            var newEquipNum = GameModel.Singleton.NewEquipNum - 1;
            GameModel.Singleton.SetmNewEquipNum(newEquipNum);
        }
    }

    /// <summary>
    /// 解绑所有红点名
    /// </summary>
    private void UnbindAllRedDotNames()
    {
        RedDotManager.Singleton.UnbindRedDotName(RedDotNames.BACKPACK_UI_ITEM_TAG, OnRedDotRefresh);
        RedDotManager.Singleton.UnbindRedDotName(RedDotNames.BACKPACK_UI_RESOURCE_TAG, OnRedDotRefresh);
        RedDotManager.Singleton.UnbindRedDotName(RedDotNames.BACKPACK_UI_EQUIP_TAG, OnRedDotRefresh);
    }

    /// <summary>
    /// 移除所有监听
    /// </summary>
    private void RemoveAllListeners()
    {
        BtnClose.onClick.RemoveListener(OnBtnCloseClick);
        BtnItemTag.onClick.RemoveListener(OnBtnItemTagClick);
        BtnResourceTag.onClick.RemoveListener(OnBtnResourceTagClick);
        BtnEquipTag.onClick.RemoveListener(OnBtnEquipTagClick);
        BtnAddNewItem.onClick.RemoveListener(OnBtnAddNewItemClick);
        BtnMinusNewItem.onClick.RemoveListener(OnBtnMinusNewItemClick);
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