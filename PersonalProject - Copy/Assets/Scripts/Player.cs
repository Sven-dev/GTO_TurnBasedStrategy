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

    [HideInInspector]
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
            BaseTile = SelectedTile;

            BaseTree b = s as BaseTree;
            b.GrowCost.resource = Resources[2];

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

            RootPlacer.PathFind(SelectedTile, this);
            RootPlacer.SpawnAround(SelectedTile, this);
            DeselectTile();
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
                Structure s = null;
                switch (spot.Type)
                {
                    case ResourceType.Water:
                        s = structures[2].Buy();
                        break;
                    case ResourceType.co2:
                        s = structures[3].Buy();
                        break;
                    case ResourceType.Solar:
                        s = structures[4].Buy();
                        break;
                }

                if (s != null)
                {
                    Place(s, SelectedTile);
                }
            }
        }
    }

    public Resource[] GetResources()
    {
        return transform.GetChild(1).GetComponentsInChildren<Resource>();
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
        if (SelectedTile != null)
        {
            SelectedTile.Deselect();
        }

        SelectedTile = null;
        gameObject.SetActive(false);
    }

    public void GrowTree()
    {
        BaseTree b = BaseTile.GetStructure() as BaseTree;
        b.Grow();
        SliderContoller.UpdateSlider(b.Growthcurrent);
        VictoryController.CheckVictory(b);
    }
}
