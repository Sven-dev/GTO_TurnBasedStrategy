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

    public abstract void StartUp(Player p, Grid g);

    private void Start()
    {
        Label.UpdateLabel(Health);
    }

    public void TakeDamage(int damage)
    {
        StartCoroutine(_takeDamage(damage));
    }

    IEnumerator _takeDamage(int damage)
    {
        Label.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        Health -= damage;
        if (Health < 0)
        {
            Health = 0;
        }

        Label.UpdateLabel(Health);
        yield return new WaitForSeconds(1.5f);

        if (Health == 0)
        {
            DestroyStructure();
            Destroy(gameObject);
        }

        Label.gameObject.SetActive(false);
    }

    public void DestroyStructure()
    {
        foreach (Root r in Roots)
        {
            Destroy(r);
        }
    }
}