﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrainer : Structure
{
    public Grid Grid;
    public int Range;

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
    }
}
