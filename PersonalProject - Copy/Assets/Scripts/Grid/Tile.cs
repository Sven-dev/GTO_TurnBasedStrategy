using System.Collections;
using UnityEngine;

public class Tile : MonoBehaviour {

    public Color Default;
    public Point Position;
    public Player Owner;

    public bool Selected;
    private Renderer r;

    /// <summary>
    /// Returns the color of the owner, or the default color if owner is null
    /// </summary>
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

    /// <summary>
    /// Sets the owner back to null
    /// </summary>
    public void SetNeutral()
    {
        Owner = null;
        r.material.color = Default;
    }

    /// <summary>
    /// Sets the owner to p
    /// </summary>
    /// <param name="p">The player who gained ownership</param>
    public void SetOwner(Player p)
    {
        if (Owner == null)
        {
            Owner = p;
            r.material.color = p.PlayerColor;
        }
    }

    /// <summary>
    /// Selects the tile
    /// </summary>
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

    /// <summary>
    /// Deselects the tile
    /// </summary>
    public void Deselect()
    {
        Selected = false;

        Structure s = GetComponentInChildren<Structure>();
        if (s != null)
        {
            s.HideData();
        }
    }

    /// <summary>
    /// Pulsates the tile while selected
    /// </summary>
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

    /// <summary>
    /// Checks what structure is on the tile, and returns it
    /// </summary>
    public Structure GetStructure()
    {
        return transform.GetComponentInChildren<Structure>();
    }
}