using System.Collections;
using System;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace EventsDefinitions
{
    [XmlRoot("EventHub")]
    public class SerializedEvents
    {
        [XmlArrayItem("Event")]
        public List<SerializableEvent> events;
    }

    public class SerializableEvent
    {
        public string Name;

        [XmlArray("Params")]
        [XmlArrayItem("Type")]
        public List<string> parameters;
    }

    #region Abstract Factories
    public abstract class SerializableEventFactory
    {
        protected abstract UnityEventBase MakeEvent();

        public UnityEventBase CreateEvent()
        {
            return this.MakeEvent();
        }
    }

    public abstract class SerializableEventFactory<T0>
    {
        protected abstract UnityEvent<T0> MakeEvent();

        public UnityEvent<T0> CreateEvent()
        {
            return this.MakeEvent();
        }
    }

    public abstract class SerializableEventFactory<T0, T1>
    {
        protected abstract UnityEvent<T0, T1> MakeEvent();

        public UnityEvent<T0, T1> CreateEvent()
        {
            return this.MakeEvent();
        }
    }

    public abstract class SerializableEventFactory<T0, T1, T2>
    {
        protected abstract UnityEvent<T0, T1, T2> MakeEvent();

        public UnityEvent<T0, T1, T2> CreateEvent()
        {
            return this.MakeEvent();
        }
    }

    public abstract class SerializableEventFactory<T0, T1, T2, T3>
    {
        protected abstract UnityEvent<T0, T1, T2, T3> MakeEvent();

        public UnityEvent<T0, T1, T2, T3> CreateEvent()
        {
            return this.MakeEvent();
        }
    }
    #endregion


    /*
    public class SerializedEventArgs : EventArgs
    {
        //intentionally blank
    }

    public class SerializedEventArgs<T0> : SerializedEventArgs
    {
        public T0 param0 { get; set; }
    }

    public class SerializedEventArgs<T0, T1> : SerializedEventArgs<T0>
    {
        public T1 param1 { get; set; }
    }

    public class SerializedEventArgs<T0, T1, T2> : SerializedEventArgs<T0, T1>
    {
        public T2 param2 { get; set; }
    }

    public class SerializedEventArgs<T0, T1, T2, T3> : SerializedEventArgs<T0, T1, T2>
    {
        public T3 param3 { get; set; }
    }
    */
}