using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point
{
    public int X;
    public int Y;

    /// <summary>
    /// Creates a 2 dimentional coordinate, used because Vector2 is not nullable
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
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

    /// <summary>
    /// Spawns the field tile and corner prefabs for the length and width given.
    /// </summary>
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

    /// <summary>
    /// Adds resources to the grid, based on how large it is.
    /// </summary>
    public void AddResources()
    {
        int amount = Length * Width / 10 / Resources.Count;
        foreach(GameObject g in Resources)
        {
            for (int i = amount; i > 0; i--)
            {
                Tile t = GetFreeTile();
                Instantiate(g, t.transform.position + Vector3.up / 5, Quaternion.identity, t.transform);
            }
        }
    }

    /// <summary>
    /// Returns the 4 corners ajacent to c
    /// </summary>
    /// <param name="c">the corner</param>
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

    /// <summary>
    /// Returns a list of all Tiles with buildings
    /// </summary>
    /// <param name="p">The player the buildings belong to</param>
    /// <param name="excludedStructure">The structure that shouldn't be returned</param>
    public List<Tile> GetBuildings(Player p, Structure excludedStructure)
    {
        List<Tile> StructureList = new List<Tile>();
        foreach (Tile t in TileArray)
        {
            Structure s = t.GetComponentInChildren<Structure>();
            if (s != null && t.Owner == p && s != excludedStructure)
            {
                StructureList.Add(t);
            }
        }

        return StructureList;
    }

    /// <summary>
    /// Returns a list of all corners of tiles with buildings on them
    /// </summary>
    /// <param name="p">The player the building belongs to</param>
    /// <param name="excludedStructure">The structure that shouldn't be returned</param>
    public List<Corner> GetBuildingCorners(Player p, Structure excludedStructure)
    {
        List<Corner> CornerList = new List<Corner>();
        foreach(Tile t in TileArray)
        {
            Structure s = t.GetComponentInChildren<Structure>();
            if (s != null && t.Owner == p && s != excludedStructure)
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
    
    /// <summary>
    /// Returns the 4 corners of a tile
    /// </summary>
    /// <param name="t"></param>
    public Corner[,] GetCorners(Tile t)
    {
        return new Corner[,]
        {
            { CornerArray[t.Position.X, t.Position.Y], CornerArray[t.Position.X + 1, t.Position.Y] },
            { CornerArray[t.Position.X, t.Position.Y +1],CornerArray[t.Position.X + 1, t.Position.Y + 1] }
        };
    }

    /// <summary>
    /// Finds and returns the closest corner to start
    /// </summary>
    /// <param name="start">The starting corner</param>
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

    /// <summary>
    /// Returns the coordinates of random tile with nothing on it
    /// </summary>
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

    /// <summary>
    /// Returns a random tile with nothing on it
    /// </summary>
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

    /// <summary>
    /// Converts a tile into a coordinate. Returns a Point
    /// </summary>
    /// <param name="t">The tile that needs to be converted</param>
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

    /// <summary>
    /// Returns a list of tiles that are in range of the given tile
    /// </summary>
    /// <param name="tile">The given tile</param>
    /// <param name="range">The furthest distance the tile has access to</param>
    public List<Tile> GetRangeTiles(Tile tile, int range)
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
                foreach (Tile t2 in GetTilesAround(t))
                {
                    if (!rangetiles.Contains(t2))
                    {
                        TempList.Add(t2);
                    }
                }
            }

            rangetiles.AddRange(TempList);
        }

        return rangetiles;
    }

    /// <summary>
    /// Returns a list of tiles that are in range of the given tile, but only if the tiles are owned by player
    /// </summary>
    /// <param name="tile">The given tile</param>
    /// <param name="range">The furthest distance the tile has access to</param>
    /// <param name="player">The owner of the Tile</param>
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
                foreach (Tile t2 in GetTilesAround(t))
                {
                    if (t2.Owner == player || t2.Owner == null)
                    {
                        if (!rangetiles.Contains(t2))
                        {
                            TempList.Add(t2);
                        }
                    }
                }
            }

            rangetiles.AddRange(TempList);
        }

        return rangetiles;
    }

    /// <summary>
    /// Returns a list of the tiles ajacent to the given Tile
    /// </summary>
    /// <param name="t">The given Tile</param>
    public List<Tile> GetTilesAround(Tile t)
    {
        List<Tile> tiles = new List<Tile>();
        //Get each tile around the tile.
        if (t.Position.X + 1 < Length)
        {
            tiles.Add(TileArray[t.Position.X + 1, t.Position.Y]);
        }

        if (t.Position.X - 1 >= 0)
        {
            tiles.Add(TileArray[t.Position.X - 1, t.Position.Y]);
        }

        if (t.Position.Y + 1 < Width)
        {
            tiles.Add(TileArray[t.Position.X, t.Position.Y + 1]);
        }

        if (t.Position.Y - 1 >= 0)
        {
            tiles.Add(TileArray[t.Position.X, t.Position.Y - 1]);
        }

        return tiles;
    }

    /// <summary>
    /// Destroys the given structure and its children, and starts _recalculate
    /// </summary>
    /// <param name="s">The parent structure</param>
    public void RecalculateOwnership(Structure s)
    {
        Player owner = s.Owner;
        s.DestroyStructure();
        StartCoroutine(_recalculate(owner));
    }

    /// <summary>
    /// reditributes all tiles to the correct owner
    /// </summary>
    /// <param name="p">The player who gets to go first</param>
    IEnumerator _recalculate(Player p)
    {
        yield return null;

        List<Structure> buildings = GetStructures();
        List<Structure> playerBuildings = new List<Structure>();
        List<Structure> enemyBuildings = new List<Structure>();

        foreach (Structure s in buildings)
        {
            if (s != null)
            {
                if (s.Owner == p)
                {
                    playerBuildings.Add(s);
                }
                else
                {
                    enemyBuildings.Add(s);
                }
            }
        }

        print("player: " + playerBuildings.Count);
        print("enemy: " + enemyBuildings.Count);

        ConvertTiles(playerBuildings);
        ConvertTiles(enemyBuildings);
    }

    /// <summary>
    /// Casts the structure, and calls ConvertTiles
    /// </summary>
    /// <param name="structures">The structures that need to be cast</param>
    void ConvertTiles(List<Structure> structures)
    {
        foreach (Structure s in structures)
        {
            if (s != null)
            {
                if (s is Terrainer)
                {
                    Terrainer st = (Terrainer)s;
                    st.ConvertTiles();
                }
                else if (s is BaseTree)
                {
                    BaseTree sb = (BaseTree)s;
                    sb.ConvertTiles();
                }
            }
        }
    }

    /// <summary>
    /// Returns a list of all structures on the map
    /// </summary>
    public List<Structure> GetStructures()
    {
        List<Structure> Structures = new List<Structure>();
        foreach (Tile t in TileArray)
        {
            Structure s = t.GetComponentInChildren<Structure>();
            if (s != null)
            {
                Structures.Add(s);
            }
        }

        return Structures;
    }
}