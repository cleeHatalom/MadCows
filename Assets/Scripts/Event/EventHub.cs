
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using EventDefinitions;

public abstract class BaseEventArgs : EventArgs
{
    public string EventName { get; protected set; }
}

public abstract class BaseEventArgs<T0> : BaseEventArgs
{
    public T0 param0 { get; set; }
}

public abstract class BaseEventArgs<T0, T1> : BaseEventArgs<T0>
{
    public T1 param1 { get; set; }
}

public abstract class BaseEventArgs<T0, T1, T2> : BaseEventArgs<T0, T1>
{
    public T2 param2 { get; set; }
}

public abstract class BaseEventArgs<T0, T1, T2, T3> : BaseEventArgs<T0, T1, T2>
{
    public T3 param3 { get; set; }
}

public class EventHub
{
    private Dictionary<string, UnityEventBase> events = new Dictionary<string, UnityEventBase>();

    public EventHub()
    {
        #region Shelved XML reading Implemenation <Revisit Later>
        /*
        //deserialize XML file
        XmlSerializer serializer = new XmlSerializer(typeof(SerializedEvents));
        string filename = "";
        /*
        eventsNoParam = new Dictionary<string, UnityEventBase>();
        eventsOneParam = new Dictionary<string, UnityEventBase>();
        eventsTwoParam = new Dictionary<string, UnityEventBase>();
        eventsThreeParam = new Dictionary<string, UnityEventBase>();
        eventsFourParam = new Dictionary<string, UnityEventBase>();
        
        #region Event Serialization
        SerializedEvents serializedEvents;
        using (Stream reader = new FileStream(filename, FileMode.Open))
        {
            // Call the Deserialize method to restore the object's state.
            serializedEvents = (SerializedEvents)serializer.Deserialize(reader);

            Dictionary<string, UnityEventBase>[] dictionaries = { eventsNoParam, eventsOneParam, eventsTwoParam, eventsThreeParam, eventsFourParam };

            foreach(SerializableEvent se in serializedEvents.events)
            {
                if(se.parameters.Count < dictionaries.Length)
                {
                    //UnityEventBase addedEvent;
                    Type[] types = new Type[se.parameters.Count];
                    for(int i =0; i < se.parameters.Count; ++i)
                    {
                        types[i] = Type.GetType(se.parameters[i]);

                        if(types[i] == null)
                        {
                            //throw message
                        }
                    }

                }
            }
        }
        */
        #endregion

        #region Manual Event Creation / Registration

        events.Add("OnScreenClicked", new OnScreenClickedEvent());
        events.Add("MapTargetSet", new MapTargetSetEvent());
        events.Add("OnCharacterSelected", new OnCharacterSelectedEvent());
        events.Add("LoadLevel", new LoadLevelEvent());

        #endregion
    }

    public void RaiseEvent(BaseEventArgs args)
    {
        UnityEventBase invokedEvent;
        if(events.TryGetValue(args.EventName, out invokedEvent))
        {
            ((UnityEvent)invokedEvent).Invoke();
        }
        else
        {
            Debug.LogError(args.EventName + " not added");
        }
    }
    
    public void RaiseEvent<T0>(BaseEventArgs<T0> args)
    {
        UnityEventBase invokedEvent;
        if (events.TryGetValue(args.EventName, out invokedEvent))
        {
            ((UnityEvent<T0>)invokedEvent).Invoke(args.param0);
        }
        else
        {
            Debug.LogError(args.EventName + " not added");
        }
    }

    public void RaiseEvent<T0, T1>(BaseEventArgs<T0, T1> args)
    {
        UnityEventBase invokedEvent;
        if (events.TryGetValue(args.EventName, out invokedEvent))
        {
            ((UnityEvent<T0, T1>)invokedEvent).Invoke(args.param0, args.param1);
        }
        else
        {
            Debug.LogError(args.EventName + " not added");
        }
    }

