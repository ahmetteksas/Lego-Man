using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

public class Magnet : MonoBehaviour
{
    public float magnetForce;

    void Start()
    {

    }


    void OnTriggerStay(Collider other)
    {
        //if (other.CompareTag("Gate"))
        //{
        //    Vector3 force = transform.position - other.transform.position;
        //    force = new Vector3(force.x * 1900f, force.y, force.z * 1900f);
        //    other.GetComponent<Rigidbody>().AddForce(force * magnetForce);
        //}
    }
}
