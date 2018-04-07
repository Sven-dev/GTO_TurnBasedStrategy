[System.Serializable]
public class Cost
{
    public Resource resource;
    public int cost;

    public Cost(Resource r, int c)
    {
        resource = r;
        cost = c;
    }
}
