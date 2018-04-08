using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RangeShower : MonoBehaviour {

    public PlayerManager PlayerManager;
    public Grid Grid;
    public List<EventTrigger> Triggers;

    /// <summary>
    /// Displays the range of a Structure
    /// </summary>
    /// <param name="s">The structure given</param>
    public void ShowRange(Structure s)
    {
        Player player = PlayerManager.GetCurrentPlayer();
        Tile selected = player.SelectedTile;
        if (selected != null)
        {
            if (selected.Owner == null || selected.Owner == player)
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
    }

    /// <summary>
    /// Returns a list of tiles that are in range of s
    /// </summary>
    /// <param name="s">The given structure</param>
    /// <param name="selected">The selected tile</param>
    /// <param name="player">The player the structure belongs to</param>
    /// <returns></returns>
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

    /// <summary>
    /// Hides the range of Structure
    /// </summary>
    /// <param name="s">The given Structure</param>
    public void HideRange(Structure s)
    {
        Player player = PlayerManager.GetCurrentPlayer();
        Tile selected = player.SelectedTile;
        if (selected != null)
        {
            if (selected.Owner == null || selected.Owner == player)
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
    }

    /// <summary>
    /// Destroys the range shower of the start tree, after the first turn
    /// </summary>
    /// <param name="index">The index of the player</param>
    public void DestroyTrigger(int index)
    {
        Destroy(Triggers[index]);
    }
}