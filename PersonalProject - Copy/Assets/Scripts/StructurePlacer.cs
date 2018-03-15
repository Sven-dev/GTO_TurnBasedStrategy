using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructurePlacer : Placer {

    public List<Structure> PrefabList;
    public Structure Tree;
    public List<Structure> StructureList;

    public void SpawnTree(Point p)
    {
        StructureList.Add(
            Instantiate(
                Tree, grid.TileArray[p.X, p.Y].transform.position, Quaternion.identity));
    }

    public void BuyUnit(int index)
    {
        foreach(Resource r in GetComponentsInChildren<Resource>())
        {
            if (r.Type == ResourceType.Water)
            {
                Resource Cost = PrefabList[index].GetComponentInChildren<Resource>();
                if (r.Amount - Cost.Amount >= 0)
                {
                    r.ChangeAmount(-Cost.Amount);
                    Tile tile = grid.GetFreeTile();
                    if (tile != null)
                    {
                        tile.place(StructureList[index]);
                        return;
                    }
                    else
                    {
                        Debug.LogError("Error: no tile selected");
                    }

                }
                else
                {
                    Debug.Log("You don't have enough " + r.Type.ToString());
                }
            }
        }
    }
}
