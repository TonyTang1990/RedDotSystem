/*
 * Description:             RedDotWidget.cs
 * Author:                  TONYTANG
 * Create Date:             2022/08/14
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// RedDotWidget.cs
/// 红点子组件
/// </summary>
public class RedDotWidget : MonoBehaviour
{
    /// <summary>
    /// 红点图片
    /// </summary>
    [Header("红点图片")]
    public Image ImgRedDot;

    /// <summary>
    /// 红点文本
    /// </summary>
    [Header("红点文本")]
    public Text TxtRedDotDes;

    /// <summary>
    /// 红点名
    /// </summary>
    [SerializeField, Header("红点名")]
    private string mRedDotName;

    /// <summary>
    /// 响应销毁
    /// </summary>
    public void OnDestroy()
    {
        UnbindRedDotName();
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="redDotName"></param>
    public void Init(string redDotName)
    {
        BindRedDotName(redDotName);
    }

    /// <summary>
    /// 解绑红点名
    /// </summary>
    public void UnbindRedDotName()
    {
        if(string.IsNullOrEmpty(mRedDotName))
        {
            SetActive(false);
            return;
        }
        RedDotManager.Singleton.UnbindRedDotName(mRedDotName, OnRedDotRefresh);
        mRedDotName = null;
    }

    /// <summary>
    /// 绑定红点名
    /// </summary>
    /// <param name="redDotName"></param>
    public void BindRedDotName(string redDotName = null)
    {
        UnbindRedDotName();
        mRedDotName = redDotName;
        if(string.IsNullOrEmpty(mRedDotName))
        {
            SetActive(false);
            return;
        }
        RedDotManager.Singleton.BindRedDotName(mRedDotName, OnRedDotRefresh);
        RedDotManager.Singleton.TriggerRedDotNameUpdate(mRedDotName);
    }

    /// <summary>
    /// 红点刷新回调
    /// </summary>
    /// <param name="redDotName"></param>
    /// <param name="result"></param>
    /// <param name="redDotType"></param>
    private void OnRedDotRefresh(string redDotName, int result, RedDotType redDotType)
    {
        var resultText = RedDotUtilities.GetRedDotResultText(result, redDotType);
        SetActive(result > 0);
        SetRedDotTxt(resultText);
    }

    /// <summary>
    /// 设置红点显隐
    /// </summary>
    /// <param name="active"></param>
    private void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    /// <summary>
    /// 设置红点描述
    /// </summary>
    /// <param name="redDotRes"></param>
    private void SetRedDotTxt(string redDotRes)
    {
        TxtRedDotDes.text = redDotRes;
    }
}