using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{
    /// <summary>
    ///Sets the position of a root between 2 points
    /// </summary>
    public void SetPosition(Vector3 start, Vector3 end)
    {
        var dir = end - start;
        var mid = (dir) / 2.0f + start;
        transform.position = mid + Vector3.down * 0.2f;
    }
    
    /// <summary>
    /// turns the root the right way between 2 points
    /// </summary>
    /// <param name="start">Point 1</param>
    /// <param name="end">Point 2</param>
    public void SetRotation(Point start, Point end)
    {
        Vector2 rotation = new Vector2();
        if (end.X != start.X)
        {
            if (end.X < start.X)
            {
                rotation = new Vector3(90, -90); //Left
            }
            else //if (end.x > start.x)
            {
                rotation = new Vector3(90, 90); //Right
            }
        }
        else //if(end.y != start.y )
        {
            if (end.Y < start.Y)
            {
                rotation = new Vector3(90, 180); //Up
            }
            else //if (end.y > start.y)
            {
                rotation = new Vector3(90, 0); //Down
            }
        }

        transform.Rotate(rotation);
    }

    /// <summary>
    /// Starts _turnAround
    /// </summary>
    public void TurnAround()
    {
        StartCoroutine(_turnAround());
    }

    /// <summary>
    /// Rotates the root 180*, which looks like the root grows
    /// </summary>
    /// <returns></returns>
    IEnumerator _turnAround()
    {
        Transform mesh = transform.GetChild(0);
        
        while (mesh.eulerAngles.x <= 90 || mesh.eulerAngles.x >= 280)
        {
            mesh.Rotate(Vector3.right * 100 * Time.deltaTime);
            yield return null;
        }
    }

    public void DestroyRoot()
    {
        Destroy(gameObject);
    }
}