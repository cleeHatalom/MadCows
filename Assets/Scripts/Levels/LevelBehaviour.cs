using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PersistentGameManager.Instance.EventHub.AddListener<LevelsData.LevelData> ("LoadLevel", SetupTiles);
    }

    private void SetupTiles(LevelsData.LevelData levelData)
    {
        int FieldTotalTiles = levelData.maxX * levelData.maxY;
        Tilemap baseTilemap = GetComponentsInChildren<Tilemap>()[0];

        baseTilemap.ClearAllTiles();
        var origin = baseTilemap.origin - new Vector3Int(levelData.maxX/2, levelData.maxY / 2, 0);
        var cellSize = baseTilemap.cellSize;
        var currentCellPosition = origin;

        //foreach(string s in levelData.SpecialTiles.Values)
        {
            //Debug.Log(s);
        }

        for (int h = 0; h < levelData.maxY; h++) 
        {
            for (int w = 0; w < levelData.maxX; w++)
            {
                if (levelData.IsSpecialTile(w, h))
                {
                    Debug.Log("Special Tile Detected at (" + w + ", " + h + ")");
                }
                else
                {
                    baseTilemap.SetTile(currentCellPosition, TilesResourcesLoader.GetBaseTile());
                }
                currentCellPosition.x = (int)Math.Ceiling(cellSize.x + currentCellPosition.x);
            }

            currentCellPosition.y = (int)Math.Ceiling(cellSize.y + currentCellPosition.y);
            currentCellPosition.x = origin.x;
        }

        baseTilemap.CompressBounds();
        /*
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
        */
    }
}
