/*
 * Description:             LevelRDInitializer.cs
 * Author:                  TONYTANG
 * Create Date:             2026/04/06
 */

/// <summary>
/// LevelRDInitializer.cs
/// 关卡红点初始化器
/// </summary>
public class LevelRDInitializer : SystemRDInitializer
{
    /// <summary>
    /// 系统类型
    /// </summary>
    protected override SystemType SystemType
    {
        get
        {
            return SystemType.Level;
        }
    }

    /// <summary>
    /// 初始化嵌套的功能红点初始化器
    /// </summary>
    protected override void InitNestedInitializers()
    {
        base.InitNestedInitializers();
        // 测试嵌套初始化器功能
        CreateNestedInitializer<PVERDInitializer>();
    }

    /// <summary>
    /// 初始化红点数据
    /// </summary>
    protected override void InitRedDotInfos()
    {
        AddRedDotInfo(RedDotNames.LEVEL_PVE_ENTRY, "关卡PVE入口红点");

        // 同类型，动态N个的红点事例1
        for(int levelId = 1; levelId <= LevelModel.MaxLevelId; levelId++)
        {
            // 注意这里使用模板红点名确保动态红点名前缀树能正确构建
            var levelEntryRedDotName = RedDotUtilities.GetTemplateRDName(RedDotNames.LEVEL_ENTRY, levelId);
            AddRedDotInfo(levelEntryRedDotName, $"关卡:{levelId}入口红点");

            // 注意这里使用模板红点名确保动态红点名前缀树能正确构建
            var levelUpgradeRedDotName = RedDotUtilities.GetTemplateRDName(RedDotNames.LEVEL_UPGRADE, levelId);
            AddDynamicRedDotInfo<LevelUpgradeRDH, int>(levelUpgradeRedDotName, $"关卡:{levelId}可升级红点", levelId);

            // 注意这里使用模板红点名确保动态红点名前缀树能正确构建
            var levelRewardRedDotName = RedDotUtilities.GetTemplateRDName(RedDotNames.LEVEL_REWARD, levelId);
            AddDynamicRedDotInfo<LevelRewardRDH, int>(levelRewardRedDotName, $"关卡:{levelId}可领奖红点", levelId);
        }
    }
}