using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public List<Player> PlayerList;
    public int CurrentPlayer;

    // Use this for initialization
    void Awake ()
    {
        CurrentPlayer = 0;
        NextTurn();
	}

    /// <summary>
    /// Ends the turn of the current player, and starts the turn of the next player in line
    /// </summary>
    public void NextTurn()
    {
        PlayerList[CurrentPlayer].EndTurn();
        CurrentPlayer++;
        if (CurrentPlayer >= PlayerList.Count)
        {
            CurrentPlayer = 0;
        }

        PlayerList[CurrentPlayer].StartTurn();
    }

    /// <summary>
    /// Returns the player currently playing
    /// </summary>
    /// <returns></returns>
    public Player GetCurrentPlayer()
    {
        return PlayerList[CurrentPlayer];
    }

    /// <summary>
    /// Selects t
    /// </summary>
    /// <param name="t">The tile that needs to be selected</param>
    public void SelectTile(Tile t)
    {
        if (PlayerList[CurrentPlayer].SelectedTile != null)
        {
            PlayerList[CurrentPlayer].DeselectTile();
        }

        PlayerList[CurrentPlayer].SelectTile(t);
    }
}