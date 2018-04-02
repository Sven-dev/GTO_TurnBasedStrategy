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

    public void Change(int Cost)
    {
        Amount += Cost;
        if (OnAmountChange != null)
        {
            OnAmountChange();
        }
    }

    public void Insufficient()
    {
        OnNotEnough();
    }
}
