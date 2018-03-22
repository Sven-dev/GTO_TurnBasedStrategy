using UnityEngine;

[System.Serializable]
public class Resource : MonoBehaviour
{
    public delegate void AmountChanged();
    public event AmountChanged OnAmountChange;

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
        OnAmountChange();
    }
}
