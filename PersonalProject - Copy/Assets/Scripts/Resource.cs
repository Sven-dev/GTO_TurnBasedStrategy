using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType
{
    Solar,
    Water,
    Co2
};

public class Resource : MonoBehaviour
{
    public delegate void AmountChanged();
    public event AmountChanged Change;

    public ResourceType Type;
    public int Amount;

    // Use this for initialization
    void Start()
    {
        if (Change != null)
        {
            Change();
        }
    }

    public void ChangeAmount(int Cost)
    {
        Amount += Cost;
        Change();
    }
} 