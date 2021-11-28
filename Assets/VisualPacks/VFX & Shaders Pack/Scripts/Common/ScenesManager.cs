using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    /// <summary>
    /// Reference for Scene name
    /// </summary>
    public Text sceneName;

    /// <summary>
    /// Back To Main Scene
    /// </summary>
    public static ScenesManager _instance;

    void Awake()
    {

        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        sceneName.text = SceneManager.GetActiveScene().name;
    }


    /// <summary>
    /// Back To Main Scene
    /// </summary>
    public void BackToMainScene()
    {
        sceneNumber = 0;
        LoadSceneByIndex(sceneNumber);
    }

    /// <summary>
    /// Scene Index
    /// </summary>
    public static int sceneNumber;


    /// <summary>
    /// Next scene
    /// </summary>
    public void Next()
    {
        sceneNumber++;
        LoadSceneByIndex(sceneNumber);
        Debug.Log(sceneNumber);
    }

    /// <summary>
    /// Previous scene
    /// </summary>
    public void Prev()
    {
        sceneNumber--;
        LoadSceneByIndex(sceneNumber);
        Debug.Log(sceneNumber);
    }

    /// <summary>
    /// Load scene by index
    /// </summary>
    public void LoadSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

}
