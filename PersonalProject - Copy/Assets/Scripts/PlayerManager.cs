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

    public Player GetCurrentPlayer()
    {
        return PlayerList[CurrentPlayer];
    }

    public void SelectTile(Tile t)
    {
        if (PlayerList[CurrentPlayer].SelectedTile != null)
        {
            PlayerList[CurrentPlayer].DeselectTile();
        }

        PlayerList[CurrentPlayer].SelectTile(t);
        print("Tile selected");
    }
}