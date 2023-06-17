using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Level", menuName = "Level/New Level")]
public class Level : ScriptableObject
{
    [SerializeField] private int _levelIndex;
    [SerializeField] private string _levelName;
    [SerializeField] private string _levelDescription;
    [SerializeField] private Object _sceneToLoad;
    [SerializeField] private LevelData _levelData;
    
    public int LevelIndex => this._levelIndex;
    public string LevelName => this._levelName;
    public string LevelDescription => this._levelDescription;
    public Object SceneToLoad => this._sceneToLoad;
    public LevelData LevelData => this._levelData;
}
