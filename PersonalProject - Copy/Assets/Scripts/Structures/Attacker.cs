using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : Structure
{
    public int Range;
    private RangeShower RangeShower;

    public int Damage;
    public bool Fired;
    public List<Cost> Costs;

    public List<Tile> Tiles;

    /// <summary>
    /// Adds variables when the Attacker is first spawned
    /// </summary>
    /// <param name="p">The player who owns the Attacker</param>
    /// <param name="g">A reference to the field</param>
    public override void StartUp(Player p, Grid g)
    {
        base.StartUp(p, g);
        RangeShower = Camera.main.GetComponent<RangeShower>();
        Owner.OnTurnChange += ReloadWeapon;
        ReloadWeapon();
        Tiles = Grid.GetRangeTiles(transform.parent.transform.GetComponent<Tile>(), Range);
        Costs.Add(new Cost(p.transform.GetChild(1).GetChild(1).GetComponent<Resource>(), 1));
    }

    /// <summary>
    /// Displays the firing range of the Attacker
    /// </summary>
    public void DisplayRange()
    {
        RangeShower.ShowRange(this);
        StartCoroutine(_selected());
    }

    /// <summary>
    /// Calls HideRange when the Attacker is deselected 
    /// </summary>
    /// <returns></returns>
    IEnumerator _selected()
    {
        Tile t = GetComponentInParent<Tile>();
        while (Owner.SelectedTile == t)
        {
            yield return null;
        }

        HideRange();
    }

    /// <summary>
    /// Hides the firing range of the Attacker
    /// </summary>
    void HideRange()
    {
        Tile selected = Owner.SelectedTile;
        foreach (Tile t in Tiles)
        {
            if (t != selected)
            {
                t.Deselect();
            }
        }
    }

    /// <summary>
    /// Sets Fired to false, allowing the Attacker to shoot again
    /// </summary>
    public void ReloadWeapon()
    {
        Fired = false;
    }

    /// <summary>
    /// Starts _turnTowards and turns firing off, if the player can afford it
    /// </summary>
    /// <param name="target">The target that is getting attacked</param>
    public void DealDamage(Structure target)
    {
        if (Buy())
        {
            StartCoroutine(_turnTowards(target));
            Fired = true;
        }
    }

    /// <summary>
    /// Checks if the player has enough resources to use the attack
    /// </summary>
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

    /// <summary>
    /// Turns the barrel of the Attacker towards the attacked Structure
    /// </summary>
    /// <param name="structure">The Structure getting attacked</param>
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

    /// <summary>
    /// Spawns a cannonball and fires it towards target
    /// </summary>
    /// <param name="cannon">The cannon mesh</param>
    /// <param name="target">The Structure getting attacked</param>
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