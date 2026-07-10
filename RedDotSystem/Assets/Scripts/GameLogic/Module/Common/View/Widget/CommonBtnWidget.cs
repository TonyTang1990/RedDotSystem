/*
 * Description:             CommonBtnWidget.cs
 * Author:                  TONYTANG
 * Create Date:             2026/05/22
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// CommonBtnWidget.cs
/// 通用按钮组件
/// </summary>
public class CommonBtnWidget : MonoBehaviour
{
    /// <summary>
    /// 按钮
    /// </summary>
    [Header("按钮")]
    public Button Btn;

    /// <summary>
    /// 文本
    /// </summary>
    [Header("文本")]
    public Text Txt;

    /// <summary>
    /// 红点组件
    /// </summary>
    [Header("红点组件")]
    public RedDotWidget RDWidget;

    /// <summary>
    /// 文本
    /// </summary>
    private string mTxt;

    /// <summary>
    /// 点击回调
    /// </summary>
    private Action mClickCb;

    /// <summary>
    /// 红点名
    /// </summary>
    private string mRedDotName;

    private void Awake()
    {
        AddAllListeners();
    }

    /// <summary>
    /// 添加所有监听
    /// </summary>
    private void AddAllListeners()
    {
        Btn.onClick.AddListener(OnBtnClick);
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="txt">文本</param>
    /// <param name="clickCb">点击回调</param>
    /// <param name="redDotName">红点名</param>
    public void Init(string txt, Action clickCb = null, string redDotName = null)
    {
        mTxt = txt;
        mClickCb = clickCb;
        mRedDotName = redDotName;
        BindRedDotName();
        RefreshView();
    }

    /// <summary>
    /// 解绑红点
    /// </summary>
    public void UnbindRedDotName()
    {
        mRedDotName = null;
        RDWidget.UnbindRedDotName();
    }

    /// <summary>
    /// 添加所有红点绑定
    /// </summary>
    private void BindRedDotName()
    {
        RDWidget.Init(mRedDotName);
    }

    /// <summary>
    /// 刷新显示
    /// </summary>
    private void RefreshView()
    {
        Txt.text = mTxt;
    }

    /// <summary>
    /// 响应按钮点击
    /// </summary>
    private void OnBtnClick()
    {
        mClickCb?.Invoke();
    }
}