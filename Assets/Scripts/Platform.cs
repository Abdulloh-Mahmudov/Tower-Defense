using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private MeshRenderer _renderer;
    private Color _origin;

    private void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
        _origin = _renderer.material.color;
    }

    public void Selected()
    {
        _renderer.material.color = Color.yellow;
    }

    public void UnSelected()
    {
        _renderer.material.color = _origin;
    }
}
