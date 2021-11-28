using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Shootable : MonoBehaviour
{
    public ShootableSettings settings;

    public float durability;

    NavMeshAgent navMesh; // use this like enum in settings

    List<GameObject> targetList = new List<GameObject>();

    private void Awake()
    {
        //if (settings == null)
        //    Debug.LogError("You need a settings for use this.");

        //if (TryGetComponent(out NavMeshAgent navMesh))
        navMesh = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        if (settings.isAutoTarget)
            StartCoroutine(UpdateTargetList());
    }

    private void OnCollisionEnter(Collision collision) //Non trigger objects
    {
        if (targetList.Contains(collision.gameObject))
        {
            TakeDamage(collision.transform.GetComponent<Shootable>().settings.damage);
        }
    }

    void TakeDamage(float damage)
    {
        durability -= damage;

        if (durability <= 0)
        {
            //Debug.Log(name + "dead");
            ObjectPool.instance.SpawnFromPool(tag + "Blood", transform.position + Vector3.up * .5f, transform.rotation);
            gameObject.SetActive(false);
        }
    }

    IEnumerator UpdateTargetList()
    {
        //yield break;
        while (true)
        {
            targetList.Clear();
            foreach (var releatedTag in settings.targetTagList)
            {
                targetList = targetList.Union(GameObject.FindGameObjectsWithTag(releatedTag).ToList()).ToList();
            }

            if (navMesh) // Auto Target
            {
                Transform closestTarget =
                    targetList.OrderBy(x => Vector3.Distance(transform.position, x.transform.position)).
                    FirstOrDefault().transform;

                navMesh.SetDestination(closestTarget.position);
                //string typeString = "NavMeshAgent";
                //Type t = Type.GetType(typeString);
                //gameObject.AddComponent(t);
                //gameObject.TryGetComponent(t, out Component cmp)
                //cmp.//.SetDestination(closestTarget.position);
            }
            yield return new WaitForSeconds(.3f);
        }
    }
}
