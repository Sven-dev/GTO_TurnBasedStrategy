using UnityEngine;

[System.Serializable]
public class ResourceTemp : MonoBehaviour
{
    public delegate void AmountChanged();
    public event AmountChanged Update;

    public int Amount;

    public void Start()
    {
        Update();
    }

    public void Change(int Cost)
    {
        Amount += Cost;
        Update();
    }
}
