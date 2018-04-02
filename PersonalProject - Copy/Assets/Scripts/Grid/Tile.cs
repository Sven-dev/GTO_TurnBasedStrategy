using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    public Color Default;
    public Point Position;
    public Player Owner;

    private bool Selected;
    private Renderer r;

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
            //Instantiate new object with player corner
            //Place new object under current object, rotated 180°
            //Start coroutine _setOwner()

            Owner = p;
            r.material.color = p.PlayerColor;
        }
    }

    IEnumerator _setOwner(GameObject Old, GameObject New)
    {
        /*while (rotateobject.transform.rotation.x >= 0)
        {
            //Rotate both objects until current object is rotated 0°
            rotateobject.transform.rotate(Vector3.left);
        }

        rotateobject.transform.rotation = Vector3.zero;

        Destroy(Old);
        Destroy old object */
        yield return null;
    }

    public void Select()
    {
        Selected = true;
        GetComponent<Renderer>().material.color = Color.white;
    }

    public void Deselect()
    {
        Selected = false;
        if (Owner == null)
        {
            GetComponent<Renderer>().material.color = Default;
            return;
        }

        GetComponent<Renderer>().material.color = Owner.PlayerColor;
    }

    //Pulsates the tile if selected
    IEnumerator _select()
    {
        //fades aplha right now, needs to fade between selected and normal color.
        float alpha = 0;
        int direction = 1;
        while (Selected)
        {
            r.material.color = new Color(
                r.material.color.r,
                r.material.color.g,
                r.material.color.b,
                r.material.color.a + direction * alpha);
            alpha += direction * 0.01f;

            if (alpha >= 1)
            {
                direction = -1;
            }
            else if (alpha <= 0)
            {
                direction = 1;
            }

            yield return null;
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