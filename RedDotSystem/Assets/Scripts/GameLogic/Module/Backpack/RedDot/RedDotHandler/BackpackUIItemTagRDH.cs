/*
 * Description:             BackpackUIItemTagRDHandler.cs
 * Author:                  TONYTANG
 * Create Date:             2026/04/06
 */

/// <summary>
/// BackpackUIItemTagRDHandler.cs
/// 背包界面道具页签红点处理器
/// </summary>
public class BackpackUIItemTagRDH : RedDotHandler
{
    /// <summary>
    /// 初始化红点数据
    /// </summary>
    protected override void InitRedDotDatas()
    {
        AddBindRedDotUnit(RedDotUnit.NEW_ITEM_NUM, "新道具数", CaculateNewItemNum, RedDotType.NUMBER);
    }

    /// <summary>
    /// 添加所有事件监听
    /// </summary>
    protected override void AddAllEvents()
    {
        base.AddAllEvents();
        AddEvent(EventId.NewItemNumUpdate, OnNewItemNumUpdate);
    }


    /// <summary>
    /// 响应道具数量更新
    /// </summary>
    /// <param name="param"></param>
    protected void OnNewItemNumUpdate(object param)
    {
        RedDotManager.Singleton.MarkRedDotUnitDirty(RedDotUnit.NEW_ITEM_NUM);
    }

    /// <summary>
    /// 计算新道具数
    /// </summary>
    /// <returns></returns>
    public static int CaculateNewItemNum()
    {
        return BackpackModel.Singleton.NewItemNum;
    }
}