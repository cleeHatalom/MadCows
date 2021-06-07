/* 
 * Copyright 2021 (C) Hatalom Corporation - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 */

using EventDefinitions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelBehaviour : SubscriberMonoBehaviour
{
    private Dictionary<string, IEnumerator> navPathProcesses = new Dictionary<string, IEnumerator>();

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
            Debug.Log("Number of Serialized Tiles read: " + levelData.SerializedTiles.Count);
        }

        for (int h = 0; h < levelData.maxY; h++) 
        {
            for (int w = 0; w < levelData.maxX; w++)
            {
                string tileToSet = levelData.GetTileFill(w, h);
                if (tileToSet != "")
                {
                    baseTilemap.SetTile(currentCellPosition, TilesResourcesLoader.GetTileByName(tileToSet));
                }
                else
                {
                    baseTilemap.SetTile(currentCellPosition, TilesResourcesLoader.GetBaseRuleTile());
                }
                currentCellPosition.x = (int)Math.Ceiling(cellSize.x + currentCellPosition.x);
            }

            currentCellPosition.y = (int)Math.Ceiling(cellSize.y + currentCellPosition.y);
            currentCellPosition.x = origin.x;
        }

        baseTilemap.CompressBounds();

    }

    public void GeneratePath(Vector2 newPosition)
    {
        //Debug.Log("New Position: " + newPosition);
        gameObject.transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);


        MapTargetSetEventArgs args = new MapTargetSetEventArgs();
        args.param0 = newPosition;

        PersistentGameManager.Instance.EventHub.RaiseEvent(args);
    }
    public void SelectCharacterDestination(Vector2 newPosition)
    {
        //Debug.Log("New Position: " + newPosition);
        gameObject.transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);


        MapTargetSetEventArgs args = new MapTargetSetEventArgs();
        args.param0 = newPosition;

        PersistentGameManager.Instance.EventHub.RaiseEvent(args);
    }

    private void PollSelectedCharacterPosition(string characterName)
    {
        if(characterName != "")
        {

        }
    }

    private IEnumerator CreateNavPath(string characterName, Vector2Int start, Vector2Int end)
    {


        yield return null;
    }

    public override void Register()
    {
        PersistentGameManager.Instance.EventHub.AddListener<LoadLevelEvent, LevelsData.LevelData>(SetupTiles);
        PersistentGameManager.Instance.EventHub.AddListener<OnScreenClickedEvent, Vector2>(SelectCharacterDestination);
        PersistentGameManager.Instance.EventHub.AddListener<OnCharacterSelectedEvent, string>(PollSelectedCharacterPosition);

    }

    public override void Unregister()
    {
        PersistentGameManager.Instance.EventHub.RemoveListener<LoadLevelEvent, LevelsData.LevelData>(SetupTiles);
        PersistentGameManager.Instance.EventHub.RemoveListener<OnScreenClickedEvent, Vector2>(SelectCharacterDestination);
        PersistentGameManager.Instance.EventHub.RemoveListener<OnCharacterSelectedEvent, string>(PollSelectedCharacterPosition);
    }
}
