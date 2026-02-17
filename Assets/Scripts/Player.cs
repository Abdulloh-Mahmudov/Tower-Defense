using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private LayerMask _mask;
    [SerializeField] private float _xBoundary;
    [SerializeField] private float _yBoundary;
    [SerializeField] private float _zBoundary;
    [SerializeField] private float _xBoundaryNegative;
    [SerializeField] private float _yBoundaryNegative;
    [SerializeField] private float _zBoundaryNegative;




    private void Update()
    {
        Movement();
        Boundaries();
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        Selection();
    }

    public void Selection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _mask))
            {
                if (hit.transform.gameObject.CompareTag("Platform"))
                {
                    SelectionManager.Instance.SelectObject(hit.transform.gameObject);
                }
                else
                {
                    SelectionManager.Instance.SelectObject(null);
                }
            }
        }
    }

    public void Movement()
    {
        if (Input.GetKey(KeyCode.R))
        {
            transform.Translate(new Vector3(0, 1, 0) * _speed * Time.deltaTime, Space.Self);
        }
        else if (Input.GetKey(KeyCode.T))
        {
            transform.Translate(new Vector3(0, -1, 0) * _speed * Time.deltaTime, Space.Self);
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(horizontal,0,vertical) * _speed * Time.deltaTime, Space.World);
    }

    public void Boundaries()
    {
        if (transform.position.x > _xBoundary)
        {
            transform.position = new Vector3(_xBoundary, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < _xBoundaryNegative)
        {
            transform.position = new Vector3(_xBoundaryNegative, transform.position.y, transform.position.z);
        }

        if (transform.position.y > _yBoundary)
        {
            transform.position = new Vector3(transform.position.x, _yBoundary, transform.position.z);
        }
        else if (transform.position.y < _yBoundaryNegative)
        {
            transform.position = new Vector3(transform.position.x, _yBoundaryNegative, transform.position.z);
        }

        if (transform.position.z > _zBoundary)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, _zBoundary);
        }
        else if (transform.position.z < _zBoundaryNegative)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, _zBoundaryNegative);
        }
    }

}
