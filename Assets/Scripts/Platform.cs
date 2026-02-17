using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private MeshRenderer _renderer;
    private Color _origin;
    private bool _occupied;
    private Transform _turretManager;
    [SerializeField] private Transform _base;
    [SerializeField] private GameObject[] _turretPrefabs;

    private void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
        _origin = _renderer.material.color;
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
        if (_occupied == false)
        {
            _occupied = true;
            GameObject turret = Instantiate(_turretPrefabs[turretID], _base.position, Quaternion.identity);
            turret.transform.parent = _turretManager;
        }
    }
}
