/* 
 * Copyright 2021 (C) Hatalom Corporation - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentGameManager : GenericSingletonMonobehaviour<PersistentGameManager>
{
    private EventHub _eventHub = null;
    public EventHub EventHub
    {
        get
        {
            if(_eventHub == null)
            {
                _eventHub = new EventHub();
            }

            return _eventHub;
        }
    }

    private LevelManager _levelManager = null;
    public LevelManager LevelManager 
    { 
        get
        {
            if(_levelManager == null)
            {
                _levelManager = new LevelManager();
            }

            return _levelManager;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // for the future: Use a coroutine to load stuff like the levels.

        LevelManager.ReadLevelsData();

        LevelManager.DispatchLoadLevelEvent(1);
    }

    public override void OnDestroy()
    {
        if (_eventHub != null)
        {
            _eventHub.Dispose();
        }
        _eventHub = null;
        _levelManager = null;
        base.OnDestroy();
    }
}
