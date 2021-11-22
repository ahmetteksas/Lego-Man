using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWithLerp : MonoBehaviour
{
    public FollowSettings settings;
    public Transform target;
    public Color myColor;
    public MeshRenderer myMesh;
    private GameObject right;
    private GameObject left;
    private float t = 0;
    private float lerpTime = 1f;
    private float k = 0;
    private bool colored;

    private void Awake()
    {
        if (settings == null)
            Debug.LogError("You need a settings for use this.");
        left = GameObject.FindGameObjectWithTag("PlayerLeft");
        right = GameObject.FindGameObjectWithTag("PlayerRight");

    }

    void Start()
    {
        //if (target != null)
        //    settings.offset = target.position - transform.position;
        //startTime = Time.time;
    }
    private void Update()
    {
        //return;
        //right.transform.position = new Vector3(right.transform.position.x, right.transform.position.y, left.transform.position.z);
        if (left.transform.position.x - right.transform.position.x > -1.1f && left.transform.position.x - right.transform.position.x < 0f)
        {

            t = (Mathf.Lerp(t, 1f, lerpTime * Time.deltaTime));
            myMesh.material.color = Color.Lerp(myColor, Color.red, t);

        }
        if (left.transform.position.x - right.transform.position.x <= -1.1f && left.transform.position.x - right.transform.position.x > -4.6f)
        {
            colored = true;

            k = (Mathf.Lerp(k, 1f, lerpTime * Time.deltaTime));
            myMesh.material.color = Color.Lerp(Color.red, myColor, k);

        }
        if (t > .9f && colored)
        {
            t = 0f;
        }
        if (k > .9f && !colored)
        {
            k = 0f;
        }
    }

    public void SetTarget(GameObject _target)
    {
        if (_target != null)
            target = _target.transform;
    }

    void FixedUpdate()
    {
        if (target) // XZ Plane follow
        {
            Vector3 targetPosition = new Vector3(target.position.x + settings.offset.x, transform.position.y, target.position.z + settings.offset.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, settings.followLerp);
        }
    }
}
