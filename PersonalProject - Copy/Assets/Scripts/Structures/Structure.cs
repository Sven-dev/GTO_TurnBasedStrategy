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

    public virtual void StartUp(Player p, Grid g)
    {
        Owner = p;
        Grid = g;
    }

    private void Start()
    {
        Label.UpdateLabel(Health);
    }

    public virtual bool TakeDamage(int damage)
    {
        Health -= damage;
        StartCoroutine(_takeDamage(damage));

        return false;
    }

    //Enables the Labelshower
    public void ShowData()
    {
        Label.gameObject.SetActive(true);
    }

    //Disables the Labelshower
    public void HideData()
    {
        Label.gameObject.SetActive(false);
    }

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

    public List<Structure> GetChildren()
    {
        List<Structure> children = new List<Structure>();
        foreach(Structure s in Children)
        {
            children.AddRange(s.GetChildren());
        }

        return children;
    }

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

    public void DestroyStructure()
    {
        foreach (Structure s in Children)
        {
            s.DestroyStructure();
        }

        Destroy(gameObject);
    }

    public void Place()
    {
        StartCoroutine(_placeStructure());
        StartCoroutine(_placeRoots());
    }

    IEnumerator _placeStructure()
    {
        while (transform.position.y < 0)
        {
            transform.Translate(Vector3.up * 3 * Time.deltaTime);
            yield return null;
        }
    }

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