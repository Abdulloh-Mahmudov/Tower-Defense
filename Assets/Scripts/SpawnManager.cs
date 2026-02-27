using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnManager : MonoBehaviour
{
    public static event Action<int> OnWaveEnded;

    [SerializeField] private Transform _destination;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _enemyContainer;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] Wave[] waves;
    [SerializeField] private int _currentWave;
    [SerializeField] private int _enemyCount = 0;
    [SerializeField] private int _currentEnemies = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        _currentEnemies = _enemyContainer.childCount;
    }



    IEnumerator Spawn()
    {
        for(_currentWave = 0; _currentWave < waves.Length; _currentWave++)
        {
            while(_currentEnemies > 0)
            {
                yield return null;
            }

            UI_Manager.Instance.UpdateWaves(_currentWave + 1, waves.Length);
            OnWaveEnded?.Invoke(waves[_currentWave].reward);
            yield return new WaitForSeconds(waves[_currentWave].waveDelay);
            for(_enemyCount = 0; _enemyCount < waves[_currentWave].enemyCount; _enemyCount++)
            {
                Enemy_AI enemy = Instantiate(_enemyPrefab, _spawnPoint.position, Quaternion.identity).GetComponent<Enemy_AI>();
                enemy.transform.parent = _enemyContainer;
                enemy.SetDestination(_destination);
                yield return new WaitForSeconds(waves[_currentWave].spawnRate);
            }
        }
        while(_currentEnemies > 0)
        {
            yield return null;
        }
        GameManager.Instance.GameWon();
    }
}

[System.Serializable]
public class Wave
{
    public int enemyCount;
    public int spawnRate;
    public int waveDelay;
    public int reward;
}