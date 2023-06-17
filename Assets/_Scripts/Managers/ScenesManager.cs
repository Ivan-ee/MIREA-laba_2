using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    private void Awake()
    {
        PlayerController.OnPlayerDeadEvent.AddListener(LoadCurrentScene);
        GameManager.OnFinishLevelEvent.AddListener(FinishLevel);
    }
    void LoadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void FinishLevel()
    {
        PlayerPrefs.SetInt("currentScene", SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene(0);
    }
}
