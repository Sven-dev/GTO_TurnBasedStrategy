using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrainer : Structure
{
    public int Range;
    public List<Tile> Tiles;

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
    }

    public override void UnsetStructure()
    {
        base.UnsetStructure();
        foreach (Tile t in Tiles)
        {
            t.SetNeutral();
        }
    }
}
