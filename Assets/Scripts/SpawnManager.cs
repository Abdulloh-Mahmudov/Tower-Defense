using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Transform _destination;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _enemyContainer;
    [SerializeField] private GameObject _enemyPrefab;
    private Player _player;
    private UI_Manager _uiManager;
    [SerializeField] Wave[] waves;
    [SerializeField] private int _currentWave;
    [SerializeField] private int _enemyCount = 0;
    [SerializeField] private int _currentEnemies = 0;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _uiManager = GameObject.Find("Canvas-UI").GetComponent<UI_Manager>();
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

            _uiManager.UpdateWaves(_currentWave + 1, waves.Length);
            _player.GetWarFunds(waves[_currentWave].reward);
            yield return new WaitForSeconds(waves[_currentWave].waveDelay);
            for(_enemyCount = 0; _enemyCount < waves[_currentWave].enemyCount; _enemyCount++)
            {
                GameObject enemy = Instantiate(_enemyPrefab, _spawnPoint.position, Quaternion.identity);
                enemy.transform.parent = _enemyContainer;
                enemy.GetComponent<Enemy_AI>().SetDestination(_destination);
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