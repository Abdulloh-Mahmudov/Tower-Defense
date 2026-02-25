using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System;


public class Player : MonoBehaviour
{
    public static event Action<int> OnLivesChanged;

    [SerializeField] private float _speed;
    public static int _warFunds;
    [SerializeField] private int _lives;
    [SerializeField] private LayerMask _mask;
    [SerializeField] private float _xBoundary;
    [SerializeField] private float _yBoundary;
    [SerializeField] private float _zBoundary;
    [SerializeField] private float _xBoundaryNegative;
    [SerializeField] private float _yBoundaryNegative;
    [SerializeField] private float _zBoundaryNegative;

    private UI_Manager _uiManager;

    private void OnEnable()
    {
        Enemy_AI.OnEnemyReachedBase += HandleEnemyAttack;
    }

    private void HandleEnemyAttack()
    {
        LoseLives();
    }

    private void OnDisable()
    {
        Enemy_AI.OnEnemyReachedBase -= HandleEnemyAttack;
    }

    private void Start()
    {
        _warFunds = 0;
        _uiManager = GameObject.Find("Canvas-UI").GetComponent<UI_Manager>();
        _uiManager.UpdateFunds(_warFunds);
    }

    private void Update()
    {
        Boundaries();
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if(_lives <= 0)
        {
            GameManager.Instance.GameOver();
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

    public void Movement(Vector2 direction)
    {
        transform.Translate(new Vector3(direction.x,0,direction.y) * _speed * Time.deltaTime, Space.World);
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

    public void GetWarFunds(int funds)
    {
        _warFunds += funds;
        _uiManager.UpdateFunds(_warFunds);
    }

    public void LooseFunds(int funds)
    {
        _warFunds -= funds;
        _uiManager.UpdateFunds(_warFunds);
    }

    public void LoseLives()
    {
        _lives--;
        OnLivesChanged?.Invoke(_lives);
    }
}
