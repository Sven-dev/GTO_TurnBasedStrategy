using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceViewer : MonoBehaviour {

    public Resource resource;
    private Text field;

    private void Start()
    {
        field = GetComponent<Text>();
        resource.Change += OnChange;
    }

    void OnChange()
    {
        field.text = "Water: " + resource.Amount;
    }
}