using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RangeType
{
    Line,
    Cone,
    AOE
}

public class Attacker : Structure
{
    public int MinRange;
    public int MaxRange;

    public Grid Grid;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void StartUp(Player p, Grid g)
    {
        Owner = p;
        this.Grid = g;
    }
}
