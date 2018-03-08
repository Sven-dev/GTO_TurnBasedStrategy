using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodButton : MonoBehaviour {

    public Resource Wood;

    public void Click()
    {
        Wood.AddAmount(1);
    }
}
