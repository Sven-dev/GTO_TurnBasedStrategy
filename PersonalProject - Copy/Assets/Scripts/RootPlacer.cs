using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RootPlacer : MonoBehaviour
{

    public Root Prefab;
    public PathFinder PathFinder;
    public Grid Grid;

    [HideInInspector]
    public List<Root> Rootlist;

    /// <summary>
    /// Spawns and returns 4 roots around a tile
    /// </summary>
    /// <param name="t">The Tile that needs to be spawned around</param>
    /// <param name="p">The Player that owns the Tile</param>
    public List<Root> SpawnAround(Tile t, Player p)
    {
        Corner[,] corners = Grid.GetCorners(t);

        foreach (Corner c in corners)
        {
            c.AddOwnerShip(p);
        }

        List<Root> roots = new List<Root>();

        roots.Add(SpawnRoot(corners[0, 0], corners[0, 1]));
        roots.Add(SpawnRoot(corners[0, 1], corners[1, 1]));
        roots.Add(SpawnRoot(corners[1, 1], corners[1, 0]));
        roots.Add(SpawnRoot(corners[1, 0], corners[0, 0]));

        return roots;
    }

    /// <summary>
    /// Spawns a Root between 2 tiles
    /// </summary>
    /// <param name="tile1"></param>
    /// <param name="tile2"></param>
    /// <returns></returns>
    public Root SpawnRoot(Corner tile1, Corner tile2)
    {
        Root r = Instantiate(Prefab, tile1.transform.position, Quaternion.identity, tile1.transform);

        r.SetRotation(tile1.Position, tile2.Position);
        r.SetPosition(tile1.transform.position, tile2.transform.position);
        return r;
    }

    /// <summary>
    /// Pathfinds the closest tile that is owned by p
    /// </summary>
    /// <param name="end">The end Tile</param>
    /// <param name="p">The player the Tile belongs to</param>
    /// <returns></returns>
    public Tile GetClosestTile(Tile end, Player p)
    {
        List<Tile> buildings = Grid.GetBuildings(p, end.GetComponentInChildren<Structure>());
        int smallestDist = int.MaxValue;
        Tile closest = null;

        foreach (Tile t in buildings)
        {
            int i = PathFinder.GuessDistance(t, end);
            if (i < smallestDist)
            {
                smallestDist = i;
                closest = t;
            }
        }

        return closest;
    }

    /// <summary>
    /// Gets the closest path between 2 tiles, that is owned by p
    /// </summary>
    /// <param name="start">The start Tile</param>
    /// <param name="end">The end Tile</param>
    /// <param name="p">The player the path needs to be owned by</param>
    /// <returns></returns>
    public List<Root> GetClosestPath(Tile start, Tile end, Player p)
    {
        Corner[,] startCorners = Grid.GetCorners(start);
        Corner[,] endCorners = Grid.GetCorners(end);

        int smallestDist = int.MaxValue;
        Corner smallestStart = null;
        Corner smallestEnd = null;

        foreach (Corner cs in startCorners)
        {
            foreach (Corner ce in endCorners)
            {
                int i = PathFinder.GuessDistance(cs, ce);
                if (i < smallestDist)
                {
                    smallestDist = i;
                    smallestStart = cs;
                    smallestEnd = ce;
                }
            }
        }

        if (smallestStart != null && smallestEnd != null)
        {
            List<Corner> path = PathFinder.FindPath(smallestStart, smallestEnd, p);
            List<Root> roots = new List<Root>();

            for (int i = 0; i < path.Count; i++)
            {
                if (i + 1 < path.Count)
                {
                    roots.Add(SpawnRoot(path[i], path[i + 1]));
                }
            }

            return roots;
        }

        return null;
    }
}