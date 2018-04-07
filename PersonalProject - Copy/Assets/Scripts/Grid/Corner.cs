using System.Collections.Generic;
using UnityEngine;

public class Corner : MonoBehaviour
{
    public Point Position;
    public List<Player> Ownership;

    [HideInInspector]
    public int GCost;

    [HideInInspector]
    public int HCost;

    [HideInInspector]
    public int FCost
    {
        get { return GCost + HCost; }
    }

    [HideInInspector]
    public Corner Parent;

    /// <summary>
    /// Adds a player to the list of owners
    /// </summary>
    /// <param name="p"> the player that needs to be added</param>
    public void AddOwnerShip(Player p)
    {
        if (!Ownership.Contains(p))
        {
            Ownership.Add(p);
        }
    }
}