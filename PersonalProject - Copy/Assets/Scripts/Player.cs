using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Grid grid;
    public StructurePlacer StructurePlacer;
    public RootPlacer RootPlacer;

    // Use this for initialization
    void Start ()
    {
        StructurePlacer.grid = this.grid;
        RootPlacer.grid = this.grid;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void StartGame()
    {
        Point p = grid.GetFreeTilePos();

        StructurePlacer.SpawnTree(p);
        RootPlacer.SpawnStartRoots(p, this);
    }

    public void StartTurn()
    {
        gameObject.SetActive(true);
    }

    public void EndTurn()
    {
        gameObject.SetActive(false);
    }
}
