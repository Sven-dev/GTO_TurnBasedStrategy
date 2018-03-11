using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    public Structure OnThisTile;

    public bool Free { get; private set; }

    public void place(Structure s)
    {
        OnThisTile = Instantiate(s);
        OnThisTile.SetPosition(transform.position);
        Free = false;
    }

	// Use this for initialization
	void Awake()
    {
        Free = true;
    }
}
