using ScriptableObjectArchitecture;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CanCollect : MonoBehaviour
{
    [SerializeField]
    CanCollectSettings settings;

    public IntGameEvent collect;
    public Inventory inventory;

    public Transform followPositions;
    List<Transform> followPositionList = new List<Transform>();


    private void Awake()
    {
        if (!inventory)
            Debug.LogError("You need a inventory for collect something.");

        //if (followPositions!=null)
        //{

        //}
        foreach (Transform child in followPositions.transform)
            followPositionList.Add(child);
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (var componentName in settings.collectableComponentNames)
        {
            Type type = Type.GetType(componentName);

            if (other.TryGetComponent(type, out Component _collectable))
                Collet(_collectable.gameObject, type);
        }
    }

    MonoBehaviour collectableScript;
    void Collet(GameObject collectable, Type type)
    {
        collectableScript = collectable.GetComponent(type) as MonoBehaviour;

        if (!collectableScript.enabled)
            return;

        GameObjectCollection releatedColelction = inventory.visuals.FirstOrDefault(x => x.Where(x => x.GetComponent(type)).Count() > 0);

        if (!releatedColelction)
            releatedColelction = inventory.visuals.FirstOrDefault(x => x.Count() == 0);

        SetFollowPosition(collectable.gameObject, releatedColelction);

        releatedColelction.Add(collectable);
        collect.Raise(collectable.gameObject.GetInstanceID());
    }

    void SetFollowPosition(GameObject collectable, GameObjectCollection collection)
    {
        if (collectable.TryGetComponent(out CanFollow canFollow))
        {
            if (collection.Count == 0)
                canFollow.SetTarget(followPositions.transform);
            else
                canFollow.target = collection.LastOrDefault().transform;
        }
    }


    private void OnApplicationQuit()
    {
        inventory.visuals.FirstOrDefault().Clear();
    }
}

/*
 
 
    void Collect(Collectable collectable)
    {
        collect.Raise(collectable.gameObject.GetInstanceID());

        // Find default name of collectable
        string collectableName = collectable.name;
        collectableName = collectableName.Replace("(Clone)", string.Empty);

        if (collectable.settings.isVisual) // it need a collections
        {
            // Check have i releated collection
            GameObjectCollection collectableCollection = inventory.visuals.FirstOrDefault();// inventory.visuals.FirstOrDefault(x => x.Where(x => x.name.Contains(collectableName)).Count() > 0);

            if (collectableCollection)
            {
                if (collectable.TryGetComponent(out CanFollow canFollow))
                {
                    if (collectableCollection.Count == 0)
                        canFollow.SetTarget(transform);
                    else
                        canFollow.target = collectableCollection.LastOrDefault().transform;
                }

                collectableCollection.Add(collectable.gameObject);

                //Debug.Log("Pick up inside " + collectableCollection.name);
            }
            else
            {
                GameObjectCollection emptyCollection = inventory.visuals.FirstOrDefault(x => x.Count() == 0);
                if (emptyCollection)
                {
                    emptyCollection.Add(collectable.gameObject);
                    Debug.Log("Pick up inside " + emptyCollection.name);
                }
                else
                {
                    Debug.LogError("There is no slot for collect this item.");
                }
            }
        }
        else // it need a float reference
        {
            //Debug.Log(inventory.nonVisuals[0].GetHashCode());
            Debug.Log("it can be a health score bullet coin ext.");
            //inventory.nonVisuals.Where(x => x.ToString().Contains(collectableName)).FirstOrDefault();
        }
    }
 */