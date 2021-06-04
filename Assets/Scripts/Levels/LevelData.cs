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

        public LevelGridLine[] filledLines;
        public SpecialTile[] specialTiles;

        public bool isInitialized = false;

        private Dictionary<int, HashSet<int>> filledGridLineDict;
        public Dictionary<int, HashSet<int>> FilledGridLines
        {
            get
            {
                if (filledGridLineDict == null)
                {
                    filledGridLineDict = new Dictionary<int, HashSet<int>>();

                    if (filledLines != null)
                    {
                        filledGridLineDict = filledLines.ToDictionary(line => line.row, line => new HashSet<int>(line.blankCol));
                    }
                }
                return filledGridLineDict;
            }
        }

        private Dictionary<int, string> specialTilesDict;
        public Dictionary<int, string> SpecialTiles
        {
            get
            {
                if (specialTilesDict == null)
                {
                    specialTilesDict = new Dictionary<int, string>();

                    if (specialTiles != null)
                    {
                        foreach (SpecialTile tile in specialTiles)
                        {
                            for (int i = 0; i < tile.sizeVector.y; ++i)
                            {
                                for (int j = 0; j < tile.sizeVector.x; ++j)
                                {
                                    specialTilesDict.Add(ConvertCoordToIndex(tile.rootLocation.x + j, tile.rootLocation.y + i, maxX), tile.tileType);
                                }
                            }
                        }
                    }
                }
                return specialTilesDict;
            }
        }

        public bool CheckIsFilledIn(int line, int column)
        {
            bool result = true;

            if (FilledGridLines.ContainsKey(line))  //if no filled lines are specified, short circuit to the default result.
            {
                result = FilledGridLines[line].Contains(column);
            }

            return result;
        }

        public bool IsSpecialTile(int x, int y)
        {
            return SpecialTiles.ContainsKey(ConvertCoordToIndex(x, y, maxX));
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
        public int[] blankCol;
    }

    [Serializable]
    public class SerializableVector2
    {
        public int x;
        public int y;
    }

    [Serializable]
    public class SpecialTile
    {
        public SerializableVector2 rootLocation;
        public SerializableVector2 sizeVector;
        public string tileType;
    }

    public static int ConvertCoordToIndex(int x, int y, int maxX)
    {
        return y*maxX + x;
    }
}