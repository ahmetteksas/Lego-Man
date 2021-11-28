using ScriptableObjectArchitecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitable : MonoBehaviour
{
    [SerializeField]
    HealthSensorSettings settings;
    [SerializeField]
    StringGameEvent playerStageChange;


    public void Hit()
    {
        playerStageChange.Raise();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out Obstacle obstacle))
        {

        }
        //foreach (var componentName in settings.collectableComponentNames)
        //{
        //    Type type = Type.GetType(componentName);

        //    if (other.TryGetComponent(type, out Component _collectable))
        //        Collet(_collectable.gameObject, type);
        //}
    }
}
