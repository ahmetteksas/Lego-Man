using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class DissolveHandler : MonoBehaviour
{
    /// <summary>
    /// Refference for the Animator
    /// </summary>
    Animator animator;

    /// <summary>
    /// Refference for the time
    /// </summary>
    float time;

    /// <summary>
    /// Refference for the Material
    /// </summary>
    public Material material;
    public VisualEffect VFXGrapgh;

    /// <summary>
    /// Start function
    /// </summary>
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).speed == 4)
        {
            time = Time.time;
            material.SetFloat("DissolveAmount_", 0);
            VFXGrapgh.Play();
        }
        else if(animator.GetCurrentAnimatorStateInfo(0).speed < 4 && animator.GetCurrentAnimatorStateInfo(0).speed > 0)
        {
            material.SetFloat("DissolveAmount_", (Time.time - time) /4);
        }
    }
}
