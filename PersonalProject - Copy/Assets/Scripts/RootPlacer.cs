using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RootPlacer : Placer {

    public Root Prefab;
    public PathFinder PathFinder;

    [HideInInspector]
    public List<Root> Rootlist;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SpawnAround(Tile t, Player p)
    {
        Corner[,] corners = grid.GetCorners(t);

        foreach (Corner c in corners)
        {
            c.AddOwnerShip(p);
        }

        SpawnRoot(corners[0, 0], corners[0, 1]);
        SpawnRoot(corners[0, 1], corners[1, 1]);
        SpawnRoot(corners[1, 1], corners[1, 0]);
        SpawnRoot(corners[1, 0], corners[0, 0]);
    }

    public Root SpawnRoot(Corner tile1, Corner tile2)
    {
        Root r = Instantiate(Prefab, tile1.transform.position, Quaternion.identity, tile1.transform);

        r.SetRotation(tile1.transform.position, tile2.transform.position);
        r.SetPosition(tile1.transform.position, tile2.transform.position);

        return r;
    }

    public void PathFind(Tile end, Player p)
    {
        List<Corner> startCorners = grid.GetBuildingCorners(p, end.GetComponentInChildren<Structure>());
        Corner[,] endCorners = grid.GetCorners(end);

        int smallestDist = int.MaxValue;
        Corner smallestStart = null;
        Corner smallestEnd = null;

        foreach (Corner cs in startCorners)
        {
            foreach(Corner ce in endCorners)
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
            print(smallestStart.Position.X + ", " + smallestStart.Position.Y);
            print(smallestEnd.Position.X + ", " + smallestEnd.Position.Y);
            List<Corner> path = PathFinder.FindPath(smallestStart, smallestEnd, p);
            print(path.Count);
            for (int i = 0; i < path.Count; i++)
            {
                if (i + 1 < path.Count)
                {
                    SpawnRoot(path[i], path[i + 1]);
                }
            }
        }
    }
}
