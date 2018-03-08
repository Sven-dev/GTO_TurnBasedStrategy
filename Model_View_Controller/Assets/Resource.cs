using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour {

    public int StartAmount;
    public delegate void AmountChanged();
    public event AmountChanged Change;

    public int CurrentAmount { get; private set; }

	// Use this for initialization
	void Awake()
    {
        CurrentAmount = StartAmount;
        Change();
	}

    public void AddAmount(int Amount)
    {
        CurrentAmount += Amount;
        Change();
    }
}
