using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projection : MonoBehaviour
{
    private MeshRenderer[] _renderers;
    private Color _current;

    private void OnDisable()
    {
        Player.OnProjectionUsed -= ColorChange;
        Player.OnUpgradePlatformSelected -= BuildTurret;
    }

    private void OnEnable()
    {
        _renderers = transform.GetComponentsInChildren<MeshRenderer>();
        Player.OnProjectionUsed += ColorChange;
        Player.OnUpgradePlatformSelected += BuildTurret;
    }

    private void BuildTurret(Platform platform, int turretID)
    {
        platform.Build(turretID);
    }

    private void ColorChange(Color currentColor)
    {
        _current = currentColor;
    }

    private void Update()
    {
        foreach (MeshRenderer i in _renderers)
        {
            i.material.color = _current;
        }
    }
}
