using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Grid grid;
    public StructurePlacer StructurePlacer;
    public RootPlacer RootPlacer;
    public Point BaseTile;

    [HideInInspector]
    public Tile SelectedTile;

    public Material Color;

    // Use this for initialization
    void Start ()
    {
        StructurePlacer.grid = this.grid;
        RootPlacer.grid = this.grid;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void BuyThing(int index)
    {
        if (SelectedTile != null)
        {
            StructurePlacer.BuyStructure(SelectedTile, index, this);
            RootPlacer.SpawnAround(grid.TileToPoint(SelectedTile), this);
        }
    }

    public void StartGame()
    {
        Point p = StructurePlacer.SpawnTree(this);
        BaseTile = p;
        RootPlacer.SpawnAround(p, this);
    }

    public void StartTurn()
    {
        gameObject.SetActive(true);
    }

    public void EndTurn()
    {
        gameObject.SetActive(false);
    }
}
