using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrainer : Structure
{
    public int Range;
    public List<Tile> Tiles;

    /// <summary>
    /// Converts all tiles in range to the player
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
    /// Converts all tiles back to neutral
    /// </summary>
    public void NeutralizeTiles()
    {
        foreach (Tile t in Tiles)
        {
            t.SetNeutral();
        }
    }

    /// <summary>
    /// Adds variables when the Attacker is first spawned
    /// </summary>
    /// <param name="p">The player who owns the Attacker</param>
    /// <param name="g">A reference to the field</param>
    public override void StartUp(Player p, Grid g)
    {
        base.StartUp(p, g);
        ConvertTiles();
    }

    /// <summary>
    /// Unsets all visuals
    /// </summary>
    public override void UnsetStructure()
    {
        base.UnsetStructure();
        foreach (Tile t in Tiles)
        {
            t.SetNeutral();
        }
    }
}
