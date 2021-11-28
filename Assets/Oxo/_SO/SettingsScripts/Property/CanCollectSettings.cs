using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CanCollect Settings", menuName = "OXO/Property/CanCollect Settings", order = 0)]
public class CanCollectSettings : ScriptableObject
{
    public List<string> collectableComponentNames = new List<string>();
}
