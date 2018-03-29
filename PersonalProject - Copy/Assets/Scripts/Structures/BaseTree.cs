using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTree : Structure
{
    public Grid Grid;
    public int Range;

    [Space]
    public int Amount;
    public List<Resource> Resources;

    [Space]
    public int Growthcurrent;
    public int GrowthMax;
    public Cost GrowCost;

    public void CollectResources()
    {
        foreach(Resource r in Resources)
        {
            r.Amount += this.Amount;
        }
    }

    public void AttachResources()
    {
        foreach (Resource r in Owner.GetResources())
        {
            Resources.Add(r);
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

        AttachResources();

    }

    public void Grow()
    {
        if (GrowCost.resource.Amount - GrowCost.cost >= 0)
        {
            GrowCost.resource.Change(-GrowCost.cost);
            Growthcurrent += GrowCost.cost;
            return;
        }

        print("You don't have enough " + GrowCost.resource.ToString());
    }
}
