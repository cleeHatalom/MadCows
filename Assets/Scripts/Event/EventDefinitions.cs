
using UnityEngine;
using UnityEngine.Events;

namespace EventDefinitions
{
    public class OnScreenClickedEvent : UnityEvent<Vector2> {}

    public class OnScreenClickedEventArgs : BaseEventArgs<Vector2>
    {
        public OnScreenClickedEventArgs()
        {
            EventName = "OnScreenClicked";
        }
    }

    public class MapTargetSetEvent : UnityEvent<Vector2>
    {
    }

    public class MapTargetSetEventArgs : BaseEventArgs<Vector2>
    {
        public MapTargetSetEventArgs()
        {
            EventName = "MapTargetSet";
        }
    }

    public class OnCharacterSelectedEvent : UnityEvent<string>
    {
    }

    public class OnCharacterSelectedEventArgs : BaseEventArgs<string>
    {
        public OnCharacterSelectedEventArgs()
        {
            EventName = "OnCharacterSelected";
        }
    }
    
    public class LoadLevelEvent : UnityEvent<LevelsData.LevelData> { }

    public class LoadLevelEventArgs : BaseEventArgs<LevelsData.LevelData>
    {
        public LoadLevelEventArgs()
        {
            EventName = "LoadLevel";
        }
    }
}