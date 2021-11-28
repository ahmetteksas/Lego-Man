using ScriptableObjectArchitecture;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GateMagnet : MonoBehaviour
{
    Rigidbody rigidbody;
    public float movementSpeed;
    public float sensivity = 0.01f;

    public Inventory inventory;

    #region MobileInputFunctions

    Vector2 firstMousePosition;
    Vector2 lastMousePosition;
    Vector2 deltaMousePosition;
    Vector2 movementVector;

    private void Break()
    {
        //rigidbody.velocity *= .00007f;
    }

    void FingerDown()
    {
        firstMousePosition = Input.mousePosition;
    }

    void FingerDrag()
    {
        lastMousePosition = Input.mousePosition;
        deltaMousePosition = lastMousePosition - firstMousePosition;
        movementVector = deltaMousePosition * sensivity;

        movementVector = movementVector.normalized;

        float axisSpeedNormalize = deltaMousePosition.magnitude / 250f;

        if (axisSpeedNormalize > 1f)
            axisSpeedNormalize = 1f;

        rigidbody.velocity = transform.forward * movementSpeed * axisSpeedNormalize;

        Vector3 movement = Quaternion.Euler(0,
            Camera.main.transform.eulerAngles.y, 0) * new Vector3(movementVector.x * sensivity * Time.deltaTime, 0, movementVector.y * sensivity * Time.deltaTime);
        transform.LookAt(transform.position + movement);

    }

    #endregion

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //if (!GameManager.instance.isGameRunning)
        //    return;

        #region MobileInputCheck
        if (Input.GetMouseButtonDown(0)) FingerDown();
        if (Input.GetMouseButton(0)) FingerDrag();
        else Break();
        //if (Input.GetMouseButtonUp(0)) FinerUp();
        #endregion
    }

    void OnGameStart()
    {

    }

    //private void OnApplicationQuit()
    //{
    //    inventory.visuals.FirstOrDefault().Clear();
    //}
}
