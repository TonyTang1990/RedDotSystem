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
    /// 设置红点显隐
    /// </summary>
    /// <param name="active"></param>
    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    /// <summary>
    /// 设置红点描述
    /// </summary>
    /// <param name="redDotRes"></param>
    public void SetRedDotTxt(string redDotRes)
    {
        TxtRedDotDes.text = redDotRes;
    }
}