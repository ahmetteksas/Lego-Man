using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Multiply : MonoBehaviour
{
    [SerializeField]
    MultiplySettings settings;

    List<GameObject> multiplyObjects = new List<GameObject>();

    private void Awake()
    {
        if (settings == null)
            Debug.LogError("You need a settings for use this.");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (settings.executeTagList.Contains(other.tag))
            Execute();

        if (settings.targetTagList.Contains(other.tag))
            multiplyObjects.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (settings.targetTagList.Contains(other.tag))
            multiplyObjects.Remove(other.gameObject);
    }

    void Execute()
    {
        if (settings.isMultiply)
        {
            foreach (GameObject targetObject in multiplyObjects)
            {
                for (int i = 0; i < settings.multiplyValue - 1; i++)
                    ObjectPool.instance.SpawnFromPool(targetObject.tag, targetObject.transform.position, targetObject.transform.rotation);
            }
        }
        else
        {
            for (int i = 0; i < settings.multiplyValue; i++)
            {
                ObjectPool.instance.SpawnFromPool(multiplyObjects.FirstOrDefault().tag, transform.position, transform.rotation);
            }
        }
    }
}
