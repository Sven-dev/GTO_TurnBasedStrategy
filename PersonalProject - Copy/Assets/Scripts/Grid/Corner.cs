using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corner : MonoBehaviour {

    public Point Position;
    public List<Player> Ownership;

    [HideInInspector]
    public int GCost;

    [HideInInspector]
    public int HCost;

    [HideInInspector]
    public int FCost
    {
        get { return GCost + HCost; }
    }

    [HideInInspector]
    public Corner Parent;

    public void AddOwnerShip(Player p)
    {
        if (!Ownership.Contains(p))
        {
            Ownership.Add(p);
        }
    }

    // Use this for initialization
    void Start ()
    {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
