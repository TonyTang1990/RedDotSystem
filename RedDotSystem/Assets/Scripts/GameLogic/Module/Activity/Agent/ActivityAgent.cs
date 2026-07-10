/*
 * Description:             ActivityAgent.cs
 * Author:                  TONYTANG
 * Create Date:             2026/07/09
 */

/// <summary>
/// ActivityAgent.cs
/// 活动代理类
/// </summary>
public static class ActivityAgent
{
    /// <summary>
    /// 获取活动入口红点名称
    /// </summary>
    /// <param name="activityConfId"></param>
    /// <returns></returns>
    public static string GetActEntryRedDotName(int activityConfId)
    {
        return RedDotUtilities.GetTemplateRDName(RedDotNames.ACT_ENTRY_TEMPLATE, activityConfId);
    }

    /// <summary>
    /// 获取活动红点1入口红点名称
    /// </summary>
    /// <param name="activityConfId"></param>
    /// <returns></returns>
    public static string GetActRedDot1EntryName(int activityConfId)
    {
        return RedDotUtilities.GetTemplateRDName(RedDotNames.ACT_RED_DOT_1_ENTRY_TEMPLATE, activityConfId);
    }

    /// <summary>
    /// 获取活动红点2入口红点名称
    /// </summary>
    /// <param name="activityConfId"></param>
    /// <returns></returns>
    public static string GetActRedDot2EntryName(int activityConfId)
    {
        return RedDotUtilities.GetTemplateRDName(RedDotNames.ACT_RED_DOT_2_ENTRY_TEMPLATE, activityConfId);
    }
}