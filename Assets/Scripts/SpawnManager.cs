using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Transform _destination;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _enemyContainer;
    [SerializeField] private int _spawnRate;
    [SerializeField] private GameObject _enemyPrefab;
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
        while (true)
        {
            GameObject enemy = Instantiate(_enemyPrefab, _spawnPoint.position, Quaternion.identity);
            enemy.transform.parent = _enemyContainer;
            enemy.GetComponent<Enemy_AI>().SetDestination(_destination);
            yield return new WaitForSeconds(_spawnRate);
        }
    }
}
