using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructurePlacer : MonoBehaviour {

    public GridSpawner Grid;
    public List<Structure> StructureList;

    public void BuyUnit(int index)
    {
        GridTile tile = Grid.GetFreeTile();
        if (tile != null)
        {
            tile.place(StructureList[index]);
        }
    }
}
