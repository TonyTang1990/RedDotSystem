/*
 * Description:             DynamicRedDotHandler.cs
 * Author:                  TONYTANG
 * Create Date:             2026/04/06
 */

/// <summary>
/// DynamicRedDotHandler.cs
/// 泛型动态红点处理器
/// Note:
/// 用于支持动态带参的红点处理器创建
/// 所有动态带参的红点处理器都继承这个
/// </summary>
public abstract class DynamicRedDotHandler<T> : RedDotHandler
{
    /// <summary>
    /// 动态数据
    /// </summary>
    public T Data
    {
        get;
        set;
    }

    /// <summary>
    /// 重置数据
    /// </summary>
    protected override void ResetDatas()
    {
        base.ResetDatas();
    }
}