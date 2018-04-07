using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : Structure
{
    public int Amount;
    public Resource Resource;

    /// <summary>
    /// Adds resources to the player's stock
    /// </summary>
    public void CollectResources()
    {
        print("Collecting resources");
        this.Resource.Change(this.Amount);
    }

    /// <summary>
    /// Attaches to the resrource-spot when spawned
    /// </summary>
    public void AttachResources()
    {
        int index = (int)transform.parent.GetChild(0).GetComponent<ResourceSpot>().Type;
        Resource[] r = Owner.GetResources();
        this.Resource = r[index];
    }

    /// <summary>
    /// Adds variables when the Collector is first spawned
    /// </summary>
    /// <param name="p">The player who owns the Attacker</param>
    /// <param name="g">A reference to the field</param>
    public override void StartUp(Player p, Grid g)
    {
        base.StartUp(p, g);
        Owner.OnTurnChange += CollectResources;
        AttachResources();
    }
}