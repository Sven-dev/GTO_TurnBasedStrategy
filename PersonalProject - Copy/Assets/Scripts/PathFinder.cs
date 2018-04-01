using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    Grid Grid;

    private void Start()
    {
        Grid = GetComponent<Grid>();
    }

    public List<Corner> FindPath(Corner start, Corner end, Player p)
    {
        List<Corner> OpenSet = new List<Corner>();
        List<Corner> ClosedSet = new List<Corner>();
        OpenSet.Add(start);

        while(OpenSet.Count > 0)
        {
            Corner current = OpenSet[0];

            for (int i = 1; i < OpenSet.Count; i++)
            {
                if (OpenSet[i].FCost < current.FCost || OpenSet[i].FCost == current.FCost && OpenSet[i].HCost < current.HCost)
                {
                    current = OpenSet[i];
                }
            }

            OpenSet.Remove(current);
            ClosedSet.Add(current);

            if (current == end)
            {
                return RetracePath(start, end);
            }

            foreach(Corner c in Grid.GetCornerNeighbours(current))
            {
                //!c.Ownership.Contains(p) || 
                if(ClosedSet.Contains(c))
                {
                    continue;
                }

                //Calculate Cost
                int guessedDistance = GuessDistance(current, c);
                if (guessedDistance < c.GCost || !OpenSet.Contains(c))
                {
                    print("Guessed distance: " + guessedDistance);
                    c.GCost = guessedDistance;
                    c.HCost = GuessDistance(c, end);
                    c.Parent = current;

                    OpenSet.Add(c);
                }
            }
        }

        Debug.LogError("This shouldn't happen.");
        return null;
    }

    public int GuessDistance(Corner A, Corner B)
    {
        int distX = Mathf.Abs(A.Position.X - B.Position.X);
        int distY = Mathf.Abs(A.Position.Y - B.Position.Y);

        if (distX > distY)
        {
            return 14 * distY + 10 * (distX - distY);
        }

        return 14 * distX + 10 * (distY - distX);
    }

    public int GuessDistance(Tile A, Tile B)
    {
        int distX = Mathf.Abs(A.Position.X - B.Position.X);
        int distY = Mathf.Abs(A.Position.Y - B.Position.Y);

        if (distX > distY)
        {
            return 14 * distY + 10 * (distX - distY);
        }

        return 14 * distX + 10 * (distY - distX);
    }

    List<Corner> RetracePath(Corner start, Corner end)
    {
        List<Corner> path = new List<Corner>();

        Corner currentCorner = end;

        while (currentCorner != start)
        {
            path.Add(currentCorner);
            currentCorner = currentCorner.Parent;
        }

        path.Add(start);

        path.Reverse();
        return path;
    }
}
