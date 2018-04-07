using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RangeShower : MonoBehaviour {

    public PlayerManager PlayerManager;
    public Grid Grid;
    public List<EventTrigger> Triggers;

    public void ShowRange(Structure s)
    {
        Player player = PlayerManager.GetCurrentPlayer();
        Tile selected = player.SelectedTile;
        if (selected != null)
        {
            List<Tile> tiles = GetTiles(s, selected, player);

            foreach (Tile t in tiles)
            {
                if (t != selected)
                {
                    t.Select();
                }
            }
        }
    }

    List<Tile> GetTiles(Structure s, Tile selected, Player player)
    {
        int range = int.MinValue;
        bool limited = false;
        if (s is Attacker)
        {
            Attacker a = (Attacker)s;
            range = a.Range;
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

        if (range != int.MinValue)
        {
            if (limited)
            {
                return Grid.GetRangeTiles(selected, range, player);
            }

            return Grid.GetRangeTiles(selected, range);
        }

        return null;
    }

    public void HideRange(Structure s)
    {
        Player player = PlayerManager.GetCurrentPlayer();
        Tile selected = player.SelectedTile;
        if (selected != null)
        {
            List<Tile> tiles = GetTiles(s, selected, player);

            foreach (Tile t in tiles)
            {
                if (t != selected && t.Selected)
                {
                    t.Deselect();
                }
            }

        }
    }

    public void DestroyTrigger(int index)
    {
        Destroy(Triggers[index]);
    }
}