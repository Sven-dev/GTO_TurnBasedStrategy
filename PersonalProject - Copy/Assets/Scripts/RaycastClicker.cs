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
	void Update ()
    {
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
            Manager.SelectTile(Selected);

            Structure s = Selected.GetStructure();
            Resource r = Selected.GetResource();

            //Tile has a structure on it                     
            if (s != null)
            {
                //Display health

                if (s is Attacker)
                {
                    Attacker a = s as Attacker;
                    //display attack range
                }
                else if (s is Terrainer)
                {
                    //display terrain range
                }
            }
            //Tile has a resource on it
            else if (r != null)
            {
                //Display resource
            }

            // Tile is owned by the player
            if (Selected.Owner == Manager.GetCurrentPlayer())
            {
                if (s == null)
                {
                    //enable attacker and terrainer buy buttons
                }
                if (r != null)
                {
                    //enable collector buy button
                }
            }

            #region old code
                /*
                Structure s = Selected.GetStructure();
                if (Selected.transform.childCount > 0)
                {
                    Resource r = Selected.transform.GetChild(0).GetComponent<Resource>();
                    //Structure s = Selected.transform.GetChild(0).GetComponent<Resource>()
                    if (r != null)
                    {
                        //Selected a resource tile
                    }
                    else //if ()
                    {

                    }
                    /* resource
                     * collector
                     * spreader
                     * attacker
                     * other
                     
                }
                */
                #endregion
        }
    }
}
