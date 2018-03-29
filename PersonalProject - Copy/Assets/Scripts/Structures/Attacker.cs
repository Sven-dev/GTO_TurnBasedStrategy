using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RangeType
{
    Line,
    Cone,
    AOE
}

public class Attacker : Structure
{
    public int MinRange;
    public int MaxRange;
    public int Spread;

    public int Damage;
    public bool Fired;

    public List<Tile> Tiles;
    public Grid Grid;

    public override void StartUp(Player p, Grid g)
    {
        Owner = p;
        this.Grid = g;
        Owner.OnTurnChange += ReloadWeapon;
        ReloadWeapon();
        Tiles = Grid.GetRangeTiles(transform.parent.transform.GetComponent<Tile>(), MaxRange, Owner);
    }

    public void DisplayRange()
    {
        foreach(Tile t in Tiles)
        {
            t.Select();
        }
    }

    public void HideRange()
    {
        foreach (Tile t in Tiles)
        {
            t.Deselect();
        }
    }

    public void ReloadWeapon()
    {
        Fired = false;
    }

    public void DealDamage(Structure target)
    {
        //Animation
        target.TakeDamage(Damage);
        Fired = true;
    }
}
