using Dreamteck.Splines;
using ScriptableObjectArchitecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Player : MonoBehaviour
{
    int id;

    //[SerializeField]
    //PlayerSettings settings;

    public Inventory inventory;
    Rigidbody rigidbody;
    Animator animator;

    private void Awake()
    {
        //if (settings == null)
        //    Debug.LogError("You need a settings for use this.");
        id = GetInstanceID();

        rigidbody = GetComponentInChildren<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        //transform.position = Vector3.zero;
    }

    public void StageChange(string _stage)
    {
        Debug.Log(_stage);
        if (_stage != "")
        {
            StartCoroutine(velocityFixer());
            //rigidbody.isKinematic = true;
            if (animator)
                animator.SetTrigger(_stage);
        }
    }

    public void StartGame()
    {
        rigidbody.isKinematic = false;

        if (animator)
            animator.SetFloat("yDir", 1f);
    }

    IEnumerator velocityFixer()
    {
        rigidbody.drag = 40f;
        while (rigidbody.drag > 4)
        {
            rigidbody.drag = Mathf.Lerp(rigidbody.drag, 0f, .02f);
            yield return new WaitForFixedUpdate();
        }
        rigidbody.drag = 0f;
    }
}
