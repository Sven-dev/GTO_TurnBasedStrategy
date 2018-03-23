using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Structure : MonoBehaviour {

    public int Health;
    public Player Owner;

    public abstract void StartUp(Player p, Grid g);
}
