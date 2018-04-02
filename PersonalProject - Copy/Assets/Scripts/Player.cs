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
            BaseTree b = structures[0].Buy() as BaseTree;
            b = Place(b, SelectedTile) as BaseTree;

            BaseTile = SelectedTile;
            b.GrowCost.resource = Resources[2];
        }
    }

    public void GetThing(int index)
    {
        if (SelectedTile != null && SelectedTile.GetComponentInChildren<Structure>() == null && SelectedTile.Owner == this)
        {
            Structure s = structures[index].Buy();
            s = Place(s, SelectedTile);

            Tile start = RootPlacer.GetClosestTile(SelectedTile, this);
            Structure Parent = start.GetComponentInChildren<Structure>();
            Parent.Children.Add(s);

            s.Roots.AddRange(RootPlacer.GetClosestPath(start, SelectedTile, this));
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
        StartCoroutine(_place(s));
        s.StartUp(this, grid);
        s.Roots = RootPlacer.SpawnAround(SelectedTile, this);
        return s;
    }

    IEnumerator _place(Structure s)
    {
        while (s.transform.position.y < 0)
        {
            s.transform.Translate(Vector3.up * 3 * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(2f);

        foreach(Root r in s.Roots)
        {
            r._turnAround();
            yield return new WaitForSeconds(0.5f);
        }
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
        if (b != null)
        {
            b.Grow();
            SliderContoller.UpdateSlider(b.Growthcurrent);
            VictoryController.CheckVictory(b);
        }
    }
}
