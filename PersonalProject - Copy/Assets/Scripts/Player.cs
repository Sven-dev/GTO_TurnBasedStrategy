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

    public Color PlayerColor;

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
            DeselectTile();
        }
    }

    public void StartGame()
    {
        Point p = StructurePlacer.SpawnTree(this);
        BaseTile = p;
        RootPlacer.SpawnAround(p, this);
    }

    public void SelectTile(Tile t)
    {
        SelectedTile = t;
        t.GetComponent<Renderer>().material.color = Color.white;
    }

    public void DeselectTile()
    {
        SelectedTile.GetComponent<Renderer>().material.color = PlayerColor;
    }

    public void StartTurn()
    {
        gameObject.SetActive(true);
    }

    public void EndTurn()
    {
        //SelectedTile.GetComponent<Renderer>().material.color = SelectedTile.Default.color;
        SelectedTile = null;
        gameObject.SetActive(false);
    }
}
