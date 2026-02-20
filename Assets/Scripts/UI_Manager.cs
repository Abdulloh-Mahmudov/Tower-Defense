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

    public void UpdateFunds(int funds)
    {
       _funds.text = funds.ToString();
    }

    public void UpdateWaves(int current, int max)
    {
        _wave.text = current + "/" + max;
    }

    public void UpdateLives(int lives)
    {
        _lives.text = lives.ToString();
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
            int ID = SelectionManager.Instance.SelectedObject.GetComponent<Platform>()._turretID;
            bool upgraded = SelectionManager.Instance.SelectedObject.GetComponent<Platform>()._upgraded;
            bool occupied = SelectionManager.Instance.SelectedObject.GetComponent<Platform>()._occupied;
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
}
