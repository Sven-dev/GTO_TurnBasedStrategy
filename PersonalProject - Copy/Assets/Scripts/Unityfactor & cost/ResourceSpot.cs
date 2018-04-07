using UnityEngine;

public enum ResourceType
{
    Water,
    co2,
    Solar
}

/// <summary>
/// Because of the unitfactory patern, and the fact things get spawned during runtime, collecters don't know what resource to attach to without these resource spots
/// </summary>
public class ResourceSpot : MonoBehaviour
{
    public ResourceType Type;
}