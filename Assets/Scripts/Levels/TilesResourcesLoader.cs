using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class TilesResourcesLoader
{
    private const string PathHorizontal = "Sprites/UI/Track_02";
    private const string StartStop = "Sprites/UI/SeigeZone";
    private const string BaseGrass = "Sprites/Tilemaps/Flat/grass-flat-01";
    public static Tile GetPathHorizontalTile()
    {
        return GetTileByName(PathHorizontal);
    }
    public static Tile GetStartStopTile()
    {
        return GetTileByName(StartStop);
    }
    private static Tile GetTileByName(string name)
    {
        return (Tile)Resources.Load(name, typeof(Tile));
    }

    public static TileBase GetBaseTile()
    {
        return GetTileByName(BaseGrass);
    }
}
