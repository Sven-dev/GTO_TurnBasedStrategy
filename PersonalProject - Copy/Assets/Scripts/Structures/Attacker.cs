using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : Structure
{
    public int Range;

    public int Damage;
    public bool Fired;
    public List<Cost> Costs;

    public List<Tile> Tiles;
    public Grid Grid;

    public override void StartUp(Player p, Grid g)
    {
        Owner = p;
        this.Grid = g;
        Owner.OnTurnChange += ReloadWeapon;
        ReloadWeapon();
        Tiles = Grid.GetRangeTiles(transform.parent.transform.GetComponent<Tile>(), Range, Owner);
        Costs.Add(new Cost(p.transform.GetChild(1).GetChild(1).GetComponent<Resource>(), 1));
    }

    public void DisplayRange()
    {
        foreach(Tile t in Tiles)
        {
            t.Select();
        }

        StartCoroutine(_selected());
    }

    IEnumerator _selected()
    {
        Tile t = GetComponentInParent<Tile>();
        while (Owner.SelectedTile == t)
        {
            yield return null;
        }

        foreach (Tile t2 in Tiles)
        {
            if (t2 != Owner.SelectedTile)
            {
                t2.Deselect();
            }
        }
    }


    public void ReloadWeapon()
    {
        Fired = false;
    }

    public void DealDamage(Structure target)
    {
        if (Buy())
        {
            StartCoroutine(_turnTowards(target));
            Fired = true;
        }
    }

    public bool Buy()
    {
        bool affordable = true;
        foreach (Cost c in Costs)
        {
            if (c.resource.Amount - c.cost < 0)
            {
                affordable = false;
                c.resource.Insufficient();
                print("You don't have enough " + c.resource.name);
            }
        }

        if (affordable)
        {
            foreach (Cost c in Costs)
            {
                c.resource.Change(-c.cost);
            }

            return true;
        }

        return false;
    }

    IEnumerator _turnTowards(Structure structure)
    {
        Transform cannon = transform.GetChild(0);
        Quaternion targetrotation = Quaternion.LookRotation(structure.transform.position - cannon.transform.position);
        float turnTime = 500;

        bool turning = true;
        while (turning && cannon.rotation != targetrotation && structure != null)
        {
            cannon.rotation = Quaternion.RotateTowards(cannon.rotation, targetrotation, turnTime * Time.deltaTime);
            yield return null;
        }

        if (structure != null)
        {
            StartCoroutine(_fire(cannon, structure));
            bool BaseTreeDead = structure.TakeDamage(Damage);

            if (BaseTreeDead)
            {
                Owner.VictoryController.Win(Owner);
            }
        }
    }

    IEnumerator _fire(Transform cannon, Structure target)
    {
        GameObject cannonball = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Renderer ballr = cannonball.GetComponent<Renderer>();
        ballr.material.color = Color.black;

        Transform barrel = cannon.GetChild(0).GetChild(0);
        cannonball.transform.parent = barrel;
        cannonball.transform.position = barrel.position;

        while (cannonball != null && target != null)
        {
            cannonball.transform.localPosition += Vector3.right * 100 * Time.deltaTime;

            if (cannonball.transform.position.x > target.transform.position.x)
            {
                ballr.material.color = new Color(
                    Color.black.r, 
                    Color.black.g, 
                    Color.black.b, 
                    ballr.material.color.a - 0.1f);

                if (ballr.material.color.a <= 0)
                {
                    Destroy(cannonball);
                }
            }

            yield return null;
        }

        Destroy(cannonball);
    }
}