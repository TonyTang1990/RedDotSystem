/*
 * Description:             RedDotUtilities.cs
 * Author:                  TONYTANG
 * Create Date:             2022/08/14
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// RedDotUtilities.cs
/// 红点工具类
/// </summary>
public static class RedDotUtilities
{
    #region 红点辅助方法部分
    /// <summary>
    /// 红点名拼接(用于动态红点名名字拼接)
    /// </summary>
    /// <param name="parentRedDotName"></param>
    /// <param name="childRedDotName"></param>
    /// <returns></returns>
    public static string ConcateRedDotName(string parentRedDotName, string childRedDotName)
    {
        return $"{parentRedDotName}{RedDotModel.Separator}{childRedDotName}";
    }

    /// <summary>
    /// 获取模板红点名(用于模板动态红点名生成)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="redDotNamePrefix"></param>
    /// <param name="postFix"></param>
    /// <returns></returns>
    public static string GetTemplateRDName<T>(string redDotNamePrefix, T postFix)
    {
        return string.Format(redDotNamePrefix, postFix);
    }

    /// <summary>
    /// 获取动态红点单元
    /// </summary>
    /// <param name="redDotUnitPrefix"></param>
    /// <param name="postFix"></param>
    /// <returns></returns>
    public static string GetDynamicRedDotUnit<T>(string redDotUnitPrefix, T postFix)
    {
        return $"D_{redDotUnitPrefix}_{postFix}";
    }

    /// <summary>
    /// 获取指定红点数量和类型的文本显示
    /// Note:
    /// 红点显示类型优先级:
    /// 新 > 纯数字 > 纯红点
    /// </summary>
    /// <param name="result"></param>
    /// <param name="redDotType"></param>
    /// <returns></returns>
    public static string GetRedDotResultText(int result, RedDotType redDotType)
    {
        if(result <= 0)
        {
            return string.Empty;
        }
        var redDotText = string.Empty;
        if((redDotType & RedDotType.NEW) != RedDotType.NONE)
        {
            redDotText = "新";
        }
        else if((redDotType & RedDotType.NUMBER) != RedDotType.NONE)
        {
            redDotText = result.ToString();
        }
        return redDotText;
    }

    /// <summary>
    /// 获取红点名深度
    /// </summary>
    /// <param name="redDotName"></param>
    /// <returns></returns>
    public static int GetRedDotNameDepth(string redDotName)
    {
        if(string.IsNullOrEmpty(redDotName))
        {
            return 0;
        }
        var nameSplitArray = redDotName.Split(RedDotModel.Separator);
        return nameSplitArray != null ? nameSplitArray.Length : 0;
    }
    #endregion
}