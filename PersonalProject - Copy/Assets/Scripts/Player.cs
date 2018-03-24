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

    public delegate void NewTurn();
    public event NewTurn OnTurnChange;


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
        if (SelectedTile != null && SelectedTile.transform.childCount == 0 && SelectedTile.Owner == this)
        {
            Structure s = structures[index].Buy();
            Place(s, SelectedTile);

            RootPlacer.SpawnAround(SelectedTile, this);
            DeselectTile();
        }
    }

    public void GetCollector()
    {
        if (SelectedTile != null && SelectedTile.Owner == this)
        {
            ResourceSpot spot = SelectedTile.GetComponentInChildren<ResourceSpot>();
            if (spot != null)
            {
                Structure s = null;
                switch (spot.Type)
                {
                    case ResourceType.Water:
                        s = structures[1].Buy();
                        break;
                    case ResourceType.co2:
                        s = structures[2].Buy();
                        break;
                    case ResourceType.Solar:
                        s = structures[3].Buy();
                        break;
                }

                if (s != null)
                {
                    Place(s, SelectedTile);
                    Collector c = s as Collector;
                    c.ConnectToSpot(spot);
                }
            }
        }
    }

    //Instantiates a structure on a tile
    public void Place(Structure s, Tile t)
    {
        s = Instantiate(s, t.transform.position, Quaternion.identity, t.transform);
        s.StartUp(this, grid);
    }

    public void SelectTile(Tile t)
    {
        SelectedTile = t;
        t.Select();
    }

    public void DeselectTile()
    {
        SelectedTile.Deselect();
    }

    public void StartTurn()
    {
        if (OnTurnChange != null)
        {
            OnTurnChange();
        }

        gameObject.SetActive(true);
    }

    public void EndTurn()
    {
        //SelectedTile.GetComponent<Renderer>().material.color = SelectedTile.Default.color;
        SelectedTile = null;
        gameObject.SetActive(false);
    }
}
