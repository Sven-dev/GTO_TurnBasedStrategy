using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public List<Player> PlayerList;
    public int CurrentPlayer;

    // Use this for initialization
    void Start ()
    {
        SpawnStartingStructures();
        CurrentPlayer = 0;
        NextTurn();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void SpawnStartingStructures()
    {
        foreach (Player p in PlayerList)
        {
            p.StartGame();
        }
    }

    public void BuyThing()
    {
        PlayerList[CurrentPlayer].BuyThing(0);
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
        PlayerList[CurrentPlayer].SelectedTile = t;
        print("Tile selected");
    }
}
