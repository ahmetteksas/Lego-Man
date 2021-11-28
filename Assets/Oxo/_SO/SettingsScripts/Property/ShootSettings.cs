using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Shoot Setting", menuName = "OXO/Property/Shoot Setting", order = 0)]
public class ShootSettings : ScriptableObject
{
    //public List<string> releatedComponentNameList;
    //public string followComponentNameList;
    public bool isAutoShoot;
    public string bulletPoolTag;
    public bool visual;
    public float firstShootWaitDelay;
    public float shootDelay;
}
