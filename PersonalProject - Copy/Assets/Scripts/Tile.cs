using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    private Material Default;

    public Structure OnThisTile;
    public Player Owner;

    private void Awake()
    {
        Default = GetComponent<Material>();
    }

    public void place(Structure s)
    {
        OnThisTile = Instantiate(s);
        OnThisTile.SetPosition(transform.position);
    }

    public void SetOwner(Player p = null)
    {
        if (Owner == null)
        {
            Owner = p;
            if (p != null)
            {
                Material m = GetComponent<Material>();
                m = p.Color;
            }
            else
            {
                Material m = GetComponent<Material>();
                m = Default;
            }
        }
    }
}
