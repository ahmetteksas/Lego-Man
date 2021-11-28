using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanFollow : MonoBehaviour
{
    int id;

    public FollowSettings settings;
    public Transform target;

    public Vector3 additinalOffset;
    public float additionalLerp;

    private void Awake()
    {
        if (settings == null)
            Debug.LogError("You need a settings for use this." + name);

        id = gameObject.GetInstanceID();
    }

    void Start()
    {
        //if (target != null)
        //    settings.offset = target.position - transform.position;
    }

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    public void StartFollow(Transform _target)
    {
        SetTarget(_target);
    }

    public void StopFollow(int _id)
    {
        if (id != _id)
            return;

        SetTarget(null);

        if (TryGetComponent(out Rigidbody rigidbody))
            rigidbody.isKinematic = false;
    }

    void FixedUpdate()
    {
        if (target) // need 3D
        {
            Vector3 targetPosition = target.position + settings.offset + additinalOffset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, settings.followLerp + additionalLerp);
        }
    }
}