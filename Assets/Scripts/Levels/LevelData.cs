using System;
using System.Collections;
using System.Collections.Generic;
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

        public int rows;
        public int cols;

        public LevelGridLine[] filledLines;
        public BlockingProp[] props;
    }

    [Serializable]
    public class LevelGridLine
    {
        public int row;
        public int startCol;
        public int endCol;
    }

    [Serializable]
    public class SerializableVector2
    {
        public int x;
        public int y;
    }

    public class BlockingProp
    {
        public SerializableVector2 rootLocation;
        public SerializableVector2 sizeVector;
        public string propType;
    }
}