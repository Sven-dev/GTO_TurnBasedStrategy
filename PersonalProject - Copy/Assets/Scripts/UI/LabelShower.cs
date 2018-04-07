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

    /// <summary>
    /// Rotates the object around
    /// </summary>
    IEnumerator Rotate()
    {
        while (enabled)
        {
            Rotatable.transform.Rotate((Vector3.down * 10) * Time.deltaTime);
            yield return null;
        }
    }

    /// <summary>
    /// Updates the amount of health on the label
    /// </summary>
    /// <param name="Health">The amount of health</param>
    public void UpdateLabel(int Health)
    {
        Hp.text = Health.ToString();
    }

    /// <summary>
    /// Positions the object above the structure it belongs to, and rotated towards the camera
    /// </summary>
    public void UpdatePos()
    {
        transform.position = new Vector3(transform.position.x, 10 * Mesh.transform.localScale.y, transform.position.z);
        transform.LookAt(2 * transform.position - Camera.main.transform.position);

        Vector3 rotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(rotation.x, 0, 0);
    }
}