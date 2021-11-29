using ScriptableObjectArchitecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    int id;

    public CollectableSettings settings;

    bool isCollected;

    [SerializeField]
    GameObject _owner;
    int owner;

    private void Awake()
    {
        if (settings == null)
            Debug.LogError("You need a settings for use this.");

        id = gameObject.GetInstanceID();
    }

    void Start()
    {
        if (_owner)
            owner = _owner.GetInstanceID();
    }

    public void Collect(int _id)
    {
        if (id != _id)
            return;

        if (!settings.isVisual)
        {
            if (settings.pickupEffect)
                Instantiate(settings.pickupEffect, transform.position, transform.rotation);

            Destroy(gameObject);
        }
        else
        {
            Obstacle obs = GetComponentInChildren<Obstacle>();

            if (obs.TryGetComponent(out BoxCollider collider))
            {
                collider.isTrigger = true;
            }
            //Destroy(GetComponentInChildren<Obstacle>());
            //if (collectable.TryGetComponent(out CanFollow canFollow))
            //{
            //    if (collectableCollection.Count == 0)
            //        canFollow.SetTarget(transform);
            //    else
            //        canFollow.target = collectableCollection.LastOrDefault().transform;
            //}
        }

        enabled = false;
    }

    void Lost()
    {

    }

}