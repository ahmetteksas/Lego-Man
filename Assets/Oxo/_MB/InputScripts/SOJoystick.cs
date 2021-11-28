using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOJoystick : MonoBehaviour
{
    Rigidbody rigidbody;
    Vector2 firstMousePosition;
    Vector2 lastMousePosition;
    Vector2 deltaMousePosition;
    Vector2 movementVector;


    public float movementSpeed;
    public float sensivity = 0.01f;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            firstMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            lastMousePosition = Input.mousePosition;

            deltaMousePosition = lastMousePosition - firstMousePosition;

            movementVector = deltaMousePosition * sensivity;
            movementVector = movementVector.normalized;
            rigidbody.velocity = transform.forward * movementSpeed;
            Vector3 movement = Quaternion.Euler(0,
                Camera.main.transform.eulerAngles.y, 0) * new Vector3(movementVector.x * sensivity * Time.deltaTime, 0, movementVector.y * sensivity * Time.deltaTime);
            transform.LookAt(transform.position + movement);
        }
    }
}
