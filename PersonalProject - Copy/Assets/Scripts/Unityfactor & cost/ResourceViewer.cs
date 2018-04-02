using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceViewer : MonoBehaviour {

    public Resource resource;
    private Text field;
    private int oldValue;

    private void Awake()
    {
        oldValue = 0;
        field = GetComponent<Text>();
        resource.OnAmountChange += OnChange;
        resource.OnNotEnough += OnInsufficient;
    }

    void OnChange()
    {
        StartCoroutine(_change(resource.Amount));
    }

    void OnInsufficient()
    {
        StartCoroutine(_Insufficient());
    }

    IEnumerator _Insufficient()
    {
        int times = 0;
        Color old = field.color;
        field.color = Color.red;

        while (times <= 4)
        {
            field.transform.Translate(Vector3.right*5);
            yield return new WaitForSeconds(0.05f);
            field.transform.Translate(Vector3.left*5);
            yield return new WaitForSeconds(0.05f);

            times++;
        }

        field.color = old;
    }

    IEnumerator _change(int value)
    {
        int direction = -1;

        if (value > oldValue)
        {
            direction = 1;
        }

        while (value != oldValue)
        {
            oldValue += direction;
            field.text = resource.gameObject.name + ": " + oldValue;
            yield return new WaitForSeconds(0.1f);
        }
    }
}