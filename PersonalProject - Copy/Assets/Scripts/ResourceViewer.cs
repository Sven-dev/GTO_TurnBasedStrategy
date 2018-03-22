using UnityEngine;
using UnityEngine.UI;

public class ResourceViewer : MonoBehaviour {

    public Resource resource;
    private Text field;

    private void Awake()
    {
        field = GetComponent<Text>();
        resource.OnAmountChange += OnChange;
    }

    void OnChange()
    {
        field.text = resource.gameObject.name + ": " + resource.Amount;
    }
}