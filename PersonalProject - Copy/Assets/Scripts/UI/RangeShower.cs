using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeShower : MonoBehaviour {

    public PlayerManager PlayerManager;
    public Grid Grid;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowRange(int index)
    {
        Player player = PlayerManager.GetCurrentPlayer();
        Tile selected = player.SelectedTile;
        if (selected != null)
        {
            Structure s = player.structures[index].prefab.GetComponent<Structure>();
            int range = -1;
            bool limited = false;
            if (s is Attacker)
            {
                Attacker a = (Attacker)s;
                range = a.MaxRange;
            }
            else if (s is Terrainer)
            {
                Terrainer t = (Terrainer)s;
                range = t.Range;
                limited = true;
            }
            else if (s is BaseTree)
            {
                BaseTree b = (BaseTree)s;
                range = b.Range;
                limited = true;
            }

            if (range != -1)
            {
                List<Tile> tiles;
                if (limited)
                {
                    tiles = Grid.GetRangeTiles(selected, range, player);
                }
                else
                {
                    tiles = Grid.GetRangeTiles(selected, range);
                }

                foreach (Tile t in tiles)
                {
                    if (t != selected)
                    {
                        t.Select();
                    }
                }
            }
        }
    }

    public void HideRange(int index)
    {
        Player player = PlayerManager.GetCurrentPlayer();
        Tile selected = player.SelectedTile;
        if (selected != null)
        {
            Structure s = player.structures[index].prefab.GetComponent<Structure>();
            int range = -1;
            bool limited = false;
            if (s is Attacker)
            {
                Attacker a = (Attacker)s;
                range = a.MaxRange;
            }
            else if (s is Terrainer)
            {
                Terrainer t = (Terrainer)s;
                range = t.Range;
                limited = true;
            }
            else if (s is BaseTree)
            {
                BaseTree b = (BaseTree)s;
                range = b.Range;
                limited = true;
            }

            if (range != -1)
            {
                List<Tile> tiles;
                if (limited)
                {
                    tiles = Grid.GetRangeTiles(selected, range, player);
                }
                else
                {
                    tiles = Grid.GetRangeTiles(selected, range);
                }

                foreach (Tile t in tiles)
                {
                    if (t != selected)
                    {
                        t.Deselect();
                    }
                }
            }
        }
    }
}
