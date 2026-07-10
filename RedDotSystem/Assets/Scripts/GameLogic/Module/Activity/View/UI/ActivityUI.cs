/*
 * Description:             ActivityUI.cs
 * Author:                  TONYTANG
 * Create Date:             2026/04/02
 */

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ActivityUI.cs
/// 活动UI
/// </summary>
public class ActivityUI : BaseUI
{
    /// <summary>
    /// 关闭按钮
    /// </summary>
    [Header("关闭按钮")]
    public Button BtnClose;

    /// <summary>
    /// 标题文本
    /// </summary>
    [Header("标题文本")]
    public Text TxtTitle;

    /// <summary>
    /// 活动红点1数量红点
    /// </summary>
    [Header("活动红点1数量红点")]
    public RedDotWidget ActivityRed1NumWidget;

    /// <summary>
    /// 活动红点2数量红点
    /// </summary>
    [Header("活动红点2数量红点")]
    public RedDotWidget ActivityRed2NumWidget;

    /// <summary>
    /// 活动红点1数量加1按钮
    /// </summary>
    [Header("活动红点1数量加1按钮")]
    public Button AddActivityRed1NumBtn;

    /// <summary>
    /// 活动红点1数量减1按钮
    /// </summary>
    [Header("活动红点1数量减1按钮")]
    public Button MinusActivityRed1NumBtn;

    /// <summary>
    /// 活动红点2数量加1按钮
    /// </summary>
    [Header("活动红点2数量加1按钮")]
    public Button AddActivityRed2NumBtn;

    /// <summary>
    /// 活动红点2数量减1按钮
    /// </summary>
    [Header("活动红点2数量减1按钮")]
    public Button MinusActivityRed2NumBtn;

    /// <summary>
    /// 活动数据
    /// </summary>
    private ActivityData mActivityData;

    /// <summary>
    /// 初始化数据
    /// </summary>
    /// <param name="args"></param>
    protected override void InitDatas(params object[] args)
    {
        base.InitDatas(args);
        var activityConfId = (int)args[0];
        mActivityData = ActivityModel.Singleton.GetActivityData(activityConfId);
    }

    /// <summary>
    /// 响应打开
    /// </summary>
    protected override void OnOpen()
    {
        base.OnOpen();
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
        BtnClose.onClick.AddListener(OnBtnCloseClick);
        AddActivityRed1NumBtn.onClick.AddListener(OnAddActivityRed1NumBtnClick);
        MinusActivityRed1NumBtn.onClick.AddListener(OnMinusActivityRed1NumBtnClick);
        AddActivityRed2NumBtn.onClick.AddListener(OnAddActivityRed2NumBtnClick);
        MinusActivityRed2NumBtn.onClick.AddListener(OnMinusActivityRed2NumBtnClick);
    }
    
    /// <summary>
    /// 移除所有Listener
    /// </summary>
    protected override void RemoveAllListeners()
    {
        base.RemoveAllListeners();
        BtnClose.onClick.RemoveListener(OnBtnCloseClick);
        AddActivityRed1NumBtn.onClick.RemoveListener(OnAddActivityRed1NumBtnClick);
        MinusActivityRed1NumBtn.onClick.RemoveListener(OnMinusActivityRed1NumBtnClick);
        AddActivityRed2NumBtn.onClick.RemoveListener(OnAddActivityRed2NumBtnClick);
        MinusActivityRed2NumBtn.onClick.RemoveListener(OnMinusActivityRed2NumBtnClick);
    }

    /// <summary>
    /// 绑定所有红点名
    /// </summary>
    protected override void BindAllRedDotNames()
    {
        base.BindAllRedDotNames();
        var actRedDot1EntryName = ActivityAgent.GetActRedDot1EntryName(mActivityData.ActivityConfId);
        ActivityRed1NumWidget.Init(actRedDot1EntryName);
        var actRedDot2EntryName = ActivityAgent.GetActRedDot2EntryName(mActivityData.ActivityConfId);
        ActivityRed2NumWidget.Init(actRedDot2EntryName);
    }

    /// <summary>
    /// 解绑所有红点名
    /// </summary>
    protected override void UnbindAllRedDotNames()
    {
        base.UnbindAllRedDotNames();
        ActivityRed1NumWidget.UnbindRedDotName();
        ActivityRed2NumWidget.UnbindRedDotName();
    }

    /// <summary>
    /// 刷新显示
    /// </summary>
    private void RefreshView()
    {
        TxtTitle.text = $"活动Id:{mActivityData.ActivityConfId}";
        RefreshRedDotView();
    }

    /// <summary>
    /// 刷新红点显示
    /// </summary>
    private void RefreshRedDotView()
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
    /// 响应活动红点1数量加1按钮点击
    /// </summary>
    private void OnAddActivityRed1NumBtnClick()
    {
        var newRedDot1Num = mActivityData.RedDot1Num + 1;
        mActivityData.SetRedDot1Num(newRedDot1Num);
    }

    /// <summary>
    /// 响应活动红点1数量减1按钮点击
    /// </summary>
    private void OnMinusActivityRed1NumBtnClick()
    {
        var newRedDot1Num = mActivityData.RedDot1Num - 1;
        mActivityData.SetRedDot1Num(newRedDot1Num);
    }

    /// <summary>
    /// 响应活动红点2数量加1按钮点击
    /// </summary>
    private void OnAddActivityRed2NumBtnClick()
    {
        var newRedDot2Num = mActivityData.RedDot2Num + 1;
        mActivityData.SetRedDot2Num(newRedDot2Num);
    }

    /// <summary>
    /// 响应活动红点2数量减1按钮点击
    /// </summary>
    private void OnMinusActivityRed2NumBtnClick()
    {
        var newRedDot2Num = mActivityData.RedDot2Num - 1;
        mActivityData.SetRedDot2Num(newRedDot2Num);
    }
}