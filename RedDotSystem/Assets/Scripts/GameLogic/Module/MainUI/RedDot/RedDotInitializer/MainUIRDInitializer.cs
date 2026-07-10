/*
 * Description:             MainUIRDInitializer.cs
 * Author:                  TONYTANG
 * Create Date:             2026/04/05
 */

/// <summary>
/// MainUIRDInitializer.cs
/// 主界面红点初始化器
/// </summary>
public class MainUIRDInitializer : FuncRDInitializer
{
    /// <summary>
    /// 初始化嵌套的功能红点初始化器
    /// </summary>
    protected override void InitNestedInitializers()
    {
        base.InitNestedInitializers();
        CreateNestedInitializer<BackpackRDInitializer>();
        CreateNestedInitializer<MailRDInitializer>();
        CreateNestedInitializer<EquipRDInitializer>();
        CreateNestedInitializer<LevelRDInitializer>();
    }

    /// <summary>
    /// 初始化红点数据
    /// </summary>
    protected override void InitRedDotInfos()
    {
        AddRedDotInfo(RedDotNames.MAIN_UI_MENU, "主界面菜单红点");

        AddRedDotInfo(RedDotNames.MAIN_UI_MAIL, "主界面邮件红点");

        AddRedDotInfo(RedDotNames.MAIN_UI_MENU_EQUIP, "主界面菜单装备红点");

        AddRedDotInfo(RedDotNames.MAIN_UI_LEVEL, "主界面关卡红点");

        AddRedDotInfo(RedDotNames.MAIN_UI_MENU_BACKPACK, "主界面菜单背包红点");
    }
}