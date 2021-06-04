using EventDefinitions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private Dictionary<int, LevelsData.LevelData> _levelsData;

    private const string LevelsPath = "levels";

    public Dictionary<int, LevelsData.LevelData> ReadLevelsData()
    {
        if (_levelsData == null)
        {
            var jsonFile = Resources.Load(LevelsPath, typeof(TextAsset)) as TextAsset;
            if (jsonFile == null)
            {
                throw new ApplicationException("Levels file is not accessible");
            }

            var loadedData = JsonUtility.FromJson<LevelsData>(jsonFile.text);
            _levelsData = loadedData.LevelDataDictionary;
        }

        Debug.Log(_levelsData.Count + " levels have been stored in the dictionary!");

        return _levelsData;
    }

    public void DispatchLoadLevelEvent(int level)
    {
        LoadLevelEventArgs args = new LoadLevelEventArgs();
        args.param0 = _levelsData[level];

        PersistentGameManager.Instance.EventHub.RaiseEvent(args);
    }

}
