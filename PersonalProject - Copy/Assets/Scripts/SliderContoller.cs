using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderContoller : MonoBehaviour {

    private Slider s;
	// Use this for initialization
	void Start ()
    {
        s = GetComponent<Slider>();
	}

    public void UpdateSlider(int v)
    {
        s.value = v;
    }
}
