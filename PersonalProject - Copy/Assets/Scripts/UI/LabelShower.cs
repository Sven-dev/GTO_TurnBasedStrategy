using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabelShower : MonoBehaviour {

    public Camera Cam;
    public GameObject Rotatable;
    public Text Hp;

    private void Awake()
    {
        Cam = Camera.main;
    }

    private void OnEnable()
    {
        transform.LookAt(2 * transform.position - Cam.transform.position);
        StartCoroutine(Rotate());
    }

    IEnumerator Rotate()
    {
        while (this.enabled)
        {
            Rotatable.transform.Rotate((Vector3.down * 10) * Time.deltaTime);
            yield return null;
        }
    }

    public void UpdateLabel(int Health)
    {
        Hp.text = Health.ToString();
    }
}
