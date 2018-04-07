using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Grid grid;
    public UnitFactory BaseTree;
    public List<UnitFactory> structures;

    [Space]
    public RootPlacer RootPlacer;
    public Tile BaseTile;

    public Tile SelectedTile;

    public Color PlayerColor;
    public List<Resource> Resources;

    [Space]
    public SliderContoller SliderContoller;
    public VictoryController VictoryController;

    public delegate void NewTurn();
    public event NewTurn OnTurnChange;


    // Use this for initialization
    void Start ()
    {
        RootPlacer.grid = this.grid;
	}

    public void PlaceStartingTree()
    {
        if (SelectedTile != null)
        {
            Structure s = structures[0].Buy();
            BaseTree b = Place(s, SelectedTile) as BaseTree;

            BaseTile = SelectedTile;
            b.GrowCost.resource = Resources[2];
        }
    }

    public void GetThing(int index)
    {
        if (SelectedTile != null && SelectedTile.GetComponentInChildren<Structure>() == null && SelectedTile.Owner == this)
        {
            Structure s = structures[index].Buy();
            if (s != null)
            {
                s = Place(s, SelectedTile);

                Tile start = RootPlacer.GetClosestTile(SelectedTile, this);
                Structure Parent = start.GetComponentInChildren<Structure>();
                Parent.Children.Add(s);

                s.Roots.AddRange(RootPlacer.GetClosestPath(start, SelectedTile, this));
            }
        }
    }

    public void GetCollector()
    {
        if (SelectedTile != null && SelectedTile.Owner == this)
        {
            ResourceSpot spot = SelectedTile.GetComponentInChildren<ResourceSpot>();
            Structure building = SelectedTile.GetComponentInChildren<Structure>();
            if (spot != null && building == null)
            {
                switch (spot.Type)
                {
                    case ResourceType.Water:
                        GetThing(2);
                        break;
                    case ResourceType.co2:
                        GetThing(3);
                        break;
                    case ResourceType.Solar:
                        GetThing(4);
                        break;
                }
            }
        }
    }

    public Resource[] GetResources()
    {
        return transform.GetChild(1).GetComponentsInChildren<Resource>();
    }

    //Instantiates a structure on a tile
    public Structure Place(Structure s, Tile t)
    {
        s = Instantiate(s, t.transform.position + new Vector3(0, -5, 0), Quaternion.identity, t.transform);
        s.StartUp(this, grid);
        s.Roots = RootPlacer.SpawnAround(SelectedTile, this);
        s.Place();
        return s;
    }

    public void SelectTile(Tile t)
    {
        SelectedTile = t;
        t.Select();
    }

    public void DeselectTile()
    {
        SelectedTile.Deselect();
        SelectedTile = null;
    }

    public void StartTurn()
    {
        transform.GetChild(2).gameObject.SetActive(true);
        if (OnTurnChange != null)
        {
            OnTurnChange();
        }
    }

    public void EndTurn()
    {
        if (SelectedTile != null)
        {
            SelectedTile.Deselect();
        }

        SelectedTile = null;
        transform.GetChild(2).gameObject.SetActive(false);
    }

    public void GrowTree()
    {
        if (BaseTile == null)
        {
            PlaceStartingTree();
        }

        BaseTree b = BaseTile.GetStructure() as BaseTree;
        if (b != null)
        {
            b.Grow();
            SliderContoller.UpdateSlider(b.Health);
            VictoryController.CheckVictory(b, this);
        }
    }
}