using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;

public class GameDisplay : MonoBehaviour
{
    public RunnerGame settings;

    private void Awake()
    {
        if (settings == null)
            Debug.LogError("You need a settings for use this.");

        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void StartGame()
    {
        settings.isGameRunning = true;
    }

    public void FinishTheGame()
    {
        settings.isGameRunning = false;
    }
}