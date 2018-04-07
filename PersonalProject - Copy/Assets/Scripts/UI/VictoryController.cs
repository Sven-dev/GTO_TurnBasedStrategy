using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryController : MonoBehaviour {

    public Text VictoryLabel;
    public List<GameObject> UI;

    /// <summary>
    /// Checks if player has completed the game by growing their tree
    /// </summary>
    /// <param name="b">The tree</param>
    /// <param name="p">The player</param>
    public void CheckVictory(BaseTree b, Player p)
    {
        if (b.Health >= b.GrowthMax)
        {
            Win(p);
        }
    }

    /// <summary>
    /// Disables the UI and enables the victory screen
    /// </summary>
    /// <param name="p">The player that won</param>
    public void Win(Player p)
    {
        foreach (GameObject g in UI)
        {
            g.SetActive(false);
        }

        gameObject.SetActive(true);
        VictoryLabel.text = p.name + " has won!";
        VictoryLabel.color = p.PlayerColor;
    }

    /// <summary>
    /// Resets the game
    /// </summary>
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}