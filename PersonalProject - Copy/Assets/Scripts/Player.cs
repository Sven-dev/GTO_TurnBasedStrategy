using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Grid grid;
    public UnitFactory BaseTree;
    public List<UnitFactory> structures;
    public RootPlacer RootPlacer;
    public Point BaseTile;

    [HideInInspector]
    public Tile SelectedTile;

    public Color PlayerColor;

    // Use this for initialization
    void Start ()
    {
        RootPlacer.grid = this.grid;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void PlaceStartingTree()
    {
        if (SelectedTile != null)
        {
            Structure s = BaseTree.Buy();
            Place(s, SelectedTile);

            RootPlacer.SpawnAround(SelectedTile, this);
            DeselectTile();
        }
    }

    public void GetThing(int index)
    {
        if (SelectedTile != null)
        {
            Structure s = structures[index].Buy();
            Place(s, SelectedTile);

            RootPlacer.SpawnAround(SelectedTile, this);
            DeselectTile();
        }
    }

    //Instantiates a structure on a tile
    public void Place(Structure s, Tile t)
    {
        //[BUG] Doesn't spawn the structure on the tile
        Instantiate(s, Vector3.zero, Quaternion.identity, t.transform);
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
