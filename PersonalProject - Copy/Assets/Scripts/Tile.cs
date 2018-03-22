using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    public Color Default;
    public Point Position;

    public Structure OnThisTile;
    public Player Owner;

    private Renderer r;

    private void Awake()
    {
        r = GetComponent<Renderer>();
        Default = r.material.color;
    }

    public void place(Structure s)
    {
        OnThisTile = Instantiate(s);
        OnThisTile.SetPosition(transform.position);
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
