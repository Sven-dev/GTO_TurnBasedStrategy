using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{
    public int test;
    // Use this for initialization
    void Awake()
    {
        test = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetPosition(Vector3 start, Vector3 end)
    {
        var dir = end - start;
        var mid = (dir) / 2.0f + start;
        transform.position = mid;
    }
    
    public void SetRotation(Vector3 start, Vector3 end)
    {
        Vector2 rotation = new Vector2();

        if (end.x != start.x)
        {
            if (end.x < start.x)
            {
                rotation = new Vector2(0, 0); //Left
            }
            else //if (end.x > start.x)
            {
                rotation = new Vector2(0, 180); //Right
            }
        }
        else //if(end.y != start.y )
        {
            if (end.y < start.y)
            {
                rotation = new Vector2(0, 270); //Up
            }
            else //if (end.y > start.y)
            {
                rotation = new Vector2(0, 90); //Down
            }
        }

        transform.Rotate(rotation);
    }
}
