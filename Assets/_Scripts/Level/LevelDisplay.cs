using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _levelName;
    [SerializeField] private TMP_Text _levelDescription;
    [SerializeField] private TMP_Text _levelIndex;
    [SerializeField] private Button _buttonPlay;
    [SerializeField] private GameObject _imageLock;
    public void Display(Level level)
    {
        _levelName.text = level.LevelName;
        _levelDescription.text = level.LevelDescription;

        _levelIndex.text = level.LevelIndex.ToString();
        bool levelUnlocked = PlayerPrefs.GetInt("currentScene", 1) >= level.LevelIndex;
        
        _imageLock.SetActive(!levelUnlocked);
        _buttonPlay.interactable = levelUnlocked;

        _buttonPlay.onClick.RemoveAllListeners();
        _buttonPlay.onClick.AddListener(() => SceneManager.LoadScene(level.SceneToLoad.name));
    }
}
