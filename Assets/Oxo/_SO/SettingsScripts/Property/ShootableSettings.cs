using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Shootable Settings", menuName = "OXO/Property/Shootable Settings", order = 0)]
public class ShootableSettings : ScriptableObject
{
    public string followType;

    public bool isAutoTarget;
    public float damage;
    public List<string> targetTagList;
}
