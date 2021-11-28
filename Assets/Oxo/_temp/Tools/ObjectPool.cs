using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    public List<Pool> pools = new List<Pool>();
    public Dictionary<string, Queue<GameObject>> poolDictionary = new Dictionary<string, Queue<GameObject>>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject go = Instantiate(pool.prefab);
                go.SetActive(false);
                objectPool.Enqueue(go);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }
    public void SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            print(tag + " tag doesn't exist");
            return;
        }

        GameObject go = poolDictionary[tag].Dequeue();
        go.transform.position = position;
        go.transform.rotation = rotation;

        go.SetActive(true);

        poolDictionary[tag].Enqueue(go);
    }

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }
}
