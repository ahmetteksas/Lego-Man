using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Collectable Setting", menuName = "OXO/Elements/Collectable Setting", order = 0)]
public class CollectableSettings : ScriptableObject
{
    public bool isVisual;

    public GameObject pickupEffect;

    public float increaseCount;
}
