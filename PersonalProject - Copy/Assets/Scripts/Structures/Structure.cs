using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Structure : MonoBehaviour {

    public int Health;
    public Player Owner;
    [Space]
    public List<Structure> Children;
    public List<Root> Roots;
    [Space]
    public LabelShower Label;
    public Grid Grid;

    /// <summary>
    /// Adds variables when the Structure is first spawned
    /// </summary>
    /// <param name="p">The player who owns the Attacker</param>
    /// <param name="g">A reference to the field</param>
    public virtual void StartUp(Player p, Grid g)
    {
        Owner = p;
        Grid = g;
    }

    private void Start()
    {
        Label.UpdateLabel(Health);
    }

    /// <summary>
    /// Detracts health and starts _takeDamage, returns a deathstate
    /// </summary>
    /// <param name="damage">The amount of damage taken</param>
    public virtual bool TakeDamage(int damage)
    {
        Health -= damage;
        StartCoroutine(_takeDamage(damage));

        return false;
    }

    /// <summary>
    /// Enables the Labelshower
    /// </summary>
    public void ShowData()
    {
        Label.gameObject.SetActive(true);
    }

    /// <summary>
    /// Disables the Labelshower
    /// </summary>
    public void HideData()
    {
        Label.gameObject.SetActive(false);
    }

    /// <summary>
    /// Gives the player feedback on the amount of damage taken by updating the label, and destroying the Structure if health is 0
    /// </summary>
    /// <param name="damage">The amount of damage taken</param>
    IEnumerator _takeDamage(int damage)
    {
        Label.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);
        if (Health < 0)
        {
            Health = 0;
        }

        Label.UpdateLabel(Health);
        yield return new WaitForSeconds(2f);

        if (Health == 0)
        {
            UnsetStructure();
            Grid.RecalculateOwnership(this);
        }

        Label.gameObject.SetActive(false);
    }

    /// <summary>
    /// Gets all Structures under this one in the hierarchy
    /// </summary>
    public List<Structure> GetChildren()
    {
        List<Structure> children = new List<Structure>();
        foreach(Structure s in Children)
        {
            children.AddRange(s.GetChildren());
        }

        return children;
    }

    /// <summary>
    /// Unsets all visuals of itself and all children in the heirarchy
    /// </summary>
    public virtual void UnsetStructure()
    {
        foreach(Structure s in Children)
        {
            if (s != null)
            {
                s.UnsetStructure();
            }
        }

        foreach (Root r in Roots)
        {
            if (r != null)
            {
                r.DestroyRoot();
            }
        }
    }

    /// <summary>
    /// Destroys this structure and all children in the hierarchy
    /// </summary>
    public void DestroyStructure()
    {
        foreach (Structure s in Children)
        {
            s.DestroyStructure();
        }

        Destroy(gameObject);
    }

    /// <summary>
    /// Starts _placeStructure and _placeRoots
    /// </summary>
    public void Place()
    {
        StartCoroutine(_placeStructure());
        StartCoroutine(_placeRoots());
    }

    /// <summary>
    /// Slowly moves the Structure out of the ground
    /// </summary>
    /// <returns></returns>
    IEnumerator _placeStructure()
    {
        while (transform.position.y < 0)
        {
            transform.Translate(Vector3.up * 3 * Time.deltaTime);
            yield return null;
        }
    }

    /// <summary>
    /// Places all Roots, and starts TurnAround for each one of them
    /// </summary>
    IEnumerator _placeRoots()
    {
        yield return new WaitForEndOfFrame();
        foreach (Root r in Roots)
        {
            r.TurnAround();
            yield return new WaitForSeconds(0.25f);
        }
    }
}