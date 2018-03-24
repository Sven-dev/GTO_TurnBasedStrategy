using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : Structure
{
    public ResourceSpot Spot;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void ConnectToSpot(ResourceSpot spot)
    {
        Spot = spot;
    }

    public void CollectResources()
    {
        transform.GetChild(0);
        foreach (Resource r in GetComponentsInChildren<Resource>())
        {
            //Add resource to player the collector belong to
        }
    }

    public override void StartUp(Player p, Grid g)
    {
        Owner = p;
        Owner.OnTurnChange += CollectResources;
    }
}
