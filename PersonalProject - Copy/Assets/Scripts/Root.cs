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
        transform.position = mid + Vector3.down * 0.2f;
    }
    
    //turns the root the right way between 2 points
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

    public void TurnAround()
    {
        StartCoroutine(_turnAround());
    }

    IEnumerator _turnAround()
    {
        Transform mesh = transform.GetChild(0);
        
        while (mesh.eulerAngles.x <= 90 || mesh.eulerAngles.x >= 280)
        {
            mesh.Rotate(Vector3.right * 100 * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator _turnBack()
    {
        Transform mesh = transform.GetChild(0);

        while (mesh.eulerAngles.x <= 90 || mesh.eulerAngles.x >= 280)
        {
            mesh.Rotate(Vector3.left * 100 * Time.deltaTime);
            yield return null;
        }

        Destroy(this.gameObject);
    }

    public void DestroyRoot()
    {
        StartCoroutine(_turnBack());
    }
}