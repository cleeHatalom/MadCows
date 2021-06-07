/* 
 * Copyright 2021 (C) Hatalom Corporation - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 */

using UnityEngine;

public interface IEventSubscriber
{ 
    void Register();
    void Unregister();
}

public abstract class SubscriberMonoBehaviour : MonoBehaviour, IEventSubscriber
{
    void Start()
    {
        Register();
    }

    void OnDestroy()
    {
        Unregister();
    }
    public abstract void Register();
    public abstract void Unregister();
}