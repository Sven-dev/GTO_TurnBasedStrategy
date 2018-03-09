using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpawner : MonoBehaviour {

    public GridTile Tile;
    public int Length;
    public int Width;
    public GridTile[,] TileArray;
    public bool[,] CornerArray;

    // Use this for initialization
    void Start()
    {
        TileArray = new GridTile[Length, Width];
        CornerArray = new bool[Length+1, Width+1];

        for (int x = 0; x < Length; x++)
        {
            for (int y = 0; y < Width; y++)
            {
                TileArray[x, y] = Instantiate(Tile, new Vector3(x*10, 0, y*10), Quaternion.identity);
            }
        }

        for (int x = 0; x < Length + 1; x++)
        {
            for (int y = 0; y < Width + 1; y++)
            {
                CornerArray[x, y] = true;
            }
        }
    }

    public GridTile GetFreeTile()
    {
        List<GridTile> FreeTiles = new List<GridTile>();


        foreach (GridTile t in TileArray)
        {
            if (t.Free)
            {
                FreeTiles.Add(t);
            }
        }

        if (FreeTiles.Count > 0)
        {
            var number = Random.Range(0, FreeTiles.Count);
            return FreeTiles[number];
        }

        print("No free tiles left");
        return null;
    }
}
