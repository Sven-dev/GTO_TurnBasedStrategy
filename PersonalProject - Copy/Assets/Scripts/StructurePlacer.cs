using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructurePlacer : Placer {

    public List<Structure> PrefabList;
    public Structure Tree;
    public List<Structure> StructureList;

    private void Awake()
    {
        //StructureList = new List<Structure>();
    }

    public void SpawnTree(Point p)
    {
        StructureList.Add(
            Instantiate(
                Tree, grid.TileArray[p.X, p.Y].transform.position, Quaternion.identity));
    }

    public void BuyUnit(int index)
    {
        /*
        if (tile != null)
        {
            //tile.place(StructureList[index]);
        }
        */
    }
}
