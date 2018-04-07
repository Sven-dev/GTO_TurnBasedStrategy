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

    /// <summary>
    /// Updates the progress of the slider
    /// </summary>
    /// <param name="v">The value</param>
    public void UpdateSlider(int v)
    {
        s.value = v;
    }
}
