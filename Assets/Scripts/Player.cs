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
        SpawnManager.OnWaveEnded += HandleWaveReward;
        Platform.OnTurretDismantle += HandleTurretDismantle;
    }

    private void HandleTurretDismantle(int reward)
    {
        GetWarFunds(reward);
    }

    private void OnDisable()
    {
        Enemy_AI.OnEnemyReachedBase -= HandleEnemyAttack;
        SpawnManager.OnWaveEnded -= HandleWaveReward;
    }

    private void HandleWaveReward(int reward)
    {
        GetWarFunds(reward);
    }

    private void HandleEnemyAttack()
    {
        LoseLives();
    }

    private void Start()
    {
        _warFunds = 0;
        _uiManager = GameObject.Find("Canvas-UI").GetComponent<UI_Manager>();
        _uiManager.UpdateFunds(_warFunds);
    }

    private void Update()
    {
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
        Boundaries();
    }

    public void Boundaries()
    {
        Vector3 pos = transform.position;

        pos.x = Mathf.Clamp(pos.x, _xBoundaryNegative, _xBoundary);
        pos.y = Mathf.Clamp(pos.y, _yBoundaryNegative, _yBoundary);
        pos.z = Mathf.Clamp(pos.z, _zBoundaryNegative, _zBoundary);

        transform.position = pos;
    }

    void GetWarFunds(int funds)
    {
        _warFunds += funds;
        _uiManager.UpdateFunds(_warFunds);
    }

    public void LooseFunds(int funds)
    {
        _warFunds -= funds;
        _uiManager.UpdateFunds(_warFunds);
    }

    void LoseLives()
    {
        _lives--;
        OnLivesChanged?.Invoke(_lives);
    }
}
