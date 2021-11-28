using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Multiply Settings", menuName = "OXO/Property/Multiply Settings", order = 0)]
public class MultiplySettings : ScriptableObject
{
    public bool isMultiply;
    public float multiplyValue;
    public List<string> targetTagList;
    public List<string> executeTagList;
}