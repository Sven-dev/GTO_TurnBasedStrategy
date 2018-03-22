using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastClicker : MonoBehaviour {

    private Camera Cam;
    public PlayerManager Manager;

	// Use this for initialization
	void Start ()
    {
        Cam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Cast();
        }
	}

    void Cast()
    {
        Ray ray = Cam.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow, 10);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);

        if (hit.collider != null)
        {
            Tile Selected = hit.collider.GetComponent<Tile>();

            if (Selected.Owner = Manager.GetCurrentPlayer())
            {
                Manager.SelectTile(Selected);
            }
            else
            {
                /*
                 * If the current player has an attack-structure selected, 
                 * the clicked tile is in range,
                 * and there is an enemy structure on the clicked tile,
                 * attack it.
                 */
            }
        }
    }

    void CleanSelectedTile()
    {
        /* set shaders back to normal:
         * Tile
         * Structure
         * Range
         * Roots?
         */
    }
}
