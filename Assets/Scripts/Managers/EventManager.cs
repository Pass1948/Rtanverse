using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    public delegate void EventDelegate<T>(T cEvent) where T : CommonEventBase;
    private delegate void EventDelegate(CommonEventBase cEvent);

    private Dictionary<Type, EventDelegate> _dicDelegate = new();
    private Dictionary<Delegate, EventDelegate> _dicDelegateLookup = new();

    public void Clean()
    {
        _dicDelegate.Clear();
        _dicDelegateLookup.Clear();
    }

    public void AddListener<T>(EventDelegate<T> Callbace) where T : CommonEventBase
    {
        EventDelegate internalDelegate = (e) => Callbace((T)e);
        _dicDelegateLookup[Callbace] = internalDelegate;

        Type type = typeof(T);
        if (!_dicDelegate.TryGetValue(type, out EventDelegate tempDelegate))
        {
            _dicDelegate[type] = tempDelegate += internalDelegate;      // 있으면 기존 + 추가
        }
        else
        {
            _dicDelegate[type] += internalDelegate;                     // 없으면 새로 추가
        }
    }

    public void DelListener<T>(EventDelegate<T> Callback) where T : CommonEventBase
    {
        if (_dicDelegateLookup.TryGetValue(Callback, out EventDelegate internalDelegate))
        {
            Type type = typeof(T);
            if (_dicDelegate.TryGetValue(type, out EventDelegate tempDelegate))
            {
                tempDelegate -= internalDelegate;
                if (tempDelegate == null)
                {
                    _dicDelegate.Remove(type);
                }
                else
                {
                    _dicDelegate[type] = tempDelegate;
                }
            }
        }

        _dicDelegateLookup.Remove(Callback);
    }

    public static void ExecuteEvent(CommonEventBase cEvent)
    {
        if (Instance._dicDelegate.TryGetValue(cEvent.GetType(), out EventDelegate Callback))
        {
            Callback.Invoke(cEvent);
        }
    }
}
