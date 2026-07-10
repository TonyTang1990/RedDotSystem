/*
 * Description:             BaseEvent.cs
 * Author:                  TONYTANG
 * Create Date:             2026/04/02
 */

/// <summary>
/// BaseEvent.cs
/// 事件分发数据含EventId和param参数
/// </summary>
public class BaseEvent : IRecycle
{
    /// <summary>
    /// 事件ID
    /// </summary>
    public EventId EId
    {
        get
        {
            return mEventId;
        }
    }
    private EventId mEventId;

    /// <summary>
    /// 事件参数
    /// </summary>
    public object[] EventParams
    {
        get
        {
            return mEventParams;
        }
    }
    private object[] mEventParams;

    public BaseEvent()
    {
        mEventId = EventId.DefaultEvent;
        mEventParams = null;
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="ei"></param>
    /// <param name="eventParams"></param>
    public void Init(EventId ei, params object[] eventParams)
    {
        mEventId = ei;
        mEventParams = eventParams;
    }

    /// <summary>
    /// 入池
    /// </summary>
    public virtual void OnCreate()
    {
        ResetDatas();
    }

    /// <summary>
    /// 出池
    /// </summary>
    public virtual void OnDispose()
    {
        ResetDatas();
    }

    /// <summary>
    /// 重置数据
    /// </summary>
    protected void ResetDatas()
    {
        mEventId = EventId.DefaultEvent;
        mEventParams = null;
    }
}