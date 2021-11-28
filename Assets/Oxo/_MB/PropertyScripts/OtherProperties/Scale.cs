using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : MonoBehaviour
{
    [SerializeField]
    ScaleSettings settings;

    List<GameObject> scaleObjects = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (settings.executeTagList.Contains(other.tag))
            Execute();

        if (settings.targetTagList.Contains(other.tag))
            scaleObjects.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (settings.targetTagList.Contains(other.tag))
            scaleObjects.Remove(other.gameObject);
    }

    void Execute()
    {

        if (settings.isMultiply)
        {
            foreach (GameObject targetObject in scaleObjects)
            {
                targetObject.transform.localScale *= settings.scaleValue;
            }
        }
        else
        {
            foreach (GameObject targetObject in scaleObjects)
            {
                targetObject.transform.localScale += Vector3.one * settings.scaleValue;
            }
        }
    }
}
