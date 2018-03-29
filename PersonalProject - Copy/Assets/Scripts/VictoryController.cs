using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryController : MonoBehaviour {

    public List<GameObject> UI;

    public void CheckVictory(BaseTree b)
    {
        if (b.Growthcurrent >= b.GrowthMax)
        {
            foreach(GameObject g in UI)
            {
                g.SetActive(false);
            }

            this.gameObject.SetActive(true);
        }
    }
}
