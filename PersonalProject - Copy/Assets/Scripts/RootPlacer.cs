using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RootPlacer : Placer {

    public Root Prefab;
    public List<Root> Rootlist;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Spawns the 4 roots around start-tile
    public void SpawnStartRoots(Point co, Player p)
    {
        Corner[,] corners = new Corner[,]
        {
            { grid.CornerArray[co.X, co.Y], grid.CornerArray[co.X + 1, co.Y] },
            { grid.CornerArray[co.X, co.Y +1], grid.CornerArray[co.X + 1, co.Y + 1] }
        };

        foreach (Corner c in corners)
        {
            c.AddOwnerShip(p);
        }

        SpawnRoot(corners[0,0], corners[0, 1]);
        SpawnRoot(corners[0, 1], corners[1, 1]);
        SpawnRoot(corners[1, 1], corners[1, 0]);
        SpawnRoot(corners[1, 0], corners[0, 0]);
    }


    public Root SpawnRoot(Corner tile1, Corner tile2)
    {
        Root r = Instantiate(Prefab, tile1.transform.position, Quaternion.identity);

        r.SetRotation(tile1.transform.position, tile2.transform.position);
        r.SetPosition(tile1.transform.position, tile2.transform.position);

        return r;
    }
}
