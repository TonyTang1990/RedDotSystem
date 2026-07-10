/*
 * Description:             MailRDInitializer.cs
 * Author:                  TONYTANG
 * Create Date:             2026/04/06
 */

/// <summary>
/// MailRDInitializer.cs
/// 邮件红点初始化器
/// </summary>
public class MailRDInitializer : FuncRDInitializer
{
    /// <summary>
    /// 初始化红点数据
    /// </summary>
    protected override void InitRedDotInfos()
    {
        AddRedDotInfo<MailUIPublicMailRDH>(RedDotNames.MAIL_UI_PUBLIC_MAIL, "邮件界面公共邮件红点");
        AddRedDotInfo<MailUIBattleMailRDH>(RedDotNames.MAIL_UI_BATTLE_MAIL, "邮件界面战斗邮件红点");        
        AddRedDotInfo<MailUIOtherMailRDH>(RedDotNames.MAIL_UI_OTHER_MAIL, "邮件界面其他邮件红点");
    }
}