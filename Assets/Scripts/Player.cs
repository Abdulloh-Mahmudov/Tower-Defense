using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private LayerMask _mask;

    private void Update()
    {
        Movement();
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

}
