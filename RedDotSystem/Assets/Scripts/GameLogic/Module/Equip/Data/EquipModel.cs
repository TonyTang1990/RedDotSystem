/*
 * Description:             EquipModel.cs
 * Author:                  TONYTANG
 * Create Date:             2026/04/03
 */

/// <summary>
/// EquipModel.cs
/// 装备数据层单例类
/// </summary>
public class EquipModel : BaseModel<EquipModel>
{
    /// <summary>
    /// 可穿戴装备数
    /// </summary>
    public int WearableEquipNum
    {
        get;
        private set;
    }

    /// <summary>
    /// 可升级装备数
    /// </summary>
    public int UpgradeableEquipNum
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
        WearableEquipNum = 0;
        UpgradeableEquipNum = 0;
    }

    /// <summary>
    /// 响应释放
    /// </summary>
    protected override void OnDispose()
    {
        base.OnDispose();
        WearableEquipNum = 0;
        UpgradeableEquipNum = 0;
}

    /// <summary>
    /// 设置可穿戴装备数
    /// </summary>
    /// <param name="newWearableEquipNum"></param>
    public void SetWearableEquipNum(int newWearableEquipNum)
    {
        newWearableEquipNum = newWearableEquipNum >= 0 ? newWearableEquipNum : 0;
        if (WearableEquipNum != newWearableEquipNum)
        {
            WearableEquipNum = newWearableEquipNum;
            EventManager.Singleton.DispatchEvent(EventId.WearableEquipNumUpdate, newWearableEquipNum);
        }
    }

    /// <summary>
    /// 设置可升级装备数
    /// </summary>
    /// <param name="newUpgradeableEquipNum"></param>
    public void SetUpgradeableEquipNum(int newUpgradeableEquipNum)
    {
        newUpgradeableEquipNum = newUpgradeableEquipNum >= 0 ? newUpgradeableEquipNum : 0;
        if (UpgradeableEquipNum != newUpgradeableEquipNum)
        {
            UpgradeableEquipNum = newUpgradeableEquipNum;
            EventManager.Singleton.DispatchEvent(EventId.UpgradeableEquipNumUpdate, newUpgradeableEquipNum);
        }
    }
}