using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point
{
    public int X;
    public int Y;

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public class Grid : MonoBehaviour
{

    [Space(10)]
    public Tile Tile;
    public Corner Corner;

    [Space(10)]
    public int Length;
    public int Width;

    [Space(10)]
    public Tile[,] TileArray;
    public Corner[,] CornerArray;

    // Use this for initialization
    void Start()
    {
        SpawnGrid();
    }

    //Spawns the field tile and corner prefabs for the length and width given.
    void SpawnGrid()
    {
        GameObject Field = new GameObject("Field");

        // Spawning tiles
        GameObject Tiles = new GameObject("Tiles");
        Tiles.transform.SetParent(Field.transform);
        TileArray = new Tile[Length, Width];
        for (int x = 0; x < Length; x++)
        {
            for (int y = 0; y < Width; y++)
            {
                TileArray[x, y] = Instantiate(Tile, new Vector3(x * 10, 0, y * 10), Quaternion.identity, Tiles.transform);
            }
        }

        //Spawning corners
        GameObject Corners = new GameObject("Corners");
        Corners.transform.SetParent(Field.transform);
        CornerArray = new Corner[Length + 1, Width + 1];
        for (int x = 0; x < Length + 1; x++)
        {
            for (int y = 0; y < Width + 1; y++)
            {
                CornerArray[x, y] = Instantiate(Corner, new Vector3(x * 10 - 5, 0.1f, y * 10 - 5), Quaternion.identity, Corners.transform);
            }
        }
    }

    public Point GetFreeTilePos()
    {
        List<Tile> FreeTiles = new List<Tile>();
        foreach (Tile t in TileArray)
        {
            if (t.OnThisTile == null)
            {
                FreeTiles.Add(t);
            }
        }

        if (FreeTiles.Count > 0)
        {
            var number = Random.Range(0, FreeTiles.Count);

            for (int x = 0; x < Length; x++)
            {
                for (int y = 0; y < Width; y++)
                {
                    if (TileArray[x, y] == FreeTiles[number])
                    {
                        return new Point(x, y);
                    }
                }
            }
        }

        print("No free tiles left!");
        return null;
    }

    public Tile GetFreeTile()
    {
        List<Tile> FreeTiles = new List<Tile>();
        foreach (Tile t in TileArray)
        {
            if (t.OnThisTile == null)
            {
                FreeTiles.Add(t);
            }
        }

        if (FreeTiles.Count > 0)
        {
            var number = Random.Range(0, FreeTiles.Count);

            return FreeTiles[number];
        }

        print("No free tiles left!");
        return null;
    }

    public Point TileToPoint(Tile t)
    {
        for (int x = 0; x < Length; x++)
        {
            for (int y = 0; y < Width; y++)
            {
                if (TileArray[x, y] == t)
                {
                    return new Point(x, y);
                }
            }
        }

        Debug.LogError("Error: Tile does not exist in grid!");
        return null;
    }

    public void AddToGrid(Tile t, Structure s)
    {

    }

    public void AddPlayerTiles(Tile tile, Player player)
    {
        print("Update terrain");
        Point pos = TileToPoint(tile);
        Terrainer structure = (Terrainer)tile.OnThisTile;
        int range = structure.Range;

        /*
        if (range > 0)
        {
            TileArray[pos.X + 1, pos.Y - 1].SetOwner(player);
            TileArray[pos.X + 1, pos.Y].SetOwner(player);
            TileArray[pos.X + 1, pos.Y + 1].SetOwner(player);
            TileArray[pos.X, pos.Y + 1].SetOwner(player);
            TileArray[pos.X - 1, pos.Y + 1].SetOwner(player);
            TileArray[pos.X - 1, pos.Y].SetOwner(player);
            TileArray[pos.X - 1, pos.Y - 1].SetOwner(player);
            TileArray[pos.X, pos.Y - 1].SetOwner(player);
        }

        if (range > 1)
        {

        }
        */

        /* Sets the range in diamond-shape
         * 0  0  0  0  0
         * 0  0  0  0  0
         * 0  0  0  0  0
         * 0  0  0  0  0
         * 0  0  0  0  0
         *
         * Get tiles on same y-axis
         *
         * 0  0  0  0  0
         * 0  0  0  0  0
         * 1  1  1  1  1
         * 0  0  0  0  0
         * 0  0  0  0  0
         * 
         * Get tiles 1 away from y-axis, but also 1 less from each side of x-axis
         * Range = 2
         * X  X  2  X  X
         * X  1  1  1  X
         * 0  0  O  0  0
         * X -1 -1 -1  X
         * X  X -2  X  X
         * 
         * 
         */

        for (int y = -range; y <= range; y++)
        {
            for (int x = range + y; x <= range - y; x++)
            {
                if (x < Length && y < Width)
                {
                    TileArray[pos.X + x, pos.Y + x].SetOwner(player);
                }
            }
        }
    }
}
