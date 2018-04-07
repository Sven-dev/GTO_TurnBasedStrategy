using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : Structure
{
    public int Amount;
    public Resource Resource;

    public void CollectResources()
    {
        print("Collecting resources");
        this.Resource.Change(this.Amount);
    }

    public void AttachResources()
    {
        int index = (int)transform.parent.GetChild(0).GetComponent<ResourceSpot>().Type;
        Resource[] r = Owner.GetResources();
        this.Resource = r[index];
    }

    public override void StartUp(Player p, Grid g)
    {
        base.StartUp(p, g);
        Owner.OnTurnChange += CollectResources;
        AttachResources();
    }
}