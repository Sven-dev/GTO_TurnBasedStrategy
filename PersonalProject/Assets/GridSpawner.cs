using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpawner : MonoBehaviour {

    public GameObject Prefab;
    public int Length;
    public int Width;
    public GameObject[,] TileArray;

    // Use this for initialization
    void Start()
    {
        TileArray = new GameObject[Length, Width];
        for (int i = 0; i < Length; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                TileArray[i, j] = (GameObject)Instantiate(Prefab, new Vector3(i, 0, j), Quaternion.identity);
            }
        }
    }
}
