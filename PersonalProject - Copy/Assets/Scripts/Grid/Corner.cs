using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corner : MonoBehaviour {

    public List<Player> Ownership;

    public void AddOwnerShip(Player p)
    {
        if (!Ownership.Contains(p))
        {
            Ownership.Add(p);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
