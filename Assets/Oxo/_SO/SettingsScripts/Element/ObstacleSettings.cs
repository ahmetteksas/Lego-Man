using ScriptableObjectArchitecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Obstacle Setting", menuName = "OXO/Elements/Obstacle Setting", order = 0)]
public class ObstacleSettings : ScriptableObject
{
    //public GameEvent onFailEvent = default(GameEvent);

    public bool oneShoot;
    public float damage;
}
