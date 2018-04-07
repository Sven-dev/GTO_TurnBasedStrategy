using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabelShower : MonoBehaviour
{
    public GameObject Rotatable;
    public Text Hp;
    public GameObject Mesh;
    private void OnEnable()
    {
        UpdatePos();
        StartCoroutine(Rotate());
    }

    IEnumerator Rotate()
    {
        while (enabled)
        {
            Rotatable.transform.Rotate((Vector3.down * 10) * Time.deltaTime);
            yield return null;
        }
    }

    public void UpdateLabel(int Health)
    {
        Hp.text = Health.ToString();
    }

    public void UpdatePos()
    {
        transform.position = new Vector3(transform.position.x, 10 * Mesh.transform.localScale.y, transform.position.z);
        transform.LookAt(2 * transform.position - Camera.main.transform.position);

        Vector3 rotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(rotation.x, 0, 0);
    }
}
