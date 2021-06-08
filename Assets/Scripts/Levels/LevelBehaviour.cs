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

[DisallowMultipleComponent]
public class LevelBehaviour : SubscriberMonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="levelData"></param>
    private void LaunchSetupTiles(LevelsData.LevelData levelData)
    {
        StopAllCoroutines();
        StartCoroutine(SetupTiles(levelData));        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="levelData"></param>
    /// <returns></returns>
    private IEnumerator SetupTiles(LevelsData.LevelData levelData)
    {
        Tilemap baseTilemap = GetComponentsInChildren<Tilemap>()[0];

        baseTilemap.ClearAllTiles();
        var origin = baseTilemap.origin - new Vector3Int(levelData.maxX / 2, levelData.maxY / 2, 0);
        var cellSize = baseTilemap.cellSize;
        var currentCellPosition = origin;

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

        levelData.SetupNavigationMatrix(GetComponent<Grid>());

        Debug.Log("Nav Matrix populate: " + levelData.NavigationMatrix.Length);

        SpawnCharacterEventArgs args = new SpawnCharacterEventArgs();
        args.param0 = GetComponent<GridLayout>().CellToWorld(new Vector3Int((int)(levelData.characterSpawnPoint.x - levelData.maxX /2), (int)(levelData.characterSpawnPoint.y - levelData.maxY / 2), 0));

        PersistentGameManager.Instance.EventHub.RaiseEvent(args);

        yield return null;
    }


    /// <summary>
    /// 
    /// </summary>
    public override void Register()
    {
        PersistentGameManager.Instance.EventHub.AddListener<LoadLevelEvent, LevelsData.LevelData>(LaunchSetupTiles);

    }
    /// <summary>
    /// 
    /// </summary>
    public override void Unregister()
    {
        PersistentGameManager.Instance.EventHub.RemoveListener<LoadLevelEvent, LevelsData.LevelData>(LaunchSetupTiles);
    }
}
