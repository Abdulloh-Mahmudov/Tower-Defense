using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_AI : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Transform _target;
    [SerializeField]
    private float _speed = 1.5f;
    [SerializeField]
    private float _health = 100f;
    private Animator _anim;
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
                Destroy(this.gameObject);
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

        if (_health < 1)
        {
            _agent.speed = 0;
            _anim.SetTrigger("Dead");
            Destroy(this.gameObject, 1.5f);
        }
        else
        {
            StartCoroutine(HitRoutine());
        }
    }

    IEnumerator HitRoutine()
    {
        _agent.speed = 1.5f;
        yield return new WaitForSeconds(1.5f);
        _agent.speed = _speed;
    }
}
