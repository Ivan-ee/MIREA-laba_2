using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private Level[] _levels;
    [SerializeField] private LevelDisplay _levelDisplay;

    private LevelData _level;
    private int _currentIndex;

    private void Awake()
    {
        ChangeLevel(0);
    }
    public void ChangeLevel(int change)
    {
        _currentIndex += change;
        
        if (_currentIndex < 0) _currentIndex = _levels.Length - 1;
        else if (_currentIndex > _levels.Length - 1) _currentIndex = 0;
        
        ChangeLevelIndex();

        if (_levelDisplay != null)
        {
            _levelDisplay.Display(_levels[_currentIndex]);
        }
    }
    void ChangeLevelIndex()
    {
        _level = _levels[_currentIndex].LevelData;
    }
}
