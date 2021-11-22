using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorMovement : MonoBehaviour
{
    private float dragOrigin;
    private float leftThreshold = -2.5f;
    private float rightThreshold = 2.5f;
    //public bool isLeftHand;
    private Rigidbody rb;
    public float movement;

    public GameObject leftPlayer;

    bool isStunned;

    public float stunDelay;
    public float minCloseOffset;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        //if (isLeftHand)
        //    HandleLeftMovement();
        //else
        HandleRightMovement();

    }
    private void FixedUpdate()
    {
        //rb.AddForce(Vector3.forward * movement);
        //rb.AddForce(Vector3.forward * movement);

        rb.velocity = Vector3.forward * movement;

        leftPlayer.transform.position = new Vector3(-transform.position.x, transform.position.y, transform.position.z);
    }
    private void HandleRightMovement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition.x;
        }

        if (Input.GetMouseButton(0))
        {
            float moveAmount = (Input.mousePosition.x - dragOrigin);


            if (isStunned)
            {
                if (transform.localPosition.x < minCloseOffset)
                {
                    transform.localPosition = new Vector3(minCloseOffset, transform.localPosition.y, transform.localPosition.z);
                }

                if (moveAmount < 0 && transform.localPosition.x <= minCloseOffset)
                {
                    Debug.Log("move amount" + moveAmount + " lpX : " + transform.localPosition.x);
                    return;
                }
            }

            transform.localPosition = new Vector3(transform.localPosition.x + moveAmount / 110, transform.localPosition.y, transform.localPosition.z);


            if (transform.localPosition.x >= rightThreshold)
            {
                transform.localPosition = new Vector3(rightThreshold, transform.localPosition.y, transform.localPosition.z);
            }
            else if (transform.localPosition.x <= .28f)
            {
                if (isStunned)
                {
                    transform.localPosition = new Vector3(.3f, transform.localPosition.y, transform.localPosition.z);
                }
                else
                {
                    transform.localPosition = new Vector3(.28f, transform.localPosition.y, transform.localPosition.z);
                }
            }

            dragOrigin = Input.mousePosition.x;
        }
    }

    public void PlayerStunned()
    {
        isStunned = true;
        transform.localPosition = new Vector3(.31f, transform.localPosition.y, transform.localPosition.z);
        StartCoroutine(SolveStun());
    }

    IEnumerator SolveStun()
    {
        yield return new WaitForSeconds(stunDelay);
        isStunned = false;
    }
}
