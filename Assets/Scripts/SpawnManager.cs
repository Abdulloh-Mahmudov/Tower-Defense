using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Transform _destination;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _enemyContainer;
    [SerializeField] private GameObject _enemyPrefab;
    //[SerializeField] private int[] _enemyCountPerWave;
    //[SerializeField] private int[] _spawnRatePerWave;
    //[SerializeField] private int[] _delayBetweenWavesPerWave;
    [SerializeField] Wave[] waves;
    [SerializeField] private int _currentWave;
    [SerializeField] private int _enemyCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Spawn()
    {
        for(_currentWave = 0; _currentWave < waves.Length; _currentWave++)
        {
            yield return new WaitForSeconds(waves[_currentWave].waveDelay);
            for(_enemyCount = 0; _enemyCount < waves[_currentWave].enemyCount; _enemyCount++)
            {
                GameObject enemy = Instantiate(_enemyPrefab, _spawnPoint.position, Quaternion.identity);
                enemy.transform.parent = _enemyContainer;
                enemy.GetComponent<Enemy_AI>().SetDestination(_destination);
                yield return new WaitForSeconds(waves[_currentWave].spawnRate);
            }
        }
        
    }
}

[System.Serializable]
public class Wave
{
    public int enemyCount;
    public int spawnRate;
    public int waveDelay;
}