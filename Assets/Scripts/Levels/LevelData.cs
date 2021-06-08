/* 
 * Copyright 2021 (C) Hatalom Corporation - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 */

using Assets.Scripts.Levels.Navigation;
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

        public SerializableVector2 characterSpawnPoint;

        public SerializableTileData[] tileData;
        public SerializableTileDataGroup[] tileBlockData;

        private Dictionary<int, SerializableTileData> tilesDict;
        public Dictionary<int, SerializableTileData> SerializedTiles
        {
            /**
             * Optimization considerations:
             * - Getting rid of the tile groups
             */
            get
            {
                if (tilesDict == null)
                {
                    tilesDict = new Dictionary<int, SerializableTileData>();

                    if (tileData != null)
                    {
                        tilesDict = tileData.ToDictionary(tile => ConvertCoordToIndex(tile.rootLocation.x, tile.rootLocation.y, maxX), tile => tile);
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

        public NavigationNode[,] NavigationMatrix
        {
            get;
            private set;
        }

        public void SetupNavigationMatrix(GridLayout gridLayout)
        {
            NavigationMatrix = new NavigationNode[maxX, maxY];

            SerializableTileData tile;
            for (int i = 0; i < maxY; ++i)
            {
                for (int j = 0; j < maxX; ++j)
                {
                    if (tilesDict.TryGetValue(ConvertCoordToIndex(j, i, maxX), out tile))
                    {
                        Vector3 gridCoordinate = gridLayout.CellToWorld(new Vector3Int(j, i, tile.elevation));
                        NavigationMatrix[j, i] = new NavigationNode(gridCoordinate, tile.canTraverse);

                        for (int k = -1; k <= 1; k += 2)
                        {
                            //Add adjacent tiles on the x-axis (no diagonals)
                            var loc = ConvertCoordToIndex(j + k, i, maxX);
                            if (SerializedTiles.ContainsKey(loc))
                            {
                                if (SerializedTiles[loc].canTraverse)
                                {
                                    NavigationMatrix[j, i].AddNeighbor(new Vector2Int(j + k, i));
                                }
                            }

                            //Add adjacent tiles onthe y-axis (no diagonals)
                            loc = ConvertCoordToIndex(j, i + k, maxX);
                            if (SerializedTiles.ContainsKey(loc))
                            {
                                if (SerializedTiles[loc].canTraverse)
                                {
                                    NavigationMatrix[j, i].AddNeighbor(new Vector2Int(j, i + k));
                                }
                            }
                        }
                    }
                }
            }
        }

        public string GetTileFill(int x, int y)
        {
            string fillType = "";

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
            if (levels != null)
            {
                levelDataDictionary = levels.ToDictionary(level => level.number, level => level);
            }

            return levelDataDictionary;
        }
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
        public int elevation;                       //could be serializable vector to represent the direction, such as slopes?
        public string tileType;
        public bool canTraverse;

        /*
        //TODO: Consider moving to a separate class.
        protected List<SerializableTileData> neighbors = new List<SerializableTileData>();
        public List<SerializableTileData> Neighbors 
        { 
            get
            {
                return neighbors;
            }
        }

        public bool TryAddNeighbor(SerializableTileData neighbor)
        {
            bool added = false;
            //TODO: Consider elevation as a criteria for being a neighbor (such as elevation difference being at most a magnitude of 1)
            if(neighbor.canTraverse)
            {
                neighbors.Add(neighbor);
                added = true;               
            }

            return added;
        }
        */
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
        return y * maxX + x;
    }

    public static Vector2Int ConvertIndexToCoord(int index, int maxX)
    {
        int x = index % maxX;
        int y = index / maxX;
        return new Vector2Int(x, y);
    }
}