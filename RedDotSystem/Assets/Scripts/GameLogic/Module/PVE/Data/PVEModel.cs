/*
 * Description:             PVEModel.cs
 * Author:                  TONYTANG
 * Create Date:             2026/05/26
 */

/// <summary>
/// PVEModel.cs
/// PVE数据层单例类
/// </summary>
public class PVEModel : BaseModel<PVEModel>
{
    /// <summary>
    /// PVE奖励数
    /// </summary>
    public int PVERewardNum
    {
        get;
        private set;
    }

    /// <summary>
    /// 响应初始化
    /// </summary>
    protected override void OnInit()
    {
        base.OnInit();
    }

    /// <summary>
    /// 响应释放
    /// </summary>
    protected override void OnDispose()
    {
        base.OnDispose();
    }

    /// <summary>
    /// 设置PVE奖励数
    /// </summary>
    /// <param name="newPVERewardNum"></param>
    public void SetPVERewardNum(int newPVERewardNum)
    {
        newPVERewardNum = newPVERewardNum >= 0 ? newPVERewardNum : 0;
        if (PVERewardNum != newPVERewardNum)
        {
            PVERewardNum = newPVERewardNum;
            EventManager.Singleton.DispatchEvent(EventId.PVERewardNumUpdate, PVERewardNum);
        }
    }
}