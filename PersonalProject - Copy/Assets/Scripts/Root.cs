using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{
    //Sets the position of a root between 2 points
    public void SetPosition(Vector3 start, Vector3 end)
    {
        var dir = end - start;
        var mid = (dir) / 2.0f + start;
        transform.position = mid;
    }
    
    //turns the root the right way between 2 points
    public void SetRotation(Point start, Point end)
    {
        Vector2 rotation = new Vector2();
        print("Start: " + start.X + ", " + start.Y);
        print("End: " + end.X + ", " + end.Y);
        if (end.X != start.X)
        {
            if (end.X < start.X)
            {
                rotation = new Vector2(-90, -90); //Left
            }
            else //if (end.x > start.x)
            {
                rotation = new Vector2(-90, 90); //Right
            }
        }
        else //if(end.y != start.y )
        {
            if (end.Y < start.Y)
            {
                rotation = new Vector2(-90, 180); //Up
            }
            else //if (end.y > start.y)
            {
                rotation = new Vector2(-90, 0); //Down
            }
        }

        transform.Rotate(rotation);
    }

    public IEnumerator _turnAround()
    {
        while (transform.rotation.z > -180)
        {
            transform.Rotate(Vector3.back * Time.deltaTime);
            yield return null;
        }
    }

    public void DestroyRoot()
    {
        Destroy(this.gameObject);
    }
}
