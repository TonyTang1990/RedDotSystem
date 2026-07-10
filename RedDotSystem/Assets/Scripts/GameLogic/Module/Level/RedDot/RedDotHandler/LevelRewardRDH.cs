/*
 * Description:             LevelRewardRDH.cs
 * Author:                  TONYTANG
 * Create Date:             2026/04/06
 */

/// <summary>
/// LevelRewardRDH.cs
/// 关卡可领奖红点处理器
/// </summary>
public class LevelRewardRDH : DynamicRedDotHandler<int>
{
    /// <summary>
    /// 初始化红点数据
    /// </summary>
    protected override void InitRedDotDatas()
    {
        // 注意这里使用模板红点名确保动态红点名前缀树能正确构建
        var levelRewardNumRDU = RedDotUtilities.GetDynamicRedDotUnit(RedDotUnit.LEVEL_REWARD_NUM, Data);
        AddBindRedDotUnit(levelRewardNumRDU, $"关卡{Data}可领奖数量", CaculateLevelRewardNum, RedDotType.NUMBER);
    }

    /// <summary>
    /// 添加所有事件
    /// </summary>
    protected override void AddAllEvents()
    {
        base.AddAllEvents();
        AddEvent(EventId.LevelRewardNumUpdate, OnLevelRewardNumUpdate);
    }

    /// <summary>
    /// 关卡可领奖数量更新事件
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
    /// 计算关卡可领奖数量
    /// </summary>
    /// <returns></returns>
    protected int CaculateLevelRewardNum()
    {
        var levelRewardNum = LevelModel.Singleton.GetLevelRewardNum(Data);
        return levelRewardNum;
    }
}