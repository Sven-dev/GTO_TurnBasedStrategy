using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    public Color Default;
    public Point Position;
    public Player Owner;

    public bool Selected;
    private Renderer r;

    public Color Color
    {
        get
        {
            if (Owner != null)
            {
                return Owner.PlayerColor;
            }

            return Default;
        }
    }

    private void Awake()
    {
        r = GetComponent<Renderer>();
        Default = r.material.color;

        Selected = false;
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
        Selected = true;
        StartCoroutine(_select());

        Structure s = GetComponentInChildren<Structure>();
        if (s != null)
        {
            s.ShowData();
        }
    }

    public void Deselect()
    {
        Selected = false;

        Structure s = GetComponentInChildren<Structure>();
        if (s != null)
        {
            s.HideData();
        }
    }

    //Pulsates the tile if selected
    IEnumerator _select()
    {
        float t = 0;
        bool direction = true;
        float duration = 0.65f;

        while (Selected)
        {
            t += Time.deltaTime / duration;
            if (direction)
            {
                r.material.color = Color.Lerp(Color, Color.white, t);
            }
            else
            {
                r.material.color = Color.Lerp(Color.white, Color, t);
            }

            if (t > 1)
            {
                t = 0;
                direction = !direction;
            }

            yield return null;
        }

        if (Owner == null)
        {
            GetComponent<Renderer>().material.color = Default;
        }
        else
        {
            GetComponent<Renderer>().material.color = Owner.PlayerColor;
        }
    }

    //Checks what structure is on the tile, and returns it
    public Structure GetStructure()
    {
        return transform.GetComponentInChildren<Structure>();
    }

    public Resource GetResource()
    {
        return GetComponentInChildren<Resource>();
    }
}