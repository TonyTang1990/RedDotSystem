/*
 * Description:             MailModel.cs
 * Author:                  TONYTANG
 * Create Date:             2026/04/03
 */

/// <summary>
/// MailModel.cs
/// 邮件数据层单例类
/// </summary>
public class MailModel : BaseModel<MailModel>
{
    /// <summary>
    /// 新公共邮件数
    /// </summary>
    public int NewPublicMailNum
    {
        get;
        private set;
    }

    /// <summary>
    /// 新战斗邮件数
    /// </summary>
    public int NewBattleMailNum
    {
        get;
        private set;
    }

    /// <summary>
    /// 新其他邮件数
    /// </summary>
    public int NewOtherMailNum
    {
        get;
        private set;
    }

    /// <summary>
    /// 公共邮件可领奖数
    /// </summary>
    public int NewPublicMailRewardNum
    {
        get;
        private set;
    }

    /// <summary>
    /// 战斗邮件可领奖数
    /// </summary>
    public int NewBattleMailRewardNum
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
        NewPublicMailNum = 0;
        NewBattleMailNum = 0;
        NewOtherMailNum = 0;
        NewPublicMailRewardNum = 0;
        NewBattleMailRewardNum = 0;
    }

    /// <summary>
    /// 响应释放
    /// </summary>
    protected override void OnDispose()
    {
        base.OnDispose();
        NewPublicMailNum = 0;
        NewBattleMailNum = 0;
        NewOtherMailNum = 0;
        NewPublicMailRewardNum = 0;
        NewBattleMailRewardNum = 0;
    }

    /// <summary>
    /// 设置新公共邮件数
    /// </summary>
    /// <param name="newPublicMailNum"></param>
    public void SetNewPublicMailNum(int newPublicMailNum)
    {
        newPublicMailNum = newPublicMailNum >= 0 ? newPublicMailNum : 0;
        if (NewPublicMailNum != newPublicMailNum)
        {
            NewPublicMailNum = newPublicMailNum;
            EventManager.Singleton.DispatchEvent(EventId.NewPublicMailNumUpdate, newPublicMailNum);
        }
    }

    /// <summary>
    /// 设置新战斗邮件数
    /// </summary>
    /// <param name="newBattleMailNum"></param>
    public void SetNewBattleMailNum(int newBattleMailNum)
    {
        newBattleMailNum = newBattleMailNum >= 0 ? newBattleMailNum : 0;
        if (NewBattleMailNum != newBattleMailNum)
        {
            NewBattleMailNum = newBattleMailNum;
            EventManager.Singleton.DispatchEvent(EventId.NewBattleMailNumUpdate, newBattleMailNum);
        }
    }

    /// <summary>
    /// 设置新其他邮件数
    /// </summary>
    /// <param name="newOtherMailNum"></param>
    public void SetmNewOtherMailNum(int newOtherMailNum)
    {
        newOtherMailNum = newOtherMailNum >= 0 ? newOtherMailNum : 0;
        if (NewOtherMailNum != newOtherMailNum)
        {
            NewOtherMailNum = newOtherMailNum;
            EventManager.Singleton.DispatchEvent(EventId.NewOtherMailNumUpdate, newOtherMailNum);
        }
    }

    /// <summary>
    /// 设置公共邮件可领奖数
    /// </summary>
    /// <param name="publicMailRewardNum"></param>
    public void SetPublicMailRewardNum(int publicMailRewardNum)
    {
        publicMailRewardNum = publicMailRewardNum >= 0 ? publicMailRewardNum : 0;
        if (NewPublicMailRewardNum != publicMailRewardNum)
        {
            NewPublicMailRewardNum = publicMailRewardNum;
            EventManager.Singleton.DispatchEvent(EventId.NewPublicMailRewardNumUpdate, publicMailRewardNum);
        }
    }

    /// <summary>
    /// 设置战斗邮件可领奖数
    /// </summary>
    /// <param name="publicBattleRewardNum"></param>
    public void SetBattleMailRewardNum(int publicBattleRewardNum)
    {
        publicBattleRewardNum = publicBattleRewardNum >= 0 ? publicBattleRewardNum : 0;
        if (NewBattleMailRewardNum != publicBattleRewardNum)
        {
            NewBattleMailRewardNum = publicBattleRewardNum;
            EventManager.Singleton.DispatchEvent(EventId.NewBattleMailRewardNumUpdate, publicBattleRewardNum);
        }
    }
}