    public void RaiseEvent<T0, T1, T2>(BaseEventArgs<T0, T1, T2> args)
    {
        UnityEventBase invokedEvent;
        if (events.TryGetValue(args.EventName, out invokedEvent))
        {
            ((UnityEvent<T0, T1, T2>)invokedEvent).Invoke(args.param0, args.param1, args.param2);
        }
        else
        {
            Debug.LogError(args.EventName + " not added");
        }
    }

    public void RaiseEvent<T0, T1, T2, T3>(BaseEventArgs<T0, T1, T2, T3> args)
    {
        UnityEventBase invokedEvent;
        if (events.TryGetValue(args.EventName, out invokedEvent))
        {
            ((UnityEvent<T0, T1, T2, T3>)invokedEvent).Invoke(args.param0, args.param1, args.param2, args.param3);
        }
        else
        {
            Debug.LogError(args.EventName + " not added");
        }
    }

    #region Adding Listeners
    public void AddListener(string name, UnityAction action)
    {
        if (events.ContainsKey(name))
        {
            UnityEvent listeningEvent = (UnityEvent)events[name];
            listeningEvent.AddListener(action);
        }
    }
    public void AddListener<T0>(string name, UnityAction<T0> action)
    {
        if (events.ContainsKey(name))
        {
            UnityEvent<T0> listeningEvent = (UnityEvent<T0>)events[name];
            listeningEvent.AddListener(action);
        }
    }
    public void AddListener<T0, T1>(string name, UnityAction<T0, T1> action)
    {
        if (events.ContainsKey(name))
        {
            UnityEvent<T0, T1> listeningEvent = (UnityEvent<T0, T1>)events[name];
            listeningEvent.AddListener(action);
        }
    }
    public void AddListener<T0, T1, T2>(string name, UnityAction<T0, T1, T2> action)
    {
        if (events.ContainsKey(name))
        {
            UnityEvent<T0, T1, T2> listeningEvent = (UnityEvent<T0, T1, T2>)events[name];
            listeningEvent.AddListener(action);
        }
    }
    public void AddListener<T0, T1, T2, T3>(string name, UnityAction<T0, T1, T2, T3> action)
    {
        if(events.ContainsKey(name))
        {
            UnityEvent<T0, T1, T2, T3> listeningEvent = (UnityEvent<T0, T1, T2, T3>)events[name];
            listeningEvent.AddListener(action);
        }
    }
    #endregion

    #region Removing Listeners
    public void RemoveListener(string name, UnityAction action)
    {
        if (events.ContainsKey(name))
        {
            UnityEvent listeningEvent = (UnityEvent)events[name];
            listeningEvent.RemoveListener(action);
        }
    }
    public void RemoveListener<T0>(string name, UnityAction<T0> action)
    {
        if (events.ContainsKey(name))
        {
            UnityEvent<T0> listeningEvent = (UnityEvent<T0>)events[name];
            listeningEvent.RemoveListener(action);
        }
    }
    public void RemoveListener<T0, T1>(string name, UnityAction<T0, T1> action)
    {
        if (events.ContainsKey(name))
        {
            UnityEvent<T0, T1> listeningEvent = (UnityEvent<T0, T1>)events[name];
            listeningEvent.RemoveListener(action);
        }
    }
    public void RemoveListener<T0, T1, T2>(string name, UnityAction<T0, T1, T2> action)
    {
        if (events.ContainsKey(name))
        {
            UnityEvent<T0, T1, T2> listeningEvent = (UnityEvent<T0, T1, T2>)events[name];
            listeningEvent.RemoveListener(action);
        }
    }
    public void RemoveListener<T0, T1, T2, T3>(string name, UnityAction<T0, T1, T2, T3> action)
    {
        if (events.ContainsKey(name))
        {
            UnityEvent<T0, T1, T2, T3> listeningEvent = (UnityEvent<T0, T1, T2, T3>)events[name];
            listeningEvent.RemoveListener(action);
        }
    }
    #endregion
}

