using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Joystick : MonoBehaviour
{
    public VariableJoystick variableJoystick;

    private bool isTouch;
    public float speed;
    private Rigidbody rigidbody;

    #region Settings Variables

    public JoystickSettings settings;
    
    bool isSensitivityStabilize;
    bool isRootControl;
    private bool isPhysic;
    private float stopDetectDistance = 1; // it just Control
    private float sensitivity = 0.008f;
    private float rotationSensitivity = 7; // zero mean no rotation control
    private float maxSwerveRotation = 30;
    private float rotationValue = 0.2f; // zero mean no rotation control

    private LayerMask detectMask;

    [Header("Character")] private float movementSpeed = 5;
    private float topSpeed = 7;
    private int stabilizeIteration = 10; // high value work like lerp

    #endregion

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();

        if (settings != null)
        {
            isRootControl = settings.isRootControl;
            movementSpeed = settings.movementSpeed;
            topSpeed = settings.topSpeed;
            rotationSensitivity = settings.rotationSensitivity;
            detectMask = settings.detectMask;
            speed = settings.movementSpeed;
        }
    }

    private void Update()
    {
        #region MobileInputCheck

        if (Input.GetMouseButtonDown(0)) isTouch = true;
        if (Input.GetMouseButtonUp(0)) isTouch = false;

        #endregion
    }

    public void FixedUpdate()
    {
        if (isTouch)
        {
            Vector3 direction = Vector3.forward * variableJoystick.Vertical +
                                Vector3.right * variableJoystick.Horizontal;
            rigidbody.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);

            if (Mathf.Abs(
                    Mathf.Atan2(variableJoystick.Horizontal, variableJoystick.Vertical) * 180 / Mathf.PI) -
                transform.eulerAngles.y > 90)
                rigidbody.velocity = rigidbody.velocity = Vector3.Lerp(rigidbody.velocity, Vector3.zero, 0.05f);

            transform.DORotate(new Vector3(0,
                    Mathf.Atan2(variableJoystick.Horizontal, variableJoystick.Vertical) * 180 / Mathf.PI, 0),
                1 / rotationSensitivity);

            // transform.eulerAngles = new Vector3(0,
            //     Mathf.Atan2(variableJoystick.Horizontal, variableJoystick.Vertical) * 180 / Mathf.PI, 0);
        }
        else
        {
            rigidbody.velocity = Vector3.Lerp(rigidbody.velocity, Vector3.zero, 0.05f);
        }
    }

}