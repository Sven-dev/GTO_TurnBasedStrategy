using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RootPlacer : Placer {

    public Root Prefab;

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
        Corner[,] corners = new Corner[,]
        {
            { grid.CornerArray[t.Position.X, t.Position.Y], grid.CornerArray[t.Position.X + 1, t.Position.Y] },
            { grid.CornerArray[t.Position.X, t.Position.Y +1], grid.CornerArray[t.Position.X + 1, t.Position.Y + 1] }
        };

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
        Root r = Instantiate(Prefab, tile1.transform.position, Quaternion.identity);

        r.SetRotation(tile1.transform.position, tile2.transform.position);
        r.SetPosition(tile1.transform.position, tile2.transform.position);

        return r;
    }
}
