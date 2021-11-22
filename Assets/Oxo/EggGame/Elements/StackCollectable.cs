using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;
using System.Linq;
using MoreMountains.NiceVibrations;

public class StackCollectable : MonoBehaviour
{
    [SerializeField]
    GameEvent stunEvent = default(GameEvent);

    [SerializeField]
    bool isCollected;

    public List<GameObjectCollection> releatedColelctionList;

    public Renderer myRenderer;
    public Renderer myRenderer2;
    public Renderer myRenderer3;
    public Renderer myRenderer4;

    public GameObject crackedEgg;

    FollowWithLerp fwl;

    public FloatReference relatedFloatRight;
    public FloatReference relatedFloatLeft;

    public static bool crashedObstacle;

    bool isStunned;

    private void Awake()
    {
        fwl = GetComponent<FollowWithLerp>();

    }

    private void OnEnable()
    {
        if (isCollected)
        {
            if (transform.position.x < 0)
                releatedColelctionList.FirstOrDefault().Add(gameObject);
            else
                releatedColelctionList.LastOrDefault().Add(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!isCollected)
            Collect(other.gameObject);


        if (other.gameObject.tag == "Obstacle"/*&&isCollected*/)
        {
            //StartCoroutine(CrashedTrue());
            crashedObstacle = true;

            Lost();
            
            Destroy(other.gameObject);
            
        }
        //if (other.gameObject.tag == null)
        //{
        //    crashedObstacle = false;
        //}
        //else
        //{
        //    crashedObstacle = false;
        //}

        if (other.collider.CompareTag("Collectable"))
        {

            bool done1 = false;
            bool done2 = false;

            foreach (var item in releatedColelctionList)
            {
                if (item.FirstOrDefault() == gameObject)
                {
                    done1 = true;
                }
                if (item.FirstOrDefault() == other.gameObject)
                {
                    done2 = true;
                }

                if (done1 && done2)
                {
                    Debug.Log("hitted");
                    stunEvent.Raise();
                    Lost();
                    other.collider.GetComponent<StackCollectable>().Lost();
                }
            }
            //if (releatedColelctionList.FirstOrDefault() == gameObject)
            //{
            //    if (other.collider.GetComponent<StackCollectable>().releatedColelctionList.LastOrDefault() == other.gameObject)
            //    {
            //        Debug.Log("hitted");
            //        Lost();
            //        other.collider.GetComponent<StackCollectable>().Lost();
            //    }
            //}
        }
    }

    private void Update()
    {
        if (/*myRenderer == null && myRenderer2 == null && myRenderer3 == null && myRenderer4 == null &&*/ isCollected)
        {
            myRenderer.enabled = true;
            myRenderer2.enabled = true;
            myRenderer3.enabled = true;
            myRenderer4.enabled = true;
        }
    }

    void Collect(GameObject other)
    {
        if (other.CompareTag("PlayerLeft"))
        {
            isCollected = true;

            #region SelectFollowTarget
            if (releatedColelctionList.Count > 0)
                fwl.target = releatedColelctionList.FirstOrDefault().LastOrDefault().transform;
            else
                fwl.target = other.transform;
            #endregion

            releatedColelctionList.FirstOrDefault().Add(gameObject);
            relatedFloatLeft.Value++;
            MMVibrationManager.Haptic(HapticTypes.Success);
        }
        if (other.CompareTag("PlayerRight"))
        {
            isCollected = true;

            #region SelectFollowTarget
            if (releatedColelctionList.Count > 0)
                fwl.target = releatedColelctionList.LastOrDefault().LastOrDefault().transform;
            else
                fwl.target = other.transform;
            #endregion

            releatedColelctionList.LastOrDefault().Add(gameObject);

            relatedFloatRight.Value++;
            MMVibrationManager.Haptic(HapticTypes.Success);
        }
        if (other.CompareTag("Player"))
        {
            isCollected = true;

            #region SelectFollowTarget
            if (releatedColelctionList.Count > 0)
                fwl.target = releatedColelctionList.FirstOrDefault().LastOrDefault().transform;
            else
                fwl.target = other.transform;
            #endregion

            releatedColelctionList.FirstOrDefault().Add(gameObject);

            //relatedFloat.Value++;
            MMVibrationManager.Haptic(HapticTypes.Success);
        }
    }

    private void OnDisable()
    {
        if (releatedColelctionList.FirstOrDefault().Contains(gameObject))
        {
            releatedColelctionList.FirstOrDefault().Remove(gameObject);
            relatedFloatLeft.Value--;
        }
        else
        {
            releatedColelctionList.LastOrDefault().Remove(gameObject);
            relatedFloatRight.Value--;
        }

        //foreach (var reletedCollection in releatedColelctionList)
        //    if (reletedCollection.Contains(gameObject))
        //        reletedCollection.Remove(gameObject);

    }

    public void Lost()
    {
        crackedEgg.transform.SetParent(null);
        crackedEgg.SetActive(true);

        gameObject.SetActive(false);
    }
    IEnumerator CrashedTrue()
    {
        yield return new WaitForSeconds(1.5f);
        crashedObstacle = false;
    }
}
