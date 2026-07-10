/*
 * Description:             PVERDInitializer.cs
 * Author:                  TONYTANG
 * Create Date:             2026/05/26
 */

/// <summary>
/// PVERDInitializer.cs
/// PVE功能红点初始化器
/// </summary>
public class PVERDInitializer : SystemRDInitializer
{
    /// <summary>
    /// 系统类型
    /// </summary>
    protected override SystemType SystemType
    {
        get
        {
            return SystemType.PVE;
        }
    }

    /// <summary>
    /// 初始化红点信息
    /// </summary>
    protected override void InitRedDotInfos()
    {
        AddRedDotInfo<PVERewardRDH>(RedDotNames.PVE_REWARD_ENTRY, "PVE奖励入口红点");
    }
}