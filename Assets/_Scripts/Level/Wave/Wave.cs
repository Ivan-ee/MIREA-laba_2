using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave 
{
    [SerializeField] private GameObject _rangedEnemy;
    [SerializeField] private GameObject _meleeEnemy;
    [SerializeField] private int _rangedEnemyCount;
    [SerializeField] private int _meleeEnemyCount;

    public GameObject RangedEnemy => _rangedEnemy;
    public GameObject MeleeEnemy => _meleeEnemy;
    public int RangedEnemyCount => _rangedEnemyCount;
    public int MeleeEnemyCount => _meleeEnemyCount;
}
