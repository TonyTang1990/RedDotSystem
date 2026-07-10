/*
 * Description:             BackpackModel.cs
 * Author:                  TONYTANG
 * Create Date:             2026/04/03
 */

/// <summary>
/// BackpackModel.cs
/// 背包数据层单例类
/// </summary>
public class BackpackModel : BaseModel<BackpackModel>
{
    /// <summary>
    /// 新道具数
    /// </summary>
    public int NewItemNum
    {
        get;
        private set;
    }

    /// <summary>
    /// 新资源数
    /// </summary>
    public int NewResourceNum
    {
        get;
        private set;
    }

    /// <summary>
    /// 新装备数
    /// </summary>
    public int NewEquipNum
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
        NewItemNum = 0;
        NewResourceNum = 0;
        NewEquipNum = 0;
    }

    /// <summary>
    /// 响应释放
    /// </summary>
    protected override void OnDispose()
    {
        base.OnDispose();
        NewItemNum = 0;
        NewResourceNum = 0;
        NewEquipNum = 0;
    }

    /// <summary>
    /// 设置新道具数
    /// </summary>
    /// <param name="newItemNum"></param>
    public void SetNewItemNum(int newItemNum)
    {
        newItemNum = newItemNum >= 0 ? newItemNum : 0;
        if (NewItemNum != newItemNum)
        {
            NewItemNum = newItemNum;
            EventManager.Singleton.DispatchEvent(EventId.NewItemNumUpdate, newItemNum);
        }
    }

    /// <summary>
    /// 设置新资源数
    /// </summary>
    /// <param name="newResourceNum"></param>
    public void SetNewResourceNum(int newResourceNum)
    {
        newResourceNum = newResourceNum >= 0 ? newResourceNum : 0;
        if (NewResourceNum != newResourceNum)
        {
            NewResourceNum = newResourceNum;
            EventManager.Singleton.DispatchEvent(EventId.NewResourceNumUpdate, newResourceNum);
        }
    }

    /// <summary>
    /// 设置新装备数
    /// </summary>
    /// <param name="newEquipNum"></param>
    public void SetmNewEquipNum(int newEquipNum)
    {
        newEquipNum = newEquipNum >= 0 ? newEquipNum : 0;
        if (NewEquipNum != newEquipNum)
        {
            NewEquipNum = newEquipNum;
            EventManager.Singleton.DispatchEvent(EventId.NewEquipNumUpdate, newEquipNum);
        }
    }
}