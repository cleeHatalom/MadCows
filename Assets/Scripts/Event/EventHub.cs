/* 
 * Copyright 2021 (C) Hatalom Corporation - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 */

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

#region Event Args Definitions

public abstract class BaseEventArgs<EventT, ParamT0> : EventArgs where EventT : UnityEventBase
{
    public ParamT0 param0 { get; set; }
}

public abstract class BaseEventArgs<EventT, ParamT0, ParamT1> : EventArgs where EventT : UnityEventBase
{
    public ParamT0 param0 { get; set; }
    public ParamT1 param1 { get; set; }
}

public abstract class BaseEventArgs<EventT, ParamT0, ParamT1, ParamT2> : EventArgs where EventT : UnityEventBase
{
    public ParamT0 param0 { get; set; }
    public ParamT1 param1 { get; set; }
    public ParamT2 param2 { get; set; }
}

public abstract class BaseEventArgs<EventT, ParamT0, ParamT1, ParamT2, ParamT3> : EventArgs where EventT : UnityEventBase
{
    public ParamT0 param0 { get; set; }
    public ParamT1 param1 { get; set; }
    public ParamT2 param2 { get; set; }
    public ParamT3 param3 { get; set; }
}
#endregion


public class EventHub : IDisposable
{
    private Dictionary<Type, UnityEventBase> events = new Dictionary<Type, UnityEventBase>();

    public EventHub()
    {
        #region Dictonary Population

        //events.Add(typeof(OnScreenClickedEvent), new OnScreenClickedEvent());
        //events.Add(typeof(MapTargetSetEvent), new MapTargetSetEvent());
        //events.Add(typeof(OnCharacterSelectedEvent), new OnCharacterSelectedEvent());
        //events.Add(typeof(LoadLevelEvent), new LoadLevelEvent());

        #endregion
    }

    #region Raising Events
    public void RaiseEvent<EventT>() where EventT : UnityEvent
    {
        UnityEventBase invokedEvent;
        if(events.TryGetValue(typeof(EventT), out invokedEvent))
        {
            ((UnityEvent)invokedEvent).Invoke();
        }
        else
        {
            Debug.LogError(typeof(EventT).Name + " not added");
        }
    }
    
    public void RaiseEvent<EventT, T0>(BaseEventArgs<EventT, T0> args) where EventT : UnityEvent<T0>
    {
        UnityEventBase invokedEvent;
        if (events.TryGetValue(typeof(EventT), out invokedEvent))
        {
            ((UnityEvent<T0>)invokedEvent).Invoke(args.param0);
        }
        else
        {
            Debug.LogError(typeof(EventT).Name + " not added");
        }
    }

    public void RaiseEvent<EventT, T0, T1>(BaseEventArgs<EventT, T0, T1> args) where EventT : UnityEvent<T0, T1>
    {
        UnityEventBase invokedEvent;
        if (events.TryGetValue(typeof(EventT), out invokedEvent))
        {
            ((UnityEvent<T0, T1>)invokedEvent).Invoke(args.param0, args.param1);
        }
        else
        {
            Debug.LogError(typeof(EventT).Name + " not added");
        }
    }

    public void RaiseEvent<EventT, T0, T1, T2>(BaseEventArgs<EventT, T0, T1, T2> args) where EventT : UnityEvent<T0, T1, T2>
    {
        UnityEventBase invokedEvent;
        if (events.TryGetValue(typeof(EventT), out invokedEvent))
        {
            ((UnityEvent<T0, T1, T2>)invokedEvent).Invoke(args.param0, args.param1, args.param2);
        }
        else
        {
            Debug.LogError(typeof(EventT).Name + " not added");
        }
    }

    public void RaiseEvent<EventT, T0, T1, T2, T3>(BaseEventArgs<EventT, T0, T1, T2, T3> args) where EventT : UnityEvent<T0, T1, T2, T3>
    {
        UnityEventBase invokedEvent;
        if (events.TryGetValue(typeof(EventT), out invokedEvent))
        {
            ((UnityEvent<T0, T1, T2, T3>)invokedEvent).Invoke(args.param0, args.param1, args.param2, args.param3);
        }
        else
        {
            Debug.LogError(typeof(EventT).Name + " not added");
        }
    }
    #endregion

    #region Adding Listeners
    public void AddListener<EventT>(UnityAction action) where EventT : UnityEvent
    {
        EventT listeningEvent;
        if (events.ContainsKey(typeof(EventT)))
        {
            listeningEvent = (EventT)events[typeof(EventT)];
        }
        else
        {
            listeningEvent = (EventT)Activator.CreateInstance(typeof(EventT));
            events.Add(typeof(EventT), listeningEvent);
        }
        listeningEvent.AddListener(action);
    }

