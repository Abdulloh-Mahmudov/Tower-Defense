using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager Instance;
    [SerializeField] private Text _funds;
    [SerializeField] private Text _wave;
    [SerializeField] private Text _lives;
    [SerializeField] private GameObject _actionMenus;
    [SerializeField] private GameObject _upgradeOne;
    [SerializeField] private GameObject _upgradeTwo;
    [SerializeField] private GameObject _gameOver;
    [SerializeField] private GameObject _gameWon;
    [SerializeField] private GameObject _mainUI;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private Text _statusText;

    private void OnDisable()
    {
        Player.OnLivesChanged -= UpDateLives;
        Player.OnWarfundsChanged -= UpdateWarfunds;
        Player.OnStatusChanged -= UpdateStatus;
        SpawnManager.OnWaveStart -= SpawnManager_OnWaveStart;
    }

    private void OnEnable()
    {
        Player.OnLivesChanged += UpDateLives;
        Player.OnWarfundsChanged += UpdateWarfunds;
        Player.OnStatusChanged += UpdateStatus;
        SpawnManager.OnWaveStart += SpawnManager_OnWaveStart;
    }

    private void UpdateStatus(int stages)
    {
        switch (stages)
        {
            case 1:
                _statusText.text = "Good";
                _statusText.color = Color.blue;
                break;
            case 2:
                _statusText.text = "Average";
                _statusText.color = Color.yellow;
                break;
            case 3:
                _statusText.text = "Bad";
                _statusText.color = Color.red;
                break;
            default:
                _statusText.text = "Good";
                _statusText.color = Color.green;
                break;
        }
    }

    private void SpawnManager_OnWaveStart(int current, int max)
    {
        _wave.text = current + "/" + max;
    }

    private void UpdateWarfunds(int funds)
    {
        _funds.text = funds.ToString();
    }

    private void UpDateLives(int lives)
    {
        _lives.text = lives.ToString();
    }



    private void Start()
    {
        Instance = this;
        _gameOver.SetActive(false);
        _gameWon.SetActive(false);
    }

    private void Update()
    {
        if (SelectionManager.Instance.SelectedObject != null)
        {
            _actionMenus.SetActive(true);
        }
        else
        {
            _actionMenus.SetActive(false);
        }
    }



    public void HideUI(GameObject element)
    {
        element.SetActive(false);
    }

    public void ShowUI(GameObject element)
    {
        if(SelectionManager.Instance.SelectedObject != null)
        {
            element.SetActive(true);
        }
    }

    public void ShowUpgradeUI()
    {
        if(SelectionManager.Instance.SelectedObject != null)
        {
            Platform platform = SelectionManager.Instance.SelectedObject.GetComponent<Platform>();
            int ID = platform._turretID;
            bool upgraded = platform._upgraded;
            bool occupied = platform._occupied;
            if (upgraded != true && occupied == true)
            {
                if (ID == 0)
                {
                    _upgradeOne.SetActive(true);
                    _upgradeTwo.SetActive(false);
                }
                else if (ID == 1)
                {
                    _upgradeOne.SetActive(false);
                    _upgradeTwo.SetActive(true);
                }
            }
        }
    }

    public void HideUpgradeUI()
    {
        for (int i = 0; i < _actionMenus.transform.childCount; i++)
        {
            _actionMenus.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void GameOver()
    {
        _gameOver.SetActive(true);
        _mainUI.SetActive(false);
    }

    public void GameWon()
    {
        _gameWon.SetActive(true);
        _mainUI.SetActive(false);
    }

    public void PauseMenu(bool active)
    {
        _pauseMenu.SetActive(active);
    }
}
