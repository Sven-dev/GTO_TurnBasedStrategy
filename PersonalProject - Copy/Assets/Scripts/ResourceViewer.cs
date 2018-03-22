using UnityEngine;
using UnityEngine.UI;

public class ResourceViewer : MonoBehaviour {

    public ResourceTemp resource;
    private Text field;

    private void Start()
    {
        field = GetComponent<Text>();
        resource.Update += OnChange;
    }

    void OnChange()
    {
        field.text = resource.gameObject.name + ": " + resource.Amount;
    }
}