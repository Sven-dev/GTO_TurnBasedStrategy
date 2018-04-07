using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceViewer : MonoBehaviour {

    public Resource resource;
    private Text field;
    private int oldValue;
    private Color Default;

    private void Awake()
    {
        oldValue = 0;
        field = GetComponent<Text>();
        Default = field.color;

        resource.OnAmountChange += OnChange;
        OnChange();
        resource.OnNotEnough += OnInsufficient;
    }

    void OnChange()
    {
        if (this.isActiveAndEnabled)
        {
            StartCoroutine(_change());
        }
    }

    void OnInsufficient()
    {
        StartCoroutine(_Insufficient());
    }

    IEnumerator _change()
    {
        if (resource.Amount > oldValue)
        {
            field.color = Color.green;
        }
        else if (resource.Amount < oldValue)
        {
            field.color = Color.red;
        }

        oldValue = resource.Amount;
        field.text = resource.name + ": " + resource.Amount.ToString();
        yield return new WaitForSeconds(1f);
        field.color = Default;
    }

    IEnumerator _Insufficient()
    {
        int times = 0;
        Color old = field.color;
        field.color = Color.red;

        while (times <= 4)
        {
            field.transform.Translate(Vector3.right);
            yield return new WaitForSeconds(0.05f);
            field.transform.Translate(Vector3.left);
            yield return new WaitForSeconds(0.05f);

            times++;
        }

        field.color = old;
    }
}