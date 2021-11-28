using ScriptableObjectArchitecture;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    int id;

    [SerializeField]
    ObstacleSettings settings;

    public List<FloatVariable> targetFloatList;
    public List<GameObjectCollection> targetCollectionList;

    [SerializeField]
    IntGameEvent shoot;

    //Collection<GameObject> 

    private void Awake()
    {
        if (!settings)
            Debug.LogError("You need a settings for use this.");

        id = GetInstanceID();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out HealthSensor player))
        {
            foreach (GameObject item in targetCollectionList.FirstOrDefault())
            {
                Collider _collider = item.GetComponent<Collider>();
                _collider.isTrigger = false;
                Rigidbody _rigidbody = item.GetComponent<Rigidbody>();
                _rigidbody.isKinematic = false;
                _rigidbody.AddForce(new Vector3(Random.Range(-3f, 3f), 4f, Random.Range(10f, 50f)));
                shoot.Raise(item.GetInstanceID());
            }
            targetCollectionList.FirstOrDefault().Clear();
            targetFloatList.FirstOrDefault().Value -= settings.damage;

            gameObject.SetActive(false);
        }
    }

    public void FailTheGame()
    {
        //settings.onFailEvent.Raise();
    }
}