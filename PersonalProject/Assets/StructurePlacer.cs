using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructurePlacer : MonoBehaviour {

    public GridSpawner Grid;

    public List<Structure> StructureList;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BuyUnit(int index)
    {
        GridTile tile = Grid.GetFreeTile();
        if (tile != null)
        {
            tile.OnThisTile = Instantiate(StructureList[index]);
            tile.UpdateTile();
        }
    }
}
