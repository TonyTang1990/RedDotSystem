/*
 * Description:             RedDotUnitBindData.cs
 * Author:                  TONYTANG
 * Create Date:             2026/04/06
 */

/// <summary>
/// RedDotUnitBindData.cs
/// 红点单元绑定信息(用于记录绑定上下文数据，方便处理是否递归添加和清理操作)
/// </summary>
public class RedDotUnitBindData : IRecycle
{
    /// <summary>
    /// 红点单元名
    /// </summary>
    public string RedDotUnit
    {
        get;
        private set;
    }

    /// <summary>
    /// 是否递归绑定
    /// </summary>
    public bool IsRecursive
    {
        get;
        private set;
    }

    /// <summary>
    /// 出池
    /// </summary>
    public void OnCreate()
    {
        ResetDatas();
    }

    /// <summary>
    /// 入池
    /// </summary>
    public void OnDispose()
    {
        ResetDatas();
    }

    /// <summary>
    /// 重置数据
    /// </summary>
    protected virtual void ResetDatas()
    {
        RedDotUnit = null;
        IsRecursive = false;
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="redDotUnit"></param>
    /// <param name="isRecursive"></param>
    public void Init(string redDotUnit, bool isRecursive)
    {
        RedDotUnit = redDotUnit;
        IsRecursive = isRecursive;
    }
}