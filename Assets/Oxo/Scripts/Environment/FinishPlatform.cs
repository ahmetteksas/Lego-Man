using DG.Tweening;
using ScriptableObjectArchitecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityStandardAssets.Cameras;

public class FinishPlatform : MonoBehaviour
{
    [SerializeField]
    GameEvent complateGame = default(GameEvent);

    public bool isLinearEnding;
    public List<GameObject> stickmanList;
    public int finalScore;

    private void Start()
    {
        LevelManager.instance.finishPlatform = gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(FinalAction());
        }
    }

    public IEnumerator FinalAction()
    {
        yield return new WaitForSeconds(0f);
        foreach (GameObject stickman in stickmanList)
        {
            Camera.main.transform.root.GetComponent<AutoCam>().SetTarget(stickman.transform);
            yield return stickman.transform.DOMoveZ(stickman.transform.position.z + 2f, 0.2f).WaitForCompletion();
        }
        complateGame.Raise();
        Debug.Log("LevelComplate");
        //GameManager.instance.LevelComplete();
    }
}