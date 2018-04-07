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

    /// <summary>
    /// Adds resources to the player's stock
    /// </summary>
    public void CollectResources()
    {
        foreach(Resource r in Resources)
        {
            r.Change(Amount);
        }
    }

    /// <summary>
    /// Finds and attaches to the player's resources on spawn
    /// </summary>
    public void AttachResources()
    {
        foreach (Resource r in Owner.GetResources())
        {
            Resources.Add(r);
        }
    }

    /// <summary>
    /// Converts all tiles in range of the BaseTree to the player
    /// </summary>
    public void ConvertTiles()
    {
        Tiles = Grid.GetRangeTiles(transform.parent.transform.GetComponent<Tile>(), Range, Owner);
        foreach (Tile t in Tiles)
        {
            t.SetOwner(Owner);
        }
    }

    /// <summary>
    /// Sets all tiles in range of the basetree back to neutral
    /// </summary>
    public void NeutralizeTiles()
    {
        foreach (Tile t in Tiles)
        {
            t.SetNeutral();
        }
    }

    /// <summary>
    /// Adds variables when the BaseTree is first spawned
    /// </summary>
    /// <param name="p">The player who owns the Attacker</param>
    /// <param name="g">A reference to the field</param>
    public override void StartUp(Player p, Grid g)
    {
        base.StartUp(p, g);
        ConvertTiles();
        Owner.OnTurnChange += CollectResources;
        AttachResources();
    }

    /// <summary>
    /// Make the tree shrink upon taking damage, returns a death state
    /// </summary>
    /// <param name="damage">The amount of damage dealt</param>
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

    /// <summary>
    /// Checks of the player has enough resources, and if so, grows the tree
    /// </summary>
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

    /// <summary>
    /// Changes the tree's scale
    /// </summary>
    /// <param name="amount">The amount it needs to be changed with</param>
    void ChangeSize(int amount)
    {
        transform.GetChild(0).localScale += new Vector3(0.1f, 0.1f, 0.1f) * amount;
        Label.UpdatePos();
    }

    /// <summary>
    /// Sets all land owned by this tree back to neutral
    /// </summary>
    public override void UnsetStructure()
    {
        base.DestroyStructure();
        foreach (Tile t in Tiles)
        {
            t.SetNeutral();
        }
    }
}