    public void AddListener<EventT, T0>(UnityAction<T0> action) where EventT : UnityEvent<T0>
    {
        EventT listeningEvent;
        if (events.ContainsKey(typeof(EventT)))
        {
            listeningEvent = (EventT)events[typeof(EventT)];
        }
        else
        {
            listeningEvent = (EventT)Activator.CreateInstance(typeof(EventT));
            events.Add(typeof(EventT), listeningEvent);
        }
        listeningEvent.AddListener(action);
    }

    public void AddListener<EventT, T0, T1>(UnityAction<T0, T1> action) where EventT : UnityEvent<T0, T1>
    {
        EventT listeningEvent;
        if (events.ContainsKey(typeof(EventT)))
        {
            listeningEvent = (EventT)events[typeof(EventT)];
        }
        else
        {
            listeningEvent = (EventT)Activator.CreateInstance(typeof(EventT));
            events.Add(typeof(EventT), listeningEvent);
        }
        listeningEvent.AddListener(action);
    }

    public void AddListener<EventT, T0, T1, T2>(UnityAction<T0, T1, T2> action) where EventT : UnityEvent<T0, T1, T2>
    {
        EventT listeningEvent;
        if (events.ContainsKey(typeof(EventT)))
        {
            listeningEvent = (EventT)events[typeof(EventT)];
        }
        else
        {
            listeningEvent = (EventT)Activator.CreateInstance(typeof(EventT));
            events.Add(typeof(EventT), listeningEvent);
        }
        listeningEvent.AddListener(action);
    }

    public void AddListener<EventT, T0, T1, T2, T3>(UnityAction<T0, T1, T2, T3> action) where EventT : UnityEvent<T0, T1, T2, T3>
    {
        EventT listeningEvent;
        if (events.ContainsKey(typeof(EventT)))
        {
            listeningEvent = (EventT)events[typeof(EventT)];
        }
        else
        {
            listeningEvent = (EventT)Activator.CreateInstance(typeof(EventT));
            events.Add(typeof(EventT), listeningEvent);
        }
        listeningEvent.AddListener(action);
    }
    #endregion

    #region Removing Listeners
    
    public void RemoveListener<EventT>(UnityAction action) where EventT : UnityEvent
    {
        if (events.ContainsKey(typeof(EventT)))
        {
            UnityEvent listeningEvent = (UnityEvent)events[typeof(EventT)];
            listeningEvent.RemoveListener(action);
        }
    }
    public void RemoveListener<EventT, T0>(UnityAction<T0> action) where EventT : UnityEvent<T0>
    {
        if (events.ContainsKey(typeof(EventT)))
        {
            UnityEvent<T0> listeningEvent = (UnityEvent<T0>)events[typeof(EventT)];
            listeningEvent.RemoveListener(action);
        }
    }
    public void RemoveListener<EventT, T0, T1>(UnityAction<T0, T1> action) where EventT : UnityEvent<T0, T1>
    {
        if (events.ContainsKey(typeof(EventT)))
        {
            UnityEvent<T0, T1> listeningEvent = (UnityEvent<T0, T1>)events[typeof(EventT)];
            listeningEvent.RemoveListener(action);
        }
    }
    public void RemoveListener<EventT, T0, T1, T2>(UnityAction<T0, T1, T2> action) where EventT : UnityEvent<T0, T1, T2>
    {
        if (events.ContainsKey(typeof(EventT)))
        {
            UnityEvent<T0, T1, T2> listeningEvent = (UnityEvent<T0, T1, T2>)events[typeof(EventT)];
            listeningEvent.RemoveListener(action);
        }
    }
    public void RemoveListener<EventT, T0, T1, T2, T3>(UnityAction<T0, T1, T2, T3> action) where EventT : UnityEvent<T0, T1, T2, T3>
    {
        if (events.ContainsKey(typeof(EventT)))
        {
            UnityEvent<T0, T1, T2, T3> listeningEvent = (UnityEvent<T0, T1, T2, T3>)events[typeof(EventT)];
            listeningEvent.RemoveListener(action);
        }
    }

    public void Dispose()
    {
        foreach(UnityEventBase ueb in events.Values)
        {
            ueb.RemoveAllListeners();
        }
        events.Clear();
    }
    #endregion
}

