using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Tower_AI : MonoBehaviour
{
    [SerializeField] private Transform _base;
    [SerializeField] private GameObject _currentTarget;
    [SerializeField] private int _damage;
    private Coroutine _attackRoutine;
    // Start is called before the first frame update
    void Start()
    {

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
            StartShooting();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Enemy")
        {
            StopShooting();
        }
    }

    public virtual void StartShooting()
    {
        Enemy_AI enemy = _currentTarget.GetComponent<Enemy_AI>();
        _attackRoutine = StartCoroutine(AttackRoutine(enemy));
    }

    public virtual void StopShooting()
    {
        _currentTarget = null;
        if (_attackRoutine != null)
        {
            StopCoroutine(_attackRoutine);
            _attackRoutine = null;
        }
    }

    IEnumerator AttackRoutine(Enemy_AI enemy)
    {
        while (enemy != null)
        {
            enemy.Damage(_damage);
            yield return new WaitForSeconds(1.5f);
        }

        StopShooting();
        
    }
}
