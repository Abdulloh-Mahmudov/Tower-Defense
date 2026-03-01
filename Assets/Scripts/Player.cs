using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System;


public class Player : MonoBehaviour
{
    public static event Action<int> OnLivesChanged;
    public static event Action<int> OnWarfundsChanged;
    public static event Action<Color> OnProjectionUsed;
    public static event Action<Platform,int> OnUpgradePlatformSelected;

    [SerializeField] private GameObject _projection;
    [SerializeField] private GameObject _currentProjection;
    private int _currentProjectionID;
    [SerializeField] private bool _isProjecting;
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

    private void OnDisable()
    {
        Enemy_AI.OnEnemyReachedBase -= HandleEnemyAttack;
        SpawnManager.OnWaveEnded -= HandleWaveReward;
        Platform.OnTurretDismantle -= HandleTurretDismantle;
        Platform.OnTurretPurchase -= HandleTurretPurchase;
    }

    private void OnEnable()
    {
        Enemy_AI.OnEnemyReachedBase += HandleEnemyAttack;
        SpawnManager.OnWaveEnded += HandleWaveReward;
        Platform.OnTurretDismantle += HandleTurretDismantle;
        Platform.OnTurretPurchase += HandleTurretPurchase;
    }

    private void HandleTurretPurchase(int price)
    {
        LooseFunds(price);
    }

    private void HandleTurretDismantle(int reward)
    {
        GetWarFunds(reward);
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
        OnWarfundsChanged?.Invoke(_warFunds);
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

        if (Input.GetMouseButtonDown(1))
        {
            _isProjecting = false;
        }
        if (Keyboard.current.rKey.isPressed)
        {
            _isProjecting = true;
        }


        if (_isProjecting)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _mask))
            {
                if(_currentProjection == null)
                {
                    _currentProjection = Instantiate(_projection, hit.point, Quaternion.identity);
                }
                else
                {
                    _currentProjection.transform.position = hit.point;
                }
                if (hit.transform.gameObject.CompareTag("Platform"))
                {
                    OnProjectionUsed?.Invoke(Color.green);
                    if (Input.GetMouseButtonDown(0))
                        OnUpgradePlatformSelected?.Invoke(hit.transform.GetComponent<Platform>(),_currentProjectionID);
                    
                }
                else
                {
                    OnProjectionUsed?.Invoke(Color.red);
                }
            }
        }
        else
        {
            Destroy(_currentProjection);
            _currentProjection = null;
            Selection();
        }
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

    void Boundaries()
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
        OnWarfundsChanged?.Invoke(_warFunds);
    }

    void LooseFunds(int funds)
    {
        _warFunds -= funds;
        OnWarfundsChanged?.Invoke(_warFunds);
    }

    void LoseLives()
    {
        _lives--;
        OnLivesChanged?.Invoke(_lives);
    }
}
