/*
 * Description:             LevelUpgradeRDH.cs
 * Author:                  TONYTANG
 * Create Date:             2026/04/06
 */

/// <summary>
/// LevelUpgradeRDH.cs
/// 关卡可升级红点处理器
/// </summary>
public class LevelUpgradeRDH : DynamicRedDotHandler<int>
{
    /// <summary>
    /// 初始化红点数据
    /// </summary>
    protected override void InitRedDotDatas()
    {
        // 注意这里使用模板红点名确保动态红点名前缀树能正确构建
        var levelUpgradeRDU = RedDotUtilities.GetDynamicRedDotUnit(RedDotUnit.LEVEL_UPGRADE_NUM, Data);
        AddBindRedDotUnit(levelUpgradeRDU, $"关卡{Data}可升级数量", CaculateLevelUpgradeNum, RedDotType.NUMBER);
    }

    /// <summary>
    /// 添加所有事件
    /// </summary>
    protected override void AddAllEvents()
    {
        base.AddAllEvents();
        AddEvent(EventId.LevelUpgradeNumUpdate, OnLevelUpgradeNumUpdate);
        AddEvent(EventId.LevelRewardNumUpdate, OnLevelRewardNumUpdate);
    }

    /// <summary>
    /// 关卡可领奖数量更新事件
    /// </summary>
    /// <param name="evt"></param>
    protected void OnLevelUpgradeNumUpdate(BaseEvent evt)
    {
        var eventParams = evt.EventParams;
        var levelId = (int)eventParams[0];
        if(levelId != Data)
        {
            return;
        }
        var dynamicRedDotUnit = RedDotUtilities.GetDynamicRedDotUnit(RedDotUnit.LEVEL_UPGRADE_NUM, levelId);
        RedDotManager.Singleton.MarkRedDotUnitDirty(dynamicRedDotUnit);
    }

    /// <summary>
    /// 关卡可升级数量更新事件
    /// </summary>
    /// <param name="evt"></param>
    protected void OnLevelRewardNumUpdate(BaseEvent evt)
    {
        var eventParams = evt.EventParams;
        var levelId = (int)eventParams[0];
        if(levelId != Data)
        {
            return;
        }
        var dynamicRedDotUnit = RedDotUtilities.GetDynamicRedDotUnit(RedDotUnit.LEVEL_REWARD_NUM, levelId);
        RedDotManager.Singleton.MarkRedDotUnitDirty(dynamicRedDotUnit);
    }

    /// <summary>
    /// 计算关卡可升级数量
    /// </summary>
    /// <returns></returns>
    protected int CaculateLevelUpgradeNum()
    {
        var levelUpgradeNum = LevelModel.Singleton.GetLevelUpgradeNum(Data);
        return levelUpgradeNum;
    }
}