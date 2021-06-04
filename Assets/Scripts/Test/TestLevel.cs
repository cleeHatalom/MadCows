using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TestLevel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PersistentGameManager.Instance.EventHub.AddListener<int, int, int>("LoadLevel", SetupTiles);
    }

    private void SetupTiles(int FieldTotalTiles, int first, int last)
    {
        Tilemap baseLevel = GetComponentsInChildren<Tilemap>()[0];
        var localTilesPositions = new List<Vector3Int>(FieldTotalTiles);
        foreach (var pos in baseLevel.cellBounds.allPositionsWithin)
        {
            Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
            localTilesPositions.Add(localPlace);
        }

        var pathHorizontalTile = TilesResourcesLoader.GetPathHorizontalTile();
        foreach (var localPosition in localTilesPositions.GetRange(first, Math.Abs(first - last)))
        {
            baseLevel.SetTile(localPosition, pathHorizontalTile);
        }

        var startStopTile = TilesResourcesLoader.GetStartStopTile();
        baseLevel.SetTile(localTilesPositions[first], startStopTile);
        baseLevel.SetTile(localTilesPositions[last], startStopTile);
    }
}
