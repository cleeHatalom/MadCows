/* 
 * Copyright 2021 (C) Hatalom Corporation - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 */

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/**
 * TODO: The Event Args paradigm asit is is very clunky and needs a more graceful solution, possibly involving the Factory pattern
 */
namespace EventDefinitions
{
    public class OnScreenClickedEvent : UnityEvent<Vector2> { }

    public class OnScreenClickedEventArgs : BaseEventArgs<OnScreenClickedEvent, Vector2> { }

    public class MapTargetSetEvent : UnityEvent<Vector2> { }

    public class MapTargetSetEventArgs : BaseEventArgs<MapTargetSetEvent, Vector2> { }

    public class SpawnCharacterEvent : UnityEvent<Vector2Int> { }

    public class SpawnCharacterEventArgs : BaseEventArgs<SpawnCharacterEvent, Vector2Int> { }

    public class PollCharacterPositionEvent : UnityEvent<string> { }

    public class PollCharacterPositionEventArgs : BaseEventArgs<PollCharacterPositionEvent, string> { }

    public class BroadcastCharacterPositionEvent : UnityEvent<Vector2Int> { }

    public class BroadcastCharacterPositionEventArgs : BaseEventArgs<BroadcastCharacterPositionEvent, Vector2Int> { }

    public class SetCharacterPathEvent : UnityEvent<string, List<Vector2Int>> { }

    public class OnCharacterClickedEvent : UnityEvent<string> { }

    public class OnCharacterClickedEventArgs : BaseEventArgs<OnCharacterClickedEvent, string> { }

    public class OnCharacterSelectedEvent : UnityEvent<string> { }

    public class OnCharacterSelectedEventArgs : BaseEventArgs<OnCharacterSelectedEvent, string> { }

    public class LoadLevelEvent : UnityEvent<LevelsData.LevelData> { }

    public class LoadLevelEventArgs : BaseEventArgs<LoadLevelEvent, LevelsData.LevelData> { }
}