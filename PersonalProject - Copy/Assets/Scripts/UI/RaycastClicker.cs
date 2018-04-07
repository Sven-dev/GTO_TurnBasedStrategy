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
            if (Selected != null)
            {
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
    }

    //On right click, attacks the clicked structure with the selected attacker
    public void Attack(Tile Selected)
    {
        Structure attacker = Manager.GetCurrentPlayer().SelectedTile.GetStructure();
        Structure defender = Selected.GetStructure();

        //Check if selected tile is an attacker                   
        if (attacker != null && defender != null && attacker is Attacker && defender.Owner != attacker.Owner)
        {
            Attacker a = attacker as Attacker;

            //Check if tile is in range
            if (a.Tiles.Contains(Selected) && a.Fired == false)
            {
                //Deal Damage
                a.DealDamage(defender);
            }
        }
    }

    public void Select(Tile Selected)
    {
        Manager.SelectTile(Selected);
        Structure s = Selected.GetStructure();

        // if Tile has a structure on it                     
        if (s != null && s is Attacker)
        {
            //display attack range
            Attacker a = s as Attacker;
            a.DisplayRange();
        }
    }
}