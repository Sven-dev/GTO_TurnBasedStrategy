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
            Cast(0);
        }

        if (Input.GetMouseButtonDown(1) && Manager.GetCurrentPlayer().SelectedTile != null)
        {
            Cast(1);
        }
	}

    void Cast(int button)
    {
        Ray ray = Cam.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow, 10);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);

        if (hit.collider != null)
        {
            Tile Selected = hit.collider.GetComponent<Tile>();

            if (button == 0)
            {
                Select(Selected);
            }
            else if (button == 1)
            {
                Attack(Selected);
            }
            
        }
    }

    //On right click, attacks the clicked structure with the selected attacker
    public void Attack(Tile Selected)
    {
        Tile target = Manager.GetCurrentPlayer().SelectedTile;
        Structure attacker = target.GetStructure();

        Structure defender = Selected.GetStructure();

        //Check if selected tile is an attacker                   
        if (attacker != null)
        {
            if (attacker is Attacker)
            {
                Attacker a = attacker as Attacker;

                //Check if tile is in range
                if (a.Tiles.Contains(target))
                {
                    //Deal Damage
                    a.DealDamage(defender);
                }
            }
        }
    }

    public void Select(Tile Selected)
    {
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
    }
}
