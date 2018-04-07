using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTree : Structure
{
    public int Range;
    public List<Tile> Tiles;

    [Space]
    public int Amount;
    public List<Resource> Resources;

    [Space]
    public int GrowthMax;
    public Cost GrowCost;

    public void CollectResources()
    {
        foreach(Resource r in Resources)
        {
            r.Change(Amount);
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
        Tiles = Grid.GetRangeTiles(transform.parent.transform.GetComponent<Tile>(), Range, Owner);
        foreach (Tile t in Tiles)
        {
            t.SetOwner(Owner);
        }
    }

    public void NeutralizeTiles()
    {
        foreach (Tile t in Tiles)
        {
            t.SetNeutral();
        }
    }

    public override void StartUp(Player p, Grid g)
    {
        base.StartUp(p, g);
        ConvertTiles();
        Owner.OnTurnChange += CollectResources;
        AttachResources();
    }

    public override bool TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (Health <= 0)
        {
            return true;
        }

        ChangeSize(-damage);
        return false;
    }

    public void Grow()
    {
        if (GrowCost.resource.Amount - GrowCost.cost >= 0)
        {
            GrowCost.resource.Change(-GrowCost.cost);
            Health += GrowCost.cost;
            Label.UpdateLabel(Health);

            ChangeSize(GrowCost.cost);
            return;
        }

        GrowCost.resource.Insufficient();
    }

    void ChangeSize(int amount)
    {
        transform.GetChild(0).localScale += new Vector3(0.1f, 0.1f, 0.1f) * amount;
        Label.UpdatePos();
    }

    public override void UnsetStructure()
    {
        base.DestroyStructure();
        foreach (Tile t in Tiles)
        {
            t.SetNeutral();
        }
    }
}
