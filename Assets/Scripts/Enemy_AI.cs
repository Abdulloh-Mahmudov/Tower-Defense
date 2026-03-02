using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Enemy_AI : MonoBehaviour
{
    public static event Action<int> OnEnemyDied;
    public static event Action OnEnemyReachedBase;
    private NavMeshAgent _agent;
    private Transform _target;
    [SerializeField]
    private float _speed = 1.5f;
    [SerializeField]
    private float _health = 100f;
    [SerializeField]
    private int _reward = 150;
    private Animator _anim;
    private bool _isDead = false;
    // Start is called before the first frame update
    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_target != null)
        {
            float distance = Vector3.Distance(transform.position, _target.position);
            _agent.destination = _target.position;
            if (distance < 3)
            {
                ReachedDestination();
            }
        }
    }

    public void SetDestination(Transform destination)
    {
        _target = destination;
        _agent.destination = _target.position;
    }

    public void Damage(int amount)
    {
        _health -= amount;
        _anim.SetTrigger("Hit");

        if (_health < 1 && _isDead == false)
        {
            Died();
        }
        else
        {
            StartCoroutine(HitRoutine());
        }
    }

    private void Died()
    {
        OnEnemyDied?.Invoke(_reward);
        _agent.speed = 0;
        _isDead = true;
        _anim.SetTrigger("Dead");
        Destroy(this.gameObject, 1.5f);
    }

    private void ReachedDestination()
    {
        OnEnemyReachedBase?.Invoke();
        Destroy(this.gameObject);
    }

    IEnumerator HitRoutine()
    {
        _agent.speed = 1.5f;
        yield return new WaitForSeconds(1.5f);
        _agent.speed = _speed;
    }
}
