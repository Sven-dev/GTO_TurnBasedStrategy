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
    public List<GameObject> Resources;

    [Space(10)]
    public int Length; //X
    public int Width; //Y

    [Space(10)]
    public Tile[,] TileArray;
    public Corner[,] CornerArray;
    public PathFinder PathFinder;

    // Use this for initialization
    void Start()
    {
        SpawnGrid();
        AddResources();
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
                TileArray[x, y].Position = new Point(x, y);
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
                CornerArray[x, y].Position = new Point(x, y);
            }
        }
    }

    //Adds resources to the grid, based on how large it is.
    public void AddResources()
    {
        int amount = Length * Width / 10 / Resources.Count;
        foreach(GameObject g in Resources)
        {
            for (int i = amount; i > 0; i--)
            {
                Tile t = GetFreeTile();
                Instantiate(g, t.transform.position + Vector3.up, Quaternion.identity, t.transform);
            }
        }
    }

    public List<Corner> GetCornerNeighbours(Corner c)
    {
        List<Corner> neighbours = new List<Corner>();
        if (c.Position.X +1< Length +1)
        {
            neighbours.Add(CornerArray[c.Position.X + 1, c.Position.Y]);
        }

        if (c.Position.X - 1 >= 0)
        {
            neighbours.Add(CornerArray[c.Position.X - 1, c.Position.Y]);
        }

        if (c.Position.Y + 1 < Width + 1)
        {
            neighbours.Add(CornerArray[c.Position.X, c.Position.Y + 1]);
        }

        if (c.Position.Y - 1 >= 0)
        {
            neighbours.Add(CornerArray[c.Position.X, c.Position.Y - 1]);
        }

        return neighbours;
    }

    
    public List<Corner> GetBuildingCorners(Player p, Structure excludedStructure)
    {
        List<Corner> CornerList = new List<Corner>();
        foreach(Tile t in TileArray)
        {
            Structure s = t.GetComponentInChildren<Structure>();
            if (t.Owner == p && s != null && s != excludedStructure)
            {
                Corner[,] temp = GetCorners(t);
                foreach (Corner c in temp)
                {
                    CornerList.Add(c);
                }
            }
        }

        return CornerList;
    }
    


    public Corner[,] GetCorners(Tile t)
    {
        return new Corner[,]
        {
            { CornerArray[t.Position.X, t.Position.Y], CornerArray[t.Position.X + 1, t.Position.Y] },
            { CornerArray[t.Position.X, t.Position.Y +1],CornerArray[t.Position.X + 1, t.Position.Y + 1] }
        };
    }

    public Corner GetClosestBuilding(Corner start)
    {
        Corner closestCorner = null;
        int closestDist = int.MaxValue;
        foreach (Corner c in CornerArray)
        {
            Root r = c.GetComponentInChildren<Root>();
            if (r != null)
            {
                int dist = PathFinder.GuessDistance(start, c);
                if (dist < closestDist)
                {
                    closestDist = dist;
                    closestCorner = c;
                }
            }
        }

        return closestCorner;
    }

    public Point GetFreeTilePos()
    {
        List<Tile> FreeTiles = new List<Tile>();
        foreach (Tile t in TileArray)
        {
            if (t.transform.childCount == 0)
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
            if (t.transform.childCount == 0 && t.Owner == null)
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

    public List<Tile> GetRangeTiles(Tile tile, int range, Player player)
    {
        List<Tile> rangetiles = new List<Tile>();
        rangetiles.Add(TileArray[tile.Position.X, tile.Position.Y]);

        //counts down range
        for (int i = range; i > 0; i--)
        {
            List<Tile> TempList = new List<Tile>();
            foreach (Tile t in rangetiles)
            {
                //Get each tile around the tile, if tile is not in list already, add it.
                if (t.Position.X + 1 < Length)
                {
                    if (!rangetiles.Contains(TileArray[t.Position.X + 1, t.Position.Y]))
                    {
                        TempList.Add(TileArray[t.Position.X + 1, t.Position.Y]);
                    }
                }

                if (t.Position.X - 1 >= 0)
                {
                    if (!rangetiles.Contains(TileArray[t.Position.X - 1, t.Position.Y]))
                    {
                        TempList.Add(TileArray[t.Position.X - 1, t.Position.Y]);
                    }
                }

                if (t.Position.Y + 1 < Width)
                {
                    if (!rangetiles.Contains(TileArray[t.Position.X, t.Position.Y + 1]))
                    {
                        TempList.Add(TileArray[t.Position.X, t.Position.Y + 1]);
                    }
                }

                if (t.Position.Y - 1 >= 0)
                {
                    if (!rangetiles.Contains(TileArray[t.Position.X, t.Position.Y - 1]))
                    {
                        TempList.Add(TileArray[t.Position.X, t.Position.Y - 1]);
                    }
                }
            }

            rangetiles.AddRange(TempList);          
        }

        return rangetiles;
    }
}
