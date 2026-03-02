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

    [SerializeField] private Projections[] _projection;
    [SerializeField] private GameObject _currentProjection;
    private int _currentProjectionID;
    [SerializeField] private bool _isProjecting;
    [SerializeField] private float _speed;
    [SerializeField] private int _initialFunds;
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
        Enemy_AI.OnEnemyDied -= HandleEnemyReward;
        Platform.OnTurretDismantle -= HandleTurretDismantle;
        Platform.OnTurretPurchase -= HandleTurretPurchase;
    }

    private void OnEnable()
    {
        Enemy_AI.OnEnemyReachedBase += HandleEnemyAttack;
        Enemy_AI.OnEnemyDied += HandleEnemyReward;
        Platform.OnTurretDismantle += HandleTurretDismantle;
        Platform.OnTurretPurchase += HandleTurretPurchase;
    }

    private void HandleEnemyReward(int reward)
    {
        GetWarFunds(reward);
    }

    private void HandleTurretPurchase(int price)
    {
        LooseFunds(price);
    }

    private void HandleTurretDismantle(int reward)
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
        _warFunds = _initialFunds;
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


        if (_isProjecting)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _mask))
            {
                if(_currentProjection!= null)
                {
                    _currentProjection.transform.position = hit.point;
                }
                if (hit.transform.gameObject.CompareTag("Platform") && _projection[_currentProjectionID].price <= _warFunds)
                {
                    if(hit.transform.GetComponent<Platform>().IsOccupied() != true)
                    {
                        OnProjectionUsed?.Invoke(Color.green);
                        if (Input.GetMouseButtonDown(0))
                            OnUpgradePlatformSelected?.Invoke(hit.transform.GetComponent<Platform>(), _currentProjectionID);
                    }
                    else
                    {
                        OnProjectionUsed?.Invoke(Color.red);
                    }
                    
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

    public void Projection(int turretID)
    {
        _currentProjectionID = turretID;
        _isProjecting = true;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _mask))
        {
            if (_currentProjection == null)
            {
                _currentProjection = Instantiate(_projection[turretID].prefab, hit.point, Quaternion.identity);
            }
            else
            {
                _currentProjection.transform.position = hit.point;
            }
        }
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

[System.Serializable]
public class Projections
{
    public GameObject prefab;
    public int price;
}
