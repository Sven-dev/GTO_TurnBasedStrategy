using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrainer : Structure
{
    public Grid Grid;
    public int Range;

    public void ConvertTiles()
    {
        List<Tile> tiles = Grid.AddPlayerTiles(transform.parent.transform.GetComponent<Tile>(), Owner);
        foreach(Tile t in tiles)
        {
            print(t.ToString());
            t.SetOwner(Owner);
        }
    }

    public override void SetVariables(Player p, Grid g)
    {
        Owner = p;
        this.Grid = g;
        ConvertTiles();
    }
}
