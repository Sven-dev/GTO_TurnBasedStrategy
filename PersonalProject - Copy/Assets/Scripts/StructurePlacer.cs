using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructurePlacer : Placer {

    public Structure Tree;
    public List<Structure> StructureList;

    public Point SpawnTree(Player p)
    {
        Tile t = grid.GetFreeTile();
        t.SetOwner(p);
        
        //grid.GetRangeTiles(t, p);
        
        return grid.TileToPoint(t);
    }

    public void BuyStructure(Tile t, int index, Player p)
    {/*
        if (t.Owner == p)
        {
            foreach (Resource r in GetComponentsInChildren<Resource>())
            {
                if (r.Type == ResourceType.Water)
                {
                    Resource Cost = StructureList[index].GetComponentInChildren<Resource>();
                    if (r.Amount - Cost.Amount >= 0)
                    {
                        r.ChangeAmount(-Cost.Amount);
                        Structure s = StructureList[index];
                        t.place(s);

                        if (s is Terrainer)
                        {
                            grid.AddPlayerTiles(t, p);
                        }
                    }
                    else
                    {
                        Debug.Log("You don't have enough " + r.Type.ToString());
                    }
                }
            }
        }
        */
    }
}
