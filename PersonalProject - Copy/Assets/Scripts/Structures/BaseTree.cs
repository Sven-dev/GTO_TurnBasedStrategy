using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTree : Structure
{
    public Grid Grid;
    public int Range;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CollectResources()
    {
        foreach (Resource r in GetComponentsInChildren<Resource>())
        {
            //Add resource to player the collector belong to
        }
    }

    public void ConvertTiles()
    {
        List<Tile> tiles = Grid.GetRangeTiles(transform.parent.transform.GetComponent<Tile>(), Range, Owner);
        foreach (Tile t in tiles)
        {
            t.SetOwner(Owner);
        }
    }

    public override void StartUp(Player p, Grid g)
    {
        Owner = p;
        this.Grid = g;

        ConvertTiles();
        Owner.OnTurnChange += CollectResources;
    }
}
