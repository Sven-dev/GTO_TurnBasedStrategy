using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType
{
    Water,
    co2,
    Solar
}

public class ResourceSpot : MonoBehaviour
{
    public ResourceType Type;
}