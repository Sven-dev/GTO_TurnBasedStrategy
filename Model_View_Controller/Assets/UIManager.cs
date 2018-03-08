using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager: MonoBehaviour {

    public Text LabelWood;

    public Resource Wood;

    // Use this for initialization
    void Start()
    {
        Wood.Change += UpdateUI;
    }

    public void UpdateUI()
    {
        LabelWood.text = "Wood: " + Wood.CurrentAmount.ToString();
    }
}