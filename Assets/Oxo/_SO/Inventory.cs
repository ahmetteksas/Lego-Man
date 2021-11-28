using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

[CreateAssetMenu]
public class Inventory : ScriptableObject
{
    public List<GameObjectCollection> visuals;

    public FloatReference health;
    public FloatReference score;
    public FloatReference coin;
    public FloatReference token;
    public FloatReference bullet;
}
