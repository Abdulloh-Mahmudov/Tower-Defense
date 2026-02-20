using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private MeshRenderer _renderer;
    private Color _origin;
    public bool _occupied;
    public bool _upgraded;
    [SerializeField] public int _turretID;
    private Transform _turretManager;
    private Player _player;
    [SerializeField] private Transform _base;
    [SerializeField] private Turret[] _turrets;
    [SerializeField] private GameObject _currentTurret;

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
            _turretID = turretID;
            _player.LooseFunds(_turrets[turretID].price);
            _occupied = true;
            _currentTurret = Instantiate(_turrets[turretID].prefab, _base.position, Quaternion.identity);
            _currentTurret.transform.parent = _turretManager;
        }
    }

    public void Upgrade(int turretID)
    {
        if (_occupied == true &&_upgraded == false && Player._warFunds >= _turrets[turretID].upgradePrice)
        {
            Destroy(_currentTurret);
            _player.LooseFunds(_turrets[turretID].upgradePrice);
            _occupied = true;
            _upgraded = true;
            _currentTurret = Instantiate(_turrets[turretID].prefabUpgrade, _base.position, Quaternion.identity);
            _currentTurret.transform.parent = _turretManager;
        }
    }

    public void Dismantle()
    {
        _turretID = 0;
        _player.GetWarFunds(100);
        _occupied = false;
        _upgraded = false;
        Destroy(_currentTurret);
    }
}

[System.Serializable]
public class Turret
{
    public GameObject prefab;
    public int price;
    public GameObject prefabUpgrade;
    public int upgradePrice;
}
