using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrainer : MonoBehaviour
{
    public Grid Grid;
    public int Range;

    public void ConvertTiles(Player p)
    {
        List<Tile> tiles = Grid.AddPlayerTiles(transform.parent.transform.GetComponent<Tile>(), p);
        foreach(Tile t in tiles)
        {
            t.SetOwner(p);
        }
    }
}
