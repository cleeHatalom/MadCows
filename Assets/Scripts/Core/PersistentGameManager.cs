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
                _levelManager = gameObject.AddComponent<LevelManager>();
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
