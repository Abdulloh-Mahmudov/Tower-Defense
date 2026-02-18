using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private MeshRenderer _renderer;
    private Color _origin;
    private bool _occupied;
    private Transform _turretManager;
    private Player _player;
    [SerializeField] private Transform _base;
    [SerializeField] private Turret[] _turrets;

    private void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
        _origin = _renderer.material.color;
        _player = GameObject.Find("Player").GetComponent<Player>();
        _turretManager = GameObject.Find("TurretManager").transform;
    }

    public void Selected()
    {
        _renderer.material.color = Color.yellow;
    }

    public void UnSelected()
    {
        _renderer.material.color = _origin;
    }

    public void Build(int turretID)
    {       
        if (_occupied == false && Player._warFunds >= _turrets[turretID].price)
        {
            _player.LooseFunds(_turrets[turretID].price);
            _occupied = true;
            GameObject turret = Instantiate(_turrets[turretID].prefab, _base.position, Quaternion.identity);
            turret.transform.parent = _turretManager;
        }
    }
}

[System.Serializable]
public class Turret
{
    public GameObject prefab;
    public int price;
}
