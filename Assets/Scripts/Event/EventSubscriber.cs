using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventSubscriber
{
    public EventSubscriber()
    {
        Register();
    }

    ~EventSubscriber()
    {
        Unregister();
    }

    protected abstract void Register();
    protected abstract void Unregister();
}
