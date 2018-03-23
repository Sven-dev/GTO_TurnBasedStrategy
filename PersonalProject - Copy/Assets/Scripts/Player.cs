﻿using System.Collections;
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
        s = Instantiate(s, t.transform.position, Quaternion.identity, t.transform);
        s.SetVariables(this, grid);
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
        gameObject.SetActive(true);
    }

    public void EndTurn()
    {
        //SelectedTile.GetComponent<Renderer>().material.color = SelectedTile.Default.color;
        SelectedTile = null;
        gameObject.SetActive(false);
    }
}
