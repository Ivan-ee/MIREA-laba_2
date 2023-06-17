using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static UnityEvent OnFinishLevelEvent = new UnityEvent();
    
    [Header("Level Config")]
    [SerializeField] private Level _level;
    [SerializeField] private Transform _rangedSpawnPoint;
    [SerializeField] private Transform _meleeSpawnPoint;
    [SerializeField] private float _timeBetweenWaves;
    [SerializeField] private float _timeBetweenSpawn;

    private List<Enemy> _activeEnemies = new List<Enemy>();
    
    private int _currentWaveIndex = -1;
    private bool _isWaveActive = false;

    private void Awake()
    {
        Enemy.OnEnemyDestroyedEvent.AddListener(OnEnemyDestroyed);
    }
    void Start()
    {
        StartCoroutine(StartWaveWithDelay());
    }
    void Update()
    {
        if (_isWaveActive && IsWaveComplete())
        {
            _isWaveActive = false;
            StartCoroutine(StartWaveWithDelay());
        }
    }
    IEnumerator StartWaveWithDelay()
    {
        yield return new WaitForSeconds(_timeBetweenWaves);
        _currentWaveIndex++;
        StartWave();
    }
    void StartWave()
    {
        if (_currentWaveIndex >= _level.LevelData.Waves.Count)
        {
            StartCoroutine(FinishLevel());
            Debug.Log("Все волны пройдены!");
            return;
        }

        Wave nextWave = _level.LevelData.Waves[_currentWaveIndex];
        StartCoroutine(SpawnEnemies(nextWave));
        _isWaveActive = true;
        Debug.Log($"Запускается волна - {_currentWaveIndex + 1}");
    }

    IEnumerator SpawnEnemies(Wave wave)
    {
        for (int i = 0; i < wave.RangedEnemyCount; i++)
        {
            SpawnRangedEnemy(wave.RangedEnemy);
            yield return new WaitForSeconds(_timeBetweenSpawn); 
        }

        for (int i = 0; i < wave.MeleeEnemyCount; i++)
        {
            SpawnMeleeEnemy(wave.MeleeEnemy);
            yield return new WaitForSeconds(_timeBetweenSpawn);
        }
    }
    void SpawnRangedEnemy(GameObject enemyPrefab)
    {
        GameObject enemyInstance = Instantiate(enemyPrefab, _rangedSpawnPoint.position, Quaternion.identity);
        Enemy enemy = enemyInstance.GetComponent<Enemy>();
        _activeEnemies.Add(enemy);
    }
    void SpawnMeleeEnemy(GameObject enemyPrefab)
    {
        GameObject enemyInstance = Instantiate(enemyPrefab, _meleeSpawnPoint.position, Quaternion.identity);
        Enemy enemy = enemyInstance.GetComponent<Enemy>();
        _activeEnemies.Add(enemy);
    }
    private void OnEnemyDestroyed(Enemy destroyedEnemy)
    {
        _activeEnemies.Remove(destroyedEnemy);
    }
    bool IsWaveComplete()
    {
        return _activeEnemies.Count == 0;;
    }

    IEnumerator FinishLevel()
    {
        yield return new WaitForSeconds(5f);
        OnFinishLevelEvent?.Invoke();
    }
}
