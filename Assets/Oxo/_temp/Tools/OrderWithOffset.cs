using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderWithOffset : MonoBehaviour
{
    [SerializeField]
    Vector3 offset;

    void Start()
    {
        float i = 0;
        foreach (Transform child in transform)
        {
            i++;
            child.transform.position += offset * i;
        }
    }

    void Update()
    {

    }
}
