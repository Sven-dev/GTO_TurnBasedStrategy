using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryController : MonoBehaviour {

    public Text VictoryLabel;
    public List<GameObject> UI;

    public void CheckVictory(BaseTree b, Player p)
    {
        if (b.Health >= b.GrowthMax)
        {
            Win(p);
        }
    }

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

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
