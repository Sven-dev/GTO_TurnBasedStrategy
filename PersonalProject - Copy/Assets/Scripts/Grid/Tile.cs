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

    public void Select()
    {
        GetComponent<Renderer>().material.color = Color.white;
    }

    public void Deselect()
    {
        if (Owner == null)
        {
            GetComponent<Renderer>().material.color = Default;
            return;
        }

        GetComponent<Renderer>().material.color = Owner.PlayerColor;
    }
}