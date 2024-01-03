using System;
using System.Collections;
using System.Collections.Generic;
using OneSignalSDK;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BootStrapper : MonoBehaviour
{
    public int OnBoardSceneIndex;
    public int MainMenuSceneIndex;

    public const string onSignalSdk = "b2643052-375f-41e7-9351-bdc9be9bec82";

    void Start()
    {
        //Init();

        StartSceneLoading();
    }

    private void StartSceneLoading()
    {
        AsyncOperation sceneTask;

        Application.targetFrameRate = 60;

        OneSignal.Initialize(onSignalSdk);
        //SetupAd();

        Debug.Log(PlayerStats.isPurchased);

        sceneTask = SceneManager.LoadSceneAsync(PlayerStats.isPurchased ? MainMenuSceneIndex : OnBoardSceneIndex);
    }
}
