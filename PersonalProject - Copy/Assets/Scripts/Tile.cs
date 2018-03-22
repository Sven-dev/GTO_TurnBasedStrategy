using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    public Color Default;
    public Point Position;
    public Player Owner;

    private Renderer r;

    private void Awake()
    {
        r = GetComponent<Renderer>();
        Default = r.material.color;
    }

    public void SetNeutral()
    {
        Owner = null;
        r.material.color = Default;
    }

    public void SetOwner(Player p)
    {
        if (Owner == null)
        {
            Owner = p;
            r.material.color = p.PlayerColor;
        }
    }
}
