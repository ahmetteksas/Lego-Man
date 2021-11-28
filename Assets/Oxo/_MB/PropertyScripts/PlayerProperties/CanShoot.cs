using ScriptableObjectArchitecture;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CanShoot : MonoBehaviour
{
    public ShootSettings settings;

    public IntGameEvent shoot;

    public Inventory inventory;

    List<GameObjectCollection> bulletCollections = new List<GameObjectCollection>();

    Coroutine autoShoot;

    private void Awake()
    {
        if (settings == null)
            Debug.LogError("You need a settings for use this.");

    }

    private void OnEnable()
    {
        if (settings.isAutoShoot)
            autoShoot = StartCoroutine(AutoShoot());
    }

    private void Update()
    {
        if (!settings.isAutoShoot)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (autoShoot != null)
                    StopCoroutine(autoShoot);
            }

            if (Input.GetMouseButtonUp(0))
            {
                autoShoot = StartCoroutine(AutoShoot());
            }
        }
    }

    IEnumerator AutoShoot()
    {
        yield return new WaitForSeconds(settings.firstShootWaitDelay);

        if (!settings.visual)
        {
            if (inventory.bullet.Value > 0) //Send NonVisual Bulletss
            {
                while (inventory.bullet.Value > 0)
                {
                    ObjectPool.instance.SpawnFromPool(settings.bulletPoolTag, transform.position, Quaternion.identity);
                    inventory.bullet.Value--;
                    yield return new WaitForSeconds(settings.shootDelay);
                }
            }
        }

        if (settings.visual) //Send Visual Bullets
        {
            GetBulletCollections();

            while (bulletCollections.Count > 0)
            {
                GameObjectCollection currentCollection = bulletCollections.FirstOrDefault();

                while (currentCollection.Count > 0)
                {
                    GameObject bullet = currentCollection.LastOrDefault();

                    shoot.Raise(bullet.GetInstanceID());

                    currentCollection.Remove(bullet);
                    yield return new WaitForSeconds(settings.shootDelay);
                }

                //bulletCollections.Remove(currentCollection);
                GetBulletCollections();
            }
        }
    }

    void GetBulletCollections()
    {
        bulletCollections.Clear();

        foreach (var visualCollection in inventory.visuals)
        {
            if (visualCollection.Count > 0)
                if (visualCollection.FirstOrDefault().TryGetComponent(out Shootable bullet))
                    bulletCollections.Add(visualCollection);
        }
    }

    private void OnDisable()
    {
        if (autoShoot != null)
            StopCoroutine(autoShoot);
    }
}

