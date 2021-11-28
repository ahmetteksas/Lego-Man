using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Scale Settings", menuName = "OXO/Property/Scale Settings", order = 0)]
public class ScaleSettings : ScriptableObject
{
    public bool isMultiply;
    public float scaleValue;
    public List<string> targetTagList;
    public List<string> executeTagList;
}
