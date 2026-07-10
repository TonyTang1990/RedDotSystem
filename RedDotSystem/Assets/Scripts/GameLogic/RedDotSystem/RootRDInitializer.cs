/*
 * Description:             RootRDInitializer.cs
 * Author:                  TONYTANG
 * Create Date:             2026/04/05
 */

/// <summary>
/// RootRDInitializer.cs
/// 红点根初始化器(用于确保从根部开始创建和反向清理工作)
/// </summary>
public class RootRDInitializer : FuncRDInitializer
{
    /// <summary>
    /// 初始化嵌套的功能红点初始化器
    /// </summary>
    protected override void InitNestedInitializers()
    {
        base.InitNestedInitializers();
        CreateNestedInitializer<MainUIRDInitializer>();
    }

    /// <summary>
    /// 初始化红点数据
    /// </summary>
    protected override void InitRedDotInfos()
    {
        
    }
}