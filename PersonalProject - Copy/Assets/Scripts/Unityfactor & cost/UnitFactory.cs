using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitFactory : MonoBehaviour
{
    public Structure prefab;
    public List<Cost> unitCost;

    /// <summary>
    /// Checks if there's enough resource to by the unit, returns the prefab
    /// </summary>
    /// <returns></returns>
    public Structure Buy()
    {
        bool affordable = true;
        foreach (Cost c in unitCost)
        {
            if (c.resource.Amount - c.cost < 0)
            {
                affordable = false;
                c.resource.Insufficient();
            }           
        }

        if (affordable)
        {
            foreach (Cost c in unitCost)
            {
                c.resource.Change(-c.cost);
            }

            return prefab;
        }

        return null;
    }
}