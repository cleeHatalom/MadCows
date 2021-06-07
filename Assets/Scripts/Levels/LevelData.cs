/* 
 * Copyright 2021 (C) Hatalom Corporation - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class LevelsData
{
    public LevelData[] levels;

    [Serializable]
    public class LevelData
    {
        public int number;
        public int[] path;

        public int maxX;
        public int maxY;

        public SerializableTileData[] tileData;
        public SerializableTileDataGroup[] tileBlockData;

        private Dictionary<int, SerializableTileData> tilesDict;
        public Dictionary<int, SerializableTileData> SerializedTiles
        {
            /**
             * Optimization considerations:
             * - 
             */
            get
            {
                if (tilesDict == null)
                {
                    tilesDict = new Dictionary<int, SerializableTileData>();

                    if(tileData != null)
                    {
                        tilesDict = tileData.ToDictionary(tile => ConvertCoordToIndex(tile.rootLocation.x, tile.rootLocation.y, maxX), tile=>tile);
                    }
                    #region Temporary Deserialization for Tile Data Groups (See <summary> above class for more)
                    if (tileBlockData != null)
                    {
                        foreach (SerializableTileDataGroup tileBlock in tileBlockData)
                        {
                            for (int i = 0; i < tileBlock.sizeVector.y; ++i)
                            {
                                for (int j = 0; j < tileBlock.sizeVector.x; ++j)
                                {
                                    int index = ConvertCoordToIndex(tileBlock.rootLocation.x + j, tileBlock.rootLocation.y + i, maxX);
                                    if (!tilesDict.ContainsKey(index))
                                    {
                                        tilesDict.Add(index, tileBlock);
                                    }
                                    else //TODO: Determine What can overwrite what
                                    {
                                        tilesDict[index] = tileBlock;
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                }
                return tilesDict;
            }
        }

        public string GetTileFill(int x, int y)
        {
            string fillType ="";

            SerializableTileData tile;
            if (SerializedTiles.TryGetValue(ConvertCoordToIndex(x, y, maxX), out tile))
            {
                fillType = tile.tileType;
            }

            return fillType;
        }
    }

    private Dictionary<int, LevelData> levelDataDictionary = new Dictionary<int, LevelData>();
    public Dictionary<int, LevelData> LevelDataDictionary
    {
        get
        {
            if(levels!=null)
            {
                levelDataDictionary = levels.ToDictionary(level => level.number, level => level);
            }

            return levelDataDictionary;
        }
    }

    [Serializable]
    public class LevelGridLine
    {
        public int row;
        public int[] blankCol; //Elevation will 
    }

    [Serializable]
    public class SerializableVector2
    {
        public int x;
        public int y;
    }

    [Serializable]
    public class SerializableTileData
    {
        public SerializableVector2 rootLocation;
        public int elevation;                       //may consider this 
        public string tileType;
        public bool canTraverse;
    }

    /// <summary>
    /// This is a testing class for JSON serialization. Will be removed eventually once
    /// a usable Stage Creator Tool is created.
    /// </summary>
    [Serializable]
    public class SerializableTileDataGroup : SerializableTileData
    {
        public SerializableVector2 sizeVector;
    }

    public static int ConvertCoordToIndex(int x, int y, int maxX)
    {
        return y*maxX + x;
    }
}