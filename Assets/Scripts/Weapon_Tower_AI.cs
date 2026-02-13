using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Tower_AI : MonoBehaviour
{
    [SerializeField] private Transform _base;
    [SerializeField] private GameObject _currentTarget;
    [SerializeField] private int _damage;
    private GameDevHQ.FileBase.Gatling_Gun.Gatling_Gun _turret;
    private Coroutine _attackRoutine;
    // Start is called before the first frame update
    void Start()
    {
        _turret = GetComponent<GameDevHQ.FileBase.Gatling_Gun.Gatling_Gun>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_currentTarget != null)
        {
            _base.LookAt(_currentTarget.transform);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Enemy" && _currentTarget == null)
        {
            Debug.Log("Enemy detected");
            _currentTarget = other.gameObject;
            _turret.ShootEnemy();
            Enemy_AI enemy = _currentTarget.GetComponent<Enemy_AI>();
            _attackRoutine = StartCoroutine(AttackRoutine(enemy));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Enemy")
        {
            _currentTarget = null;
            _turret.StopShooting();
            if (_attackRoutine != null)
            {
                StopCoroutine(_attackRoutine);
                _attackRoutine = null;
            }
        }
    }

    IEnumerator AttackRoutine(Enemy_AI enemy)
    {
        while (enemy != null)
        {
            enemy.Damage(_damage);
            yield return new WaitForSeconds(1.5f);
        }

        _turret.StopShooting();
        _currentTarget = null;
        StopCoroutine(_attackRoutine);
        _attackRoutine = null;
        
    }
}
