using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_AI : MonoBehaviour
{
    private NavMeshAgent _enemy;
    private Transform _destination;
    // Start is called before the first frame update
    void Awake()
    {
        _enemy = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_destination != null)
        {
            float distance = Vector3.Distance(transform.position, _destination.position);
            if (distance < 0.5)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void SetDestination(Transform destination)
    {
        _destination = destination;
        _enemy.destination = _destination.position;
    }
}
