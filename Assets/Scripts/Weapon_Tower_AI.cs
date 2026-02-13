using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Tower_AI : MonoBehaviour
{
    [SerializeField] private Transform _base;
    [SerializeField] private GameObject _currentTarget;
    private GameDevHQ.FileBase.Gatling_Gun.Gatling_Gun _turret;
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
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Enemy")
        {
            _currentTarget = null;
            _turret.StopShooting();
        }
    }
}
