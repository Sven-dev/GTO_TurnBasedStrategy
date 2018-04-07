using UnityEngine;

[System.Serializable]
public class Resource : MonoBehaviour
{
    public delegate void AmountChanged();
    public event AmountChanged OnAmountChange;

    public delegate void NotEnough();
    public event NotEnough OnNotEnough;

    public int Amount;

    public void Start()
    {
        if (OnAmountChange != null)
        {
            OnAmountChange();
        }
    }

    /// <summary>
    /// Changes the amount of resource available
    /// </summary>
    /// <param name="Cost">The amount that needs to change</param>
    public void Change(int Cost)
    {
        Amount += Cost;
        if (OnAmountChange != null)
        {
            OnAmountChange();
        }
    }

    /// <summary>
    /// triggers an eventhandler if there isn't enough resource
    /// </summary>
    public void Insufficient()
    {
        OnNotEnough();
    }
}
