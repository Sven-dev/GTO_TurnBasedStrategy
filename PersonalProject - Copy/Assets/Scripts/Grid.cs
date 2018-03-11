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

public class Grid : MonoBehaviour {

    public List<Player> PlayerList;

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
        SpawnCorners();
        SpawnStartingStructures();
    }

    void SpawnGrid()
    {
        TileArray = new Tile[Length, Width];
        for (int x = 0; x < Length; x++)
        {
            for (int y = 0; y < Width; y++)
            {
                TileArray[x, y] = Instantiate(Tile, new Vector3(x * 10, 0, y * 10), Quaternion.identity);
            }
        }
    }

    void SpawnCorners()
    {
        CornerArray = new Corner[Length + 1, Width + 1];
        for (int x = 0; x < Length + 1; x++)
        {
            for (int y = 0; y < Width + 1; y++)
            {
                CornerArray[x, y] = Instantiate(Corner, new Vector3(x * 10 - 5, 0.1f, y * 10 - 5), Quaternion.identity);
            }
        }
    }

    void SpawnStartingStructures()
    {
        foreach (Player p in PlayerList)
        {
            p.StartGame();
        }
    }

    public void StartCornerOwnership(Player p, Tile t)
    {
        for (int x = 0; x < Length + 1; x++)
        {
            for (int y = 0; y < Width + 1; y++)
            {
                if (TileArray[x, y] == t)
                {

                }
            }
        }
    }

    void UpdateCornerOwnership(Player p, Tile t)
    {
        for (int x = 0; x < Length + 1; x++)
        {
            for (int y = 0; y < Width + 1; y++)
            {
                if (TileArray[x, y] == t)
                {

                }
            }
        }
    }

    public Point GetFreeTile()
    {
        List<Tile> FreeTiles = new List<Tile>();
        foreach (Tile t in TileArray)
        {
            if (t.Free)
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
}